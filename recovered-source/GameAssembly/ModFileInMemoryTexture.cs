using UnityEngine;

public sealed class ModFileInMemoryTexture : ModFileBase
{
	public Texture2D Texture2D { get; private set; }

	public override byte[] Data => Texture2D.DeCompressAndConvertToPng().PngData;

	public override bool IsStreamingPreferred => false;

	public ModFileInMemoryTexture(string normalizedPath, Texture2D data)
	{
		NormalizedPath = normalizedPath;
		Texture2D = data;
	}

	public override long GetApproximateSizeInBytes()
	{
		return Texture2D.width * Texture2D.height * 4;
	}

	public override Texture2D GetAsTexture2D()
	{
		return Texture2D;
	}

	public override ModFileBase CopyToMemory(string newPath)
	{
		return new ModFileInMemoryTexture(newPath, Texture2D);
	}
}
