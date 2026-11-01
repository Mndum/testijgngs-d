using System.IO;
using System.Text;
using NVorbis;
using UnityEngine;

public abstract class ModFileBase
{
	protected string _normalizedPath;

	public abstract byte[] Data { get; }

	public abstract bool IsStreamingPreferred { get; }

	public string Filename => Path.GetFileName(NormalizedPath);

	public virtual string StringData => Encoding.UTF8.GetString(Data);

	public string NormalizedPath
	{
		get
		{
			return _normalizedPath;
		}
		protected set
		{
			if (!Path.IsPathRooted(value))
			{
				value = "/" + value;
			}
			_normalizedPath = value;
		}
	}

	public virtual Texture2D GetAsTexture2D()
	{
		Texture2D texture2D = ImageUtility.LoadTexture(Data);
		texture2D.PremultiplyTextureRuntime();
		return texture2D;
	}

	public virtual Stream OpenReadStream()
	{
		return new MemoryStream(Data);
	}

	public virtual AudioClip LoadAsAudioClip()
	{
		using VorbisReader vorbisReader = new VorbisReader(OpenReadStream());
		long num = vorbisReader.TotalSamples * vorbisReader.Channels;
		float[] array = new float[num];
		int num2;
		for (int i = 0; i < num; i += num2)
		{
			num2 = vorbisReader.ReadSamples(array, i, (int)(num - i));
			if (num2 <= 0)
			{
				break;
			}
		}
		AudioClip audioClip = AudioClip.Create(Filename, (int)(num / vorbisReader.Channels), vorbisReader.Channels, vorbisReader.SampleRate, stream: false);
		audioClip.SetData(array, 0);
		return audioClip;
	}

	public abstract long GetApproximateSizeInBytes();

	public virtual ModFileBase CopyToMemory(string newPath)
	{
		return new ModFileInMemory(newPath, Data);
	}
}
