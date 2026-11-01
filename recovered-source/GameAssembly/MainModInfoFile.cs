using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[Serializable]
public class MainModInfoFile : ISerializationCallbackReceiver
{
	public static readonly string ModInfoFileName = "mod.json";

	public string name;

	public string description;

	[SerializeField]
	private string modVersion;

	private ParsedVersion _parsedModVersion = ParsedVersion.FailedParsingVersion;

	public ModsSingleton.ModScriptWindow OnGameStart = new ModsSingleton.ModScriptWindow();

	[SerializeField]
	private string targetVersion;

	private ParsedVersion _parsedTargetVersion = ParsedVersion.FailedParsingVersion;

	[SerializeField]
	private string languageCode;

	private Locale _locale;

	public TechnicalData doNotChangeVariablesBelowThis;

	private static string DefaultName = "Default mod name";

	public ParsedVersion ParsedModVersion
	{
		get
		{
			return _parsedModVersion;
		}
		set
		{
			_parsedModVersion = value;
			modVersion = _parsedModVersion.StringRepresentation;
		}
	}

	public ParsedVersion ParsedTargetGameVersion
	{
		get
		{
			return _parsedTargetVersion;
		}
		set
		{
			_parsedTargetVersion = value;
			targetVersion = _parsedTargetVersion.StringRepresentation;
		}
	}

	public Locale Locale
	{
		get
		{
			return _locale;
		}
		set
		{
			_locale = value;
			languageCode = value.Identifier.Code;
		}
	}

	public Guid guid => doNotChangeVariablesBelowThis.guid;

	public TechnicalData TechnicalData
	{
		get
		{
			return doNotChangeVariablesBelowThis;
		}
		set
		{
			doNotChangeVariablesBelowThis = value;
		}
	}

	public string DirectoryName => doNotChangeVariablesBelowThis.guid.ToString();

	public bool IsDefault()
	{
		return name == DefaultName;
	}

	public static MainModInfoFile CreateMinimal()
	{
		return new MainModInfoFile
		{
			name = DefaultName,
			description = "Put your mod's description here",
			TechnicalData = TechnicalData.CreateDefault(),
			ParsedTargetGameVersion = ParsedVersion.CurrentGameVersion,
			ParsedModVersion = ParsedVersion.ModDefaultVersion,
			Locale = LocalizationSettings.ProjectLocale
		};
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		ParsedTargetGameVersion = ParsedVersion.CreateFromString(targetVersion);
		if (string.IsNullOrWhiteSpace(modVersion))
		{
			ParsedModVersion = ParsedVersion.ModDefaultVersion;
		}
		else
		{
			ParsedModVersion = ParsedVersion.CreateFromString(modVersion);
		}
		Locale = SingletonLoadableMonoBehaviour<ModsSingleton>.Instance.GetLocaleFromCodeOrProject(languageCode);
	}
}
