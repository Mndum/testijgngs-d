using System;

[Serializable]
public class RequirementContainer : PolymorphicSerializableContainer<RequirementContainer, Requirement>
{
	public static readonly (Type type, string prefix)[] _polymorphicTypes = new (Type, string)[12]
	{
		(typeof(OrRequirement), "OR"),
		(typeof(AndRequirement), "AND"),
		(typeof(InOrderRequirement), "IORD"),
		(typeof(TimeConstraintRequirement), "TC"),
		(typeof(AchieveInOneStream), "AIOS"),
		(typeof(StreamForXMinutesRequirement), "SFXM"),
		(typeof(WhileStreamViewershipRequirement), "WSVR"),
		(typeof(WhileStreamTimeOfDayRequirement), "WSTOD"),
		(typeof(WhileStreamPositionRequirement), "WSPR"),
		(typeof(WhileWearingItemRequirement), "WWI"),
		(typeof(WhileWearingMerchRequirement), "WWMR"),
		(typeof(StreamEventRequirement), "SER")
	};

	public override (Type type, string prefix)[] PolymorphicTypes => _polymorphicTypes;

	public RequirementContainer(Requirement deserializedObject)
		: base(deserializedObject)
	{
	}

	public static implicit operator RequirementContainer(Requirement obj)
	{
		return new RequirementContainer(obj);
	}
}
