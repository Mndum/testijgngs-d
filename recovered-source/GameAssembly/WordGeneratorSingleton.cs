using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class WordGeneratorSingleton : SingletonLoadableMonoBehaviour<WordGeneratorSingleton>, ILocaleChangedReceiver
{
	private static Dictionary<string, List<string>> _words;

	private void LocalizeInternal()
	{
		StringTable result = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Other.TABLE_REFERENCE, LocalizationSettings.ProjectLocale).Result;
		StringTable result2 = LocalizationSettings.StringDatabase.GetTableAsync(LOC.Other.TABLE_REFERENCE).Result;
		string text = "Generator_";
		foreach (StringTableEntry value in result.Values)
		{
			if (value.Key.StartsWith(text))
			{
				StringTableEntry stringTableEntry = result2.GetEntry(value.KeyId) ?? value;
				string key = value.Key.Substring(text.Length);
				_words[key] = (from t in stringTableEntry.Value.Split(',', '、', '、')
					select t.Trim()).ToList();
			}
		}
	}

	public void OnLocaleChanged(Locale newLocale)
	{
		if (IsLoaded)
		{
			LocalizeInternal();
		}
	}

	protected override UniTask LoadInner(IProgressManager progressManager)
	{
		_words = new Dictionary<string, List<string>>();
		LocalizeInternal();
		if (Application.isPlaying)
		{
			LocalizationSingleton.Register(this);
		}
		return UniTask.CompletedTask;
	}

	public bool CheckFormat(string type)
	{
		return GetRandomWord(type) != "";
	}

	public string GetRandomWord(string type)
	{
		List<string> value;
		if (type.StartsWith("number"))
		{
			string[] array = type.Split(',');
			if (array.Length == 3 && int.TryParse(array[1], out var result) && int.TryParse(array[2], out var result2) && result2 > result)
			{
				return UnityEngine.Random.Range(result, result2).ToString();
			}
		}
		else if (_words.TryGetValue(type, out value))
		{
			return value[UnityEngine.Random.Range(0, value.Count)];
		}
		return "";
	}

	public string GetRandomWord(string type, int seed)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState(seed);
		string result = "";
		List<string> value;
		if (type.StartsWith("number"))
		{
			string[] array = type.Split(',');
			if (array.Length == 3 && int.TryParse(array[1], out var result2) && int.TryParse(array[2], out var result3) && result3 > result2)
			{
				result = UnityEngine.Random.Range(result2, result3).ToString();
			}
		}
		else if (_words.TryGetValue(type, out value))
		{
			result = value[UnityEngine.Random.Range(0, value.Count)];
		}
		UnityEngine.Random.state = state;
		return result;
	}

	public override Type[] GetDependenciesArray()
	{
		return null;
	}
}
