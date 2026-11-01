using System;
using IC.AutomaticReference;

[Serializable]
public class WebsiteSerializableAsset
{
	public SerializableAutomaticReference<Website> WebsiteReference;

	public GameUrl GameUrl;

	public bool IsListed;
}
