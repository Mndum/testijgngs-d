using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "OtherDataGenerated", menuName = "Automated/OtherDataGenerated", order = 1)]
public class OtherDataGenerated : SingletonScriptableObject<OtherDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class ShowerParticleSystemsContainer
	{
		public SerializableAutomaticReference<ParticleSystem> Soap;

		public SerializableAutomaticReference<ParticleSystem> HideBrushSoapParticleSystem;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<ParticleSystem>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<ParticleSystem>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<ParticleSystem>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<ParticleSystem>>();

		[NonSerialized]
		public SerializableAutomaticReference<ParticleSystem>[] All = Array.Empty<SerializableAutomaticReference<ParticleSystem>>();
	}

	[Serializable]
	public class ShowerParticleSystemsGroupsContainer
	{
		public SerializableAutomaticReference<ParticleSystemGroup> FlyOffSoapParticleSystem;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<ParticleSystemGroup>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<ParticleSystemGroup>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<ParticleSystemGroup>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<ParticleSystemGroup>>();

		[NonSerialized]
		public SerializableAutomaticReference<ParticleSystemGroup>[] All = Array.Empty<SerializableAutomaticReference<ParticleSystemGroup>>();
	}

	public ShowerParticleSystemsContainer ShowerParticleSystems;

	public ShowerParticleSystemsGroupsContainer ShowerParticleSystemsGroups;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		ShowerParticleSystems.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<ParticleSystem>>
		{
			["FlyOffSoapParticleSystem"] = ShowerParticleSystems.Soap,
			["HideBrushSoapParticleSystem"] = ShowerParticleSystems.HideBrushSoapParticleSystem
		};
		ShowerParticleSystems.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<ParticleSystem>>
		{
			[("FlyOffSoapParticleSystem", "Soap")] = ShowerParticleSystems.Soap,
			[("HideBrushSoapParticleSystem", "HideBrushSoapParticleSystem")] = ShowerParticleSystems.HideBrushSoapParticleSystem
		};
		ShowerParticleSystems.All = new SerializableAutomaticReference<ParticleSystem>[2] { ShowerParticleSystems.Soap, ShowerParticleSystems.HideBrushSoapParticleSystem };
		ShowerParticleSystemsGroups.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<ParticleSystemGroup>> { ["FlyOffSoapParticleSystem"] = ShowerParticleSystemsGroups.FlyOffSoapParticleSystem };
		ShowerParticleSystemsGroups.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<ParticleSystemGroup>> { [("FlyOffSoapParticleSystem", "FlyOffSoapParticleSystem")] = ShowerParticleSystemsGroups.FlyOffSoapParticleSystem };
		ShowerParticleSystemsGroups.All = new SerializableAutomaticReference<ParticleSystemGroup>[1] { ShowerParticleSystemsGroups.FlyOffSoapParticleSystem };
	}

	public void RegisterLua()
	{
		UserData.RegisterType<OtherDataGenerated>();
		UserData.RegisterType<ShowerParticleSystemsContainer>();
		UserData.RegisterType<ParticleSystem>();
		UserData.RegisterType<SerializableAutomaticReference<ParticleSystem>>();
		UserData.RegisterType<ShowerParticleSystemsGroupsContainer>();
		UserData.RegisterType<ParticleSystemGroup>();
		UserData.RegisterType<SerializableAutomaticReference<ParticleSystemGroup>>();
	}
}
