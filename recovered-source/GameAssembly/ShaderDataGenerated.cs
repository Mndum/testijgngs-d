using System;
using System.Collections.Generic;
using IC.AutomaticReference;
using MoonSharp.Interpreter;
using UnityEngine;

[CreateAssetMenu(fileName = "ShaderDataGenerated", menuName = "Automated/ShaderDataGenerated", order = 1)]
public class ShaderDataGenerated : SingletonScriptableObject<ShaderDataGenerated>, ISerializationCallbackReceiver, IAutomaticReferenceContainer
{
	[Serializable]
	public class Live2DShadersContainer
	{
		public SerializableAutomaticReference<Shader> MovingTexture;

		public SerializableAutomaticReference<Shader> Rainbow;

		public SerializableAutomaticReference<Shader> RayMarching;

		[NonSerialized]
		public Dictionary<string, SerializableAutomaticReference<Shader>> RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Shader>>();

		[NonSerialized]
		public Dictionary<(string path, string name), SerializableAutomaticReference<Shader>> RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Shader>>();

		[NonSerialized]
		public SerializableAutomaticReference<Shader>[] All = Array.Empty<SerializableAutomaticReference<Shader>>();
	}

	public Live2DShadersContainer Live2DShaders;

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		Live2DShaders.RelativePathToFirst = new Dictionary<string, SerializableAutomaticReference<Shader>>
		{
			["MovingTexture"] = Live2DShaders.MovingTexture,
			["Rainbow"] = Live2DShaders.Rainbow,
			["RayMarching"] = Live2DShaders.RayMarching
		};
		Live2DShaders.RelativePathAndNameToReference = new Dictionary<(string, string), SerializableAutomaticReference<Shader>>
		{
			[("MovingTexture", "Live2D Cubism/Unlit(Moving texture effect)")] = Live2DShaders.MovingTexture,
			[("Rainbow", "Live2D Cubism/Unlit(Rainbow effect)")] = Live2DShaders.Rainbow,
			[("RayMarching", "Live2D Cubism/Raymarching")] = Live2DShaders.RayMarching
		};
		Live2DShaders.All = new SerializableAutomaticReference<Shader>[3] { Live2DShaders.MovingTexture, Live2DShaders.Rainbow, Live2DShaders.RayMarching };
	}

	public void RegisterLua()
	{
		UserData.RegisterType<ShaderDataGenerated>();
		UserData.RegisterType<Live2DShadersContainer>();
		UserData.RegisterType<Shader>();
		UserData.RegisterType<SerializableAutomaticReference<Shader>>();
	}
}
