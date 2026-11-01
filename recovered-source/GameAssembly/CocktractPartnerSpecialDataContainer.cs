using System;

[Serializable]
public class CocktractPartnerSpecialDataContainer : PolymorphicSerializableContainer<CocktractPartnerSpecialDataContainer, CocktractPartnerSpecialData>
{
	public static readonly (Type type, string prefix)[] _polymorphicTypes = new (Type, string)[2]
	{
		(typeof(CocktractPartnerSpecialData), "CPSD"),
		(typeof(GenericCocktractSpecialData), "GCSD")
	};

	public override (Type type, string prefix)[] PolymorphicTypes => _polymorphicTypes;

	public CocktractPartnerSpecialDataContainer(CocktractPartnerSpecialData deserializedObject)
		: base(deserializedObject)
	{
	}

	public static implicit operator CocktractPartnerSpecialDataContainer(CocktractPartnerSpecialData obj)
	{
		return new CocktractPartnerSpecialDataContainer(obj);
	}
}
