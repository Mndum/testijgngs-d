using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "FontDataGenerated", menuName = "Automated/FontDataGenerated", order = 1)]
public class FontDataGenerated : SingletonScriptableObject<FontDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class TextPrefabsContainer
	{
		public SerializableAutomaticReference<TextMeshProUGUI> Commissioner_Commissioner_Text;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> Commissioner_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextMeshProUGUI>> Commissioner_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] Commissioner_All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();

		public SerializableAutomaticReference<TextMeshProUGUI> Fonarto_Fonarto_Text;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> Fonarto_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextMeshProUGUI>> Fonarto_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] Fonarto_All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();

		public SerializableAutomaticReference<TextMeshProUGUI> Main_Font_Main_Text;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> Main_Font_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextMeshProUGUI>> Main_Font_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] Main_Font_All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();

		public SerializableAutomaticReference<TextMeshProUGUI> Monospace_Font_Monospace_Text;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> Monospace_Font_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextMeshProUGUI>> Monospace_Font_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] Monospace_Font_All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();

		public SerializableAutomaticReference<TextMeshProUGUI> Noto_Sans_NotoSans_Text;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> Noto_Sans_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<TextMeshProUGUI>> Noto_Sans_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] Noto_Sans_All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();

		[NonSerialized]
		public SerializableAutomaticReference<TextMeshProUGUI>[] All = Array.Empty<SerializableAutomaticReference<TextMeshProUGUI>>();
	}

	public TextPrefabsContainer TextPrefabs;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		TextPrefabs.Commissioner_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> { ["Commissioner Text"] = TextPrefabs.Commissioner_Commissioner_Text };
		TextPrefabs.Commissioner_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>> { [("Commissioner Text", "Commissioner Text")] = TextPrefabs.Commissioner_Commissioner_Text };
		TextPrefabs.Commissioner_All = new SerializableAutomaticReference<TextMeshProUGUI>[1] { TextPrefabs.Commissioner_Commissioner_Text };
		TextPrefabs.Fonarto_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> { ["Fonarto Text"] = TextPrefabs.Fonarto_Fonarto_Text };
		TextPrefabs.Fonarto_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>> { [("Fonarto Text", "Fonarto Text")] = TextPrefabs.Fonarto_Fonarto_Text };
		TextPrefabs.Fonarto_All = new SerializableAutomaticReference<TextMeshProUGUI>[1] { TextPrefabs.Fonarto_Fonarto_Text };
		TextPrefabs.Main_Font_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> { ["Main Text"] = TextPrefabs.Main_Font_Main_Text };
		TextPrefabs.Main_Font_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>> { [("Main Text", "Main Text")] = TextPrefabs.Main_Font_Main_Text };
		TextPrefabs.Main_Font_All = new SerializableAutomaticReference<TextMeshProUGUI>[1] { TextPrefabs.Main_Font_Main_Text };
		TextPrefabs.Monospace_Font_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> { ["Monospace Text"] = TextPrefabs.Monospace_Font_Monospace_Text };
		TextPrefabs.Monospace_Font_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>> { [("Monospace Text", "Monospace Text")] = TextPrefabs.Monospace_Font_Monospace_Text };
		TextPrefabs.Monospace_Font_All = new SerializableAutomaticReference<TextMeshProUGUI>[1] { TextPrefabs.Monospace_Font_Monospace_Text };
		TextPrefabs.Noto_Sans_RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<TextMeshProUGUI>> { ["NotoSans Text"] = TextPrefabs.Noto_Sans_NotoSans_Text };
		TextPrefabs.Noto_Sans_RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<TextMeshProUGUI>> { [("NotoSans Text", "NotoSans Text")] = TextPrefabs.Noto_Sans_NotoSans_Text };
		TextPrefabs.Noto_Sans_All = new SerializableAutomaticReference<TextMeshProUGUI>[1] { TextPrefabs.Noto_Sans_NotoSans_Text };
		TextPrefabs.All = new SerializableAutomaticReference<TextMeshProUGUI>[5] { TextPrefabs.Commissioner_Commissioner_Text, TextPrefabs.Fonarto_Fonarto_Text, TextPrefabs.Main_Font_Main_Text, TextPrefabs.Monospace_Font_Monospace_Text, TextPrefabs.Noto_Sans_NotoSans_Text };
	}

	public void RegisterLua()
	{
		UserData.RegisterType<FontDataGenerated>();
		UserData.RegisterType<TextPrefabsContainer>();
		UserData.RegisterType<TextMeshProUGUI>();
		UserData.RegisterType<SerializableAutomaticReference<TextMeshProUGUI>>();
	}
}
