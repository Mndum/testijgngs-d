using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "TextDataGenerated", menuName = "Automated/TextDataGenerated", order = 1)]
public class TextDataGenerated : SingletonScriptableObject<TextDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class CreditsTextContainer
	{
		public SerializableAutomaticReference<TextAsset> Credits;

		public SerializableAutomaticReference<TextAsset> Credits2;

		public SerializableAutomaticReference<TextAsset> Credits3;

		public SerializableAutomaticReference<TextAsset> CreditsTranslation;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextAsset>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextAsset>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextAsset>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextAsset>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextAsset>[] All = Array.Empty<SerializableAutomaticReference<TextAsset>>();
	}

	[Serializable]
	public class OtherTextContainer
	{
		public SerializableAutomaticReference<TextAsset> _8ballAsciiArt;

		public SerializableAutomaticReference<TextAsset> annaliesay;

		public SerializableAutomaticReference<TextAsset> bootup1;

		public SerializableAutomaticReference<TextAsset> bootup2;

		public SerializableAutomaticReference<TextAsset> cowsay;

		public SerializableAutomaticReference<TextAsset> junBoobs;

		public SerializableAutomaticReference<TextAsset> junMouth;

		public SerializableAutomaticReference<TextAsset> junsay;

		public SerializableAutomaticReference<TextAsset> sheepsay;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextAsset>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextAsset>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextAsset>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextAsset>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextAsset>[] All = Array.Empty<SerializableAutomaticReference<TextAsset>>();
	}

	public CreditsTextContainer CreditsText;

	public OtherTextContainer OtherText;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		CreditsText.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextAsset>>
		{
			["Credits"] = CreditsText.Credits,
			["Credits2"] = CreditsText.Credits2,
			["Credits3"] = CreditsText.Credits3,
			["CreditsTranslation"] = CreditsText.CreditsTranslation
		};
		CreditsText.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextAsset>>
		{
			[("Credits", "Credits")] = CreditsText.Credits,
			[("Credits2", "Credits2")] = CreditsText.Credits2,
			[("Credits3", "Credits3")] = CreditsText.Credits3,
			[("CreditsTranslation", "CreditsTranslation")] = CreditsText.CreditsTranslation
		};
		CreditsText.All = new SerializableAutomaticReference<TextAsset>[4] { CreditsText.Credits, CreditsText.Credits2, CreditsText.Credits3, CreditsText.CreditsTranslation };
		OtherText.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextAsset>>
		{
			["8ballAsciiArt"] = OtherText._8ballAsciiArt,
			["annaliesay"] = OtherText.annaliesay,
			["bootup1"] = OtherText.bootup1,
			["bootup2"] = OtherText.bootup2,
			["cowsay"] = OtherText.cowsay,
			["junBoobs"] = OtherText.junBoobs,
			["junMouth"] = OtherText.junMouth,
			["junsay"] = OtherText.junsay,
			["sheepsay"] = OtherText.sheepsay
		};
		OtherText.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextAsset>>
		{
			[("8ballAsciiArt", "8ballAsciiArt")] = OtherText._8ballAsciiArt,
			[("annaliesay", "annaliesay")] = OtherText.annaliesay,
			[("bootup1", "bootup1")] = OtherText.bootup1,
			[("bootup2", "bootup2")] = OtherText.bootup2,
			[("cowsay", "cowsay")] = OtherText.cowsay,
			[("junBoobs", "junBoobs")] = OtherText.junBoobs,
			[("junMouth", "junMouth")] = OtherText.junMouth,
			[("junsay", "junsay")] = OtherText.junsay,
			[("sheepsay", "sheepsay")] = OtherText.sheepsay
		};
		OtherText.All = new SerializableAutomaticReference<TextAsset>[9] { OtherText._8ballAsciiArt, OtherText.annaliesay, OtherText.bootup1, OtherText.bootup2, OtherText.cowsay, OtherText.junBoobs, OtherText.junMouth, OtherText.junsay, OtherText.sheepsay };
	}

	public void RegisterLua()
	{
		UserData.RegisterType<TextDataGenerated>();
		UserData.RegisterType<CreditsTextContainer>();
		UserData.RegisterType<TextAsset>();
		UserData.RegisterType<SerializableAutomaticReference<TextAsset>>();
		UserData.RegisterType<OtherTextContainer>();
	}
}
