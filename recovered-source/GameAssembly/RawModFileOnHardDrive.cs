public sealed class RawModFileOnHardDrive : ModFileOnHardDriveBase
{
	public string Directory { get; private set; }

	public override string HardDrivePath => Directory + NormalizedPath;

	public RawModFileOnHardDrive(string normalizedPath, string directory)
	{
		NormalizedPath = normalizedPath;
		Directory = directory;
	}
}
