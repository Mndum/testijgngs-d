using System.IO;
using System.IO.Compression;

public sealed class RawModFileInZip : ModFileBase
{
	public ZipArchiveEntry ZipEntry { get; private set; }

	public override byte[] Data
	{
		get
		{
			using Stream stream = OpenReadStream();
			return stream.ReadAllBytes();
		}
	}

	public override string StringData
	{
		get
		{
			using Stream stream = OpenReadStream();
			using StreamReader streamReader = new StreamReader(stream);
			return streamReader.ReadToEnd();
		}
	}

	public override bool IsStreamingPreferred => true;

	public RawModFileInZip(ZipArchiveEntry zipEntry)
	{
		ZipEntry = zipEntry;
		NormalizedPath = zipEntry.FullName;
	}

	public override long GetApproximateSizeInBytes()
	{
		return ZipEntry.Length;
	}

	public override Stream OpenReadStream()
	{
		return ZipEntry.Open();
	}
}
