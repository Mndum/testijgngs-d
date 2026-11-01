using MoonSharp.Interpreter;

public class R
{
	public static FontDataGenerated.TextPrefabsContainer TextPrefabs => SingletonScriptableObject<FontDataGenerated>.Instance.TextPrefabs;

	public static MusicDataGenerated.MusicGroupsContainer MusicGroups => SingletonScriptableObject<MusicDataGenerated>.Instance.MusicGroups;

	public static MusicDataGenerated.MusicContainer Music => SingletonScriptableObject<MusicDataGenerated>.Instance.Music;

	public static MusicDataGenerated.ArtistsContainer Artists => SingletonScriptableObject<MusicDataGenerated>.Instance.Artists;

	public static MusicDataGenerated.MidiContainer Midi => SingletonScriptableObject<MusicDataGenerated>.Instance.Midi;

	public static OtherDataGenerated.ShowerParticleSystemsContainer ShowerParticleSystems => SingletonScriptableObject<OtherDataGenerated>.Instance.ShowerParticleSystems;

	public static OtherDataGenerated.ShowerParticleSystemsGroupsContainer ShowerParticleSystemsGroups => SingletonScriptableObject<OtherDataGenerated>.Instance.ShowerParticleSystemsGroups;

	public static ShaderDataGenerated.Live2DShadersContainer Live2DShaders => SingletonScriptableObject<ShaderDataGenerated>.Instance.Live2DShaders;

	public static SoundDataGenerated.SoundContainer Sound => SingletonScriptableObject<SoundDataGenerated>.Instance.Sound;

	public static SpriteTextureDataGenerated.AltBackgroundsContainer AltBackgrounds => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.AltBackgrounds;

	public static SpriteTextureDataGenerated.FishingBackgroundsContainer FishingBackgrounds => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.FishingBackgrounds;

	public static SpriteTextureDataGenerated.PartnerLogosContainer PartnerLogos => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.PartnerLogos;

	public static SpriteTextureDataGenerated.OtherLogosContainer OtherLogos => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.OtherLogos;

	public static SpriteTextureDataGenerated.IconsContainer Icons => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.Icons;

	public static SpriteTextureDataGenerated.CardsMainContainer CardsMain => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.CardsMain;

	public static SpriteTextureDataGenerated.CardsCasinoContainer CardsCasino => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.CardsCasino;

	public static SpriteTextureDataGenerated.PcIconsContainer PcIcons => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.PcIcons;

	public static SpriteTextureDataGenerated.NewsImagesContainer NewsImages => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.NewsImages;

	public static SpriteTextureDataGenerated.GameLogoContainer GameLogo => SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.GameLogo;

	public static TextDataGenerated.CreditsTextContainer CreditsText => SingletonScriptableObject<TextDataGenerated>.Instance.CreditsText;

	public static TextDataGenerated.OtherTextContainer OtherText => SingletonScriptableObject<TextDataGenerated>.Instance.OtherText;

	public static void RegisterLua(Table globalsTable)
	{
		UserData.RegisterType<R>();
		globalsTable["R"] = UserData.CreateStatic<R>();
		SingletonScriptableObject<FontDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<MusicDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<OtherDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<ShaderDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<SoundDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<SpriteTextureDataGenerated>.Instance.RegisterLua();
		SingletonScriptableObject<TextDataGenerated>.Instance.RegisterLua();
	}
}
