using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DialogueChainSingleton : SingletonLoadableMonoBehaviour<DialogueChainSingleton>, ILocaleChangedReceiver
{
	public DialogueChainContainer Donations { get; private set; }

	public DialogueChainContainer PositiveComments { get; private set; }

	public DialogueChainContainer NegativeComments { get; private set; }

	public DialogueChainContainer MainNews { get; private set; }

	public DialogueChainContainer SideNews { get; private set; }

	public DialogueChainContainer OpinionNews { get; private set; }

	public override Type[] GetDependenciesArray()
	{
		return new Type[1] { typeof(WordGeneratorSingleton) };
	}

	private void LocalizeInternal()
	{
		foreach (DialogueChainContainer item in LocalizableText())
		{
			item.LocalizeText(LocalizationSettings.SelectedLocale);
		}
	}

	public void OnLocaleChanged(Locale newLocale)
	{
		if (IsLoaded)
		{
			LocalizeInternal();
		}
	}

	public IEnumerable<DialogueChainContainer> LocalizableText()
	{
		yield return PositiveComments;
		yield return Donations;
		yield return NegativeComments;
		yield return MainNews;
		yield return OpinionNews;
		yield return SideNews;
	}

	public void LoadFromFiles()
	{
		PositiveComments = new DialogueChainContainer("TextFiles/Comments/PositiveComments", LOC.Comments.TABLE_REFERENCE, StreamManager.StreamEventTagsParse);
		Donations = new DialogueChainContainer("TextFiles/Comments/Donations", LOC.Comments.TABLE_REFERENCE, StreamManager.StreamEventTagsParse);
		NegativeComments = new DialogueChainContainer("TextFiles/Comments/NegativeComments", LOC.Comments.TABLE_REFERENCE, StreamManager.StreamEventTagsParse);
		MainNews = new DialogueChainContainer("TextFiles/News/MainNews", LOC.News.TABLE_REFERENCE);
		OpinionNews = new DialogueChainContainer("TextFiles/News/OpinionNews", LOC.News.TABLE_REFERENCE);
		SideNews = new DialogueChainContainer("TextFiles/News/SideNews", LOC.News.TABLE_REFERENCE);
	}

	protected override UniTask LoadInner(IProgressManager progressManager)
	{
		LoadFromFiles();
		LocalizeInternal();
		if (Application.isPlaying)
		{
			LocalizationSingleton.Register(this);
		}
		return UniTask.CompletedTask;
	}
}
