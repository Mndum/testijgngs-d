public sealed class WorkingDirectoryModFileOnHardDrive : ModFileOnHardDriveBase
{
	public override string HardDrivePath => Mod.WorkingDirectory + NormalizedPath;

	public Mod Mod { get; private set; }

	public WorkingDirectoryModFileOnHardDrive(string normalizedPath, Mod mod)
	{
		NormalizedPath = normalizedPath;
		Mod = mod;
	}
}
