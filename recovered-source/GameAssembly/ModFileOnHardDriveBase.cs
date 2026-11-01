using System.IO;

public abstract class ModFileOnHardDriveBase : ModFileBase
{
	public override byte[] Data => File.ReadAllBytes(HardDrivePath);

	public override string StringData => File.ReadAllText(HardDrivePath);

	public abstract string HardDrivePath { get; }

	public override bool IsStreamingPreferred => true;

	public bool Exists()
	{
		return File.Exists(HardDrivePath);
	}

	public override long GetApproximateSizeInBytes()
	{
		return new FileInfo(HardDrivePath).Length;
	}

	public override Stream OpenReadStream()
	{
		return File.OpenRead(HardDrivePath);
	}
}
