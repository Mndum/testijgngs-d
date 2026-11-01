using System.Text;

public sealed class ModFileInMemory : ModFileBase
{
	private byte[] innerData;

	public override bool IsStreamingPreferred => false;

	public override byte[] Data => innerData;

	public ModFileInMemory(string normalizedPath, string data)
	{
		NormalizedPath = normalizedPath;
		innerData = Encoding.UTF8.GetBytes(data);
	}

	public ModFileInMemory(string normalizedPath, byte[] data)
	{
		NormalizedPath = normalizedPath;
		innerData = data;
	}

	public override long GetApproximateSizeInBytes()
	{
		return innerData.Length;
	}
}
