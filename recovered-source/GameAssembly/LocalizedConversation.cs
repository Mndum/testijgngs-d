using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public abstract class LocalizedConversation
{
	public class LocalizedLineSummary
	{
		public enum LineStatusEnum
		{
			Correct,
			Missing,
			MissingOnPurpose,
			Empty,
			Changed,
			ExcessiveLine
		}

		public string ModifiedText { get; set; }

		public BetterConversationManager.SayLine Item { get; private set; }

		public LocalizedStringReference LocalizedLine { get; private set; }

		public LineStatusEnum LineStatus { get; private set; }

		public LocalizedLineSummary(BetterConversationManager.SayLine item, LocalizedStringReference localizedLine, LineStatusEnum lineStatus)
		{
			Item = item;
			LocalizedLine = localizedLine;
			LineStatus = lineStatus;
			ModifiedText = item?.UnlocalizedText ?? "";
		}

		public static bool IsWrong(LineStatusEnum status)
		{
			if (status != LineStatusEnum.Correct)
			{
				return status != LineStatusEnum.MissingOnPurpose;
			}
			return false;
		}
	}

	public class LocalizedLinesSummaryContainer
	{
		private bool _summaryDirty = true;

		public LocalizedConversation LocalizedConversation;

		public BetterConversationManager.ConversationData ParsedData = new BetterConversationManager.ConversationData();

		public List<LocalizedLineSummary> LineSummaries = new List<LocalizedLineSummary>();

		public Dictionary<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>> StatusDict = new Dictionary<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>>();

		public LocalizedLinesSummaryContainer(LocalizedConversation localizedConversation)
		{
			LocalizedConversation = localizedConversation;
		}

		private void Add(LocalizedLineSummary summary)
		{
			LineSummaries.Add(summary);
			StatusDict.AddToLookup(summary.LineStatus, summary);
		}

		public void Recalculate(bool forceUpdate)
		{
			if (!forceUpdate && !_summaryDirty)
			{
				return;
			}
			_summaryDirty = false;
			ParsedData.Clear();
			LineSummaries.Clear();
			foreach (KeyValuePair<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>> item in StatusDict)
			{
				item.Value.Clear();
			}
			ParsedData.Parse(LocalizedConversation.mainText);
			LocalizedConversation localizedConversation = LocalizedConversation;
			ReadOnlyCollection<BetterConversationManager.SayLine> sayLines = ParsedData.SayLines;
			List<LocalizedStringReference> list = localizedConversation.GetLocalizedLines().ToList();
			for (int i = 0; i < sayLines.Count; i++)
			{
				BetterConversationManager.SayLine sayLine = sayLines[i];
				if (i >= list.Count)
				{
					if (sayLine.HasText)
					{
						Add(new LocalizedLineSummary(sayLine, null, LocalizedLineSummary.LineStatusEnum.Missing));
					}
					else
					{
						Add(new LocalizedLineSummary(sayLine, null, LocalizedLineSummary.LineStatusEnum.MissingOnPurpose));
					}
					continue;
				}
				LocalizedStringReference localizedStringReference = list[i];
				StringTableEntry entryProjectLocale = localizedStringReference.GetEntryProjectLocale();
				if (entryProjectLocale == null)
				{
					Add(new LocalizedLineSummary(sayLine, localizedStringReference, LocalizedLineSummary.LineStatusEnum.Empty));
				}
				else if (entryProjectLocale.Value == sayLine.UnlocalizedText)
				{
					Add(new LocalizedLineSummary(sayLine, localizedStringReference, LocalizedLineSummary.LineStatusEnum.Correct));
				}
				else
				{
					Add(new LocalizedLineSummary(sayLine, localizedStringReference, LocalizedLineSummary.LineStatusEnum.Changed));
				}
			}
			for (int j = sayLines.Count; j < list.Count; j++)
			{
				Add(new LocalizedLineSummary(null, list[j], LocalizedLineSummary.LineStatusEnum.ExcessiveLine));
			}
		}

		public IEnumerable<string> GetAllNames()
		{
			foreach (LocalizedLineSummary lineSummary in LineSummaries)
			{
				if (lineSummary.Item != null && lineSummary.Item.HasCharacterName)
				{
					yield return lineSummary.Item.CharacterName;
				}
			}
		}

		public IEnumerable<BetterConversationManager.SayLine> GetAllLinesBy(string id)
		{
			foreach (LocalizedLineSummary lineSummary in LineSummaries)
			{
				if (lineSummary.Item != null && lineSummary.Item.CharacterName == id)
				{
					yield return lineSummary.Item;
				}
			}
		}

		public void MarkDirty()
		{
			_summaryDirty = true;
		}

		public bool HasAny(LocalizedLineSummary.LineStatusEnum statusEnum, out List<LocalizedLineSummary> summaries)
		{
			if (StatusDict.TryGetValue(statusEnum, out summaries))
			{
				return summaries.Count > 0;
			}
			return false;
		}

		public Result IsGood()
		{
			if (StatusDict.Any((KeyValuePair<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>> x) => LocalizedLineSummary.IsWrong(x.Key) && x.Value.Count > 0))
			{
				return Result.FailWithDescription("One of the lines is fucked!");
			}
			if (ParsedData.TryGetParsingErrors(out var errors))
			{
				return Result.All(errors);
			}
			if (string.IsNullOrEmpty(LocalizedConversation.mainText))
			{
				return Result.FailWithDescription("Empty text!");
			}
			return ParsedData.CheckAllSpecialItems();
		}

		public bool IsSomethingWrong()
		{
			if (!StatusDict.Any((KeyValuePair<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>> x) => LocalizedLineSummary.IsWrong(x.Key) && x.Value.Count > 0) && !ParsedData.TryGetParsingErrors(out var _) && !ParsedData.CheckAllSpecialItems().IsFail)
			{
				return string.IsNullOrEmpty(LocalizedConversation.mainText);
			}
			return true;
		}

		public string GetStatusDescription()
		{
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				foreach (KeyValuePair<LocalizedLineSummary.LineStatusEnum, List<LocalizedLineSummary>> item in StatusDict)
				{
					if (item.Value.Count > 0 && item.Key != LocalizedLineSummary.LineStatusEnum.Correct)
					{
						bool flag = LocalizedLineSummary.IsWrong(item.Key);
						if (flag)
						{
							stringBuilder.AppendColorTagStart(Color.red);
						}
						stringBuilder.Append(item.Key);
						stringBuilder.Append(" ");
						stringBuilder.Append(item.Value.Count);
						stringBuilder.AppendLine();
						if (flag)
						{
							stringBuilder.AppendColorTagEnd();
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		public void FillMissing()
		{
			if (HasAny(LocalizedLineSummary.LineStatusEnum.Missing, out var summaries))
			{
				LocalizedConversation.CreateLinesAtEnd(summaries.Count);
				MarkDirty();
			}
			else
			{
				Debug.LogError("No missing found!");
			}
		}

		public void PushAllChanges(bool removeLocalization, Locale targetLocale = null)
		{
			if (HasAny(LocalizedLineSummary.LineStatusEnum.Changed, out var summaries))
			{
				MarkDirty();
				if ((object)targetLocale == null)
				{
					targetLocale = LocalizationSettings.ProjectLocale;
				}
				foreach (LocalizedLineSummary item in summaries)
				{
					item.LocalizedLine.SetString(item.Item.UnlocalizedText, targetLocale);
					if (removeLocalization)
					{
						LocalizedStringReference localizedLine = item.LocalizedLine;
						LOS.ClearAllNonTargetLocaleEntries(localizedLine.KeyId, localizedLine.TargetTable, targetLocale);
					}
				}
			}
			MarkDirty();
		}
	}

	public string mainText;

	public abstract IEnumerable<LocalizedStringReference> GetLocalizedLines();

	public IEnumerator GetConversationEnumerator(BetterConversationManager.ConversationItemWriteSettings writeSettings = null)
	{
		BetterConversationManager.ConversationData conversationData = new BetterConversationManager.ConversationData();
		conversationData.Parse(mainText);
		conversationData.SetLocalization(GetLocalizedLines());
		return BetterConversationManager.DoConversation(conversationData, writeSettings);
	}

	public BetterConversationManager.ConversationData GetUnlocalizedConversationData()
	{
		BetterConversationManager.ConversationData conversationData = new BetterConversationManager.ConversationData();
		conversationData.Parse(mainText);
		return conversationData;
	}

	public abstract void CreateLinesAtEnd(int count);

	public bool CreateEmptyKeys(Func<int, string> keyNameGenerator)
	{
		bool flag = false;
		foreach (var item3 in GetLocalizedLines().Select((LocalizedStringReference x, int i) => (x: x, i: i)))
		{
			LocalizedStringReference item = item3.x;
			int item2 = item3.i;
			flag |= item.CreateIfNotExists(keyNameGenerator(item2));
		}
		MarkDirty();
		return flag;
	}

	protected void MarkDirty()
	{
	}

	public LocalizedLinesSummaryContainer GetSummary(bool forceUpdate = false)
	{
		LocalizedLinesSummaryContainer localizedLinesSummaryContainer = new LocalizedLinesSummaryContainer(this);
		localizedLinesSummaryContainer.Recalculate(forceUpdate);
		return localizedLinesSummaryContainer;
	}
}
