using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParsedVersion : IEquatable<ParsedVersion>
{
	public struct VersionComparisonStruct
	{
		public int MajorDifference;

		public int MinorDifference;

		public int PatchDifference;

		public bool IsNewer => VersionDifferenceDirection() < 0;

		public bool IsOlder => VersionDifferenceDirection() > 0;

		public bool IsExactlyTheSame => VersionDifferenceDirection() == 0;

		public int VersionDifferenceDirection()
		{
			if (MajorDifference != 0)
			{
				return MajorDifference.CompareTo(0);
			}
			if (MinorDifference != 0)
			{
				return MinorDifference.CompareTo(0);
			}
			return PatchDifference.CompareTo(0);
		}

		public string GetLocalizedDifferenceDescription()
		{
			if (MajorDifference > 0)
			{
				return LOC.UI.Mods_MajorVersionBehind(MajorDifference);
			}
			if (MajorDifference < 0)
			{
				return LOC.UI.Mods_MajorVersionAhead(-MajorDifference);
			}
			if (MinorDifference > 0)
			{
				return LOC.UI.Mods_MinorVersionBehind(MinorDifference);
			}
			if (MinorDifference < 0)
			{
				return LOC.UI.Mods_MinorVersionAhead(-MinorDifference);
			}
			if (PatchDifference > 0)
			{
				return LOC.UI.Mods_PatchVersionBehind(PatchDifference);
			}
			if (PatchDifference < 0)
			{
				return LOC.UI.Mods_PatchVersionAhead(-PatchDifference);
			}
			return LOC.UI.Mods_ExactlySameVersion;
		}
	}

	private static Regex _removeNonNumericRegex = new Regex("[^0-9.]");

	public readonly int MajorVersion;

	public readonly int MinorVersion;

	public readonly int PatchVersion;

	public readonly string StringRepresentation;

	private static ParsedVersion _currentGameVersion = null;

	public static ParsedVersion Version0_75 = CreateFromString("0.75 beta");

	public static ParsedVersion Version0_80 = CreateFromString("0.80 beta");

	public static ParsedVersion Version0_84 = new ParsedVersion(0, 84, 0, "0.85 beta");

	public static ParsedVersion Version0_85 = CreateFromString("0.85.0");

	public static ParsedVersion Version0_85_1 = CreateFromString("0.85.1");

	public static ParsedVersion Version0_85_69 = CreateFromString("0.85.69");

	public static ParsedVersion Version0_86_0 = CreateFromString("0.86.0");

	public static ParsedVersion Version0_86_1 = CreateFromString("0.86.1");

	public static ParsedVersion Version0_86_2 = CreateFromString("0.86.2");

	public static ParsedVersion Version0_86_3 = CreateFromString("0.86.3");

	public static ParsedVersion Version0_86_4 = CreateFromString("0.86.4");

	public static ParsedVersion Version0_87_0 = CreateFromString("0.87.0");

	public static ParsedVersion Version0_87_1 = CreateFromString("0.87.1");

	public static ParsedVersion Version0_87_2 = CreateFromString("0.87.2");

	public static ParsedVersion Version0_87_3 = CreateFromString("0.87.3");

	public static ParsedVersion Version0_87_4 = CreateFromString("0.87.4");

	public static ParsedVersion Version0_87_5 = CreateFromString("0.87.5");

	public static ParsedVersion Version0_87_6 = CreateFromString("0.87.6");

	public static ParsedVersion Version0_87_7 = CreateFromString("0.87.7");

	public static ParsedVersion Version0_87_8 = CreateFromString("0.87.8");

	public static ParsedVersion Version0_90_0 = CreateFromString("0.90.0");

	public static ParsedVersion Version0_90_1 = CreateFromString("0.90.1");

	public static ParsedVersion Version0_90_2 = CreateFromString("0.90.2");

	public static ParsedVersion Version0_90_3 = CreateFromString("0.90.3");

	public static ParsedVersion Version0_90_4 = CreateFromString("0.90.4");

	public static ParsedVersion Version0_90_5 = CreateFromString("0.90.5");

	public static ParsedVersion Version0_90_6 = CreateFromString("0.90.6");

	public static ParsedVersion Version0_90_7 = CreateFromString("0.90.7");

	public static ParsedVersion Version0_90_8 = CreateFromString("0.90.8");

	public static ParsedVersion Version0_90_9 = CreateFromString("0.90.9");

	public static ParsedVersion Version0_90_10 = CreateFromString("0.90.10");

	public static ParsedVersion Version0_90_11 = CreateFromString("0.90.11");

	public static ParsedVersion Version0_90_12 = CreateFromString("0.90.12");

	public static ParsedVersion Version0_90_13 = CreateFromString("0.90.13");

	public static ParsedVersion Version0_90_14 = CreateFromString("0.90.14");

	public static ParsedVersion Version0_90_15 = CreateFromString("0.90.15");

	public static ParsedVersion Version0_90_16 = CreateFromString("0.90.16");

	public static ParsedVersion Version0_95_1 = CreateFromString("0.95.1");

	public static ParsedVersion Version0_95_2 = CreateFromString("0.95.2");

	public static ParsedVersion Version0_95_3 = CreateFromString("0.95.3");

	public static ParsedVersion Version0_95_4 = CreateFromString("0.95.4");

	public static ParsedVersion Version0_95_5 = CreateFromString("0.95.5");

	public static ParsedVersion Version0_95_6 = CreateFromString("0.95.6");

	public static ParsedVersion Version0_95_7 = CreateFromString("0.95.7");

	public static ParsedVersion Version0_95_8 = CreateFromString("0.95.8");

	public static ParsedVersion Version0_95_9 = CreateFromString("0.95.9");

	public static ParsedVersion Version0_95_10 = CreateFromString("0.95.10");

	public static ParsedVersion Version0_95_11 = CreateFromString("0.95.11");

	public static ParsedVersion Version0_95_12 = CreateFromString("0.95.12");

	public static ParsedVersion Version0_95_13 = CreateFromString("0.95.13");

	public static ParsedVersion Version0_95_14 = CreateFromString("0.95.14");

	public static ParsedVersion Version0_95_15 = CreateFromString("0.95.15");

	public static ParsedVersion Version0_96_1 = CreateFromString("0.96.1");

	public static ParsedVersion Version0_96_2 = CreateFromString("0.96.2");

	public static ParsedVersion Version0_96_3 = CreateFromString("0.96.3");

	public static ParsedVersion Version0_96_4 = CreateFromString("0.96.4");

	public static ParsedVersion Version0_96_5 = CreateFromString("0.96.5");

	public static ParsedVersion Version0_96_6 = CreateFromString("0.96.6");

	public static ParsedVersion Version0_96_7 = CreateFromString("0.96.7");

	public static ParsedVersion Version0_97_1 = CreateFromString("0.97.1");

	public static ParsedVersion Version0_97_2 = CreateFromString("0.97.2");

	public static ParsedVersion Version0_97_3 = CreateFromString("0.97.3");

	public static ParsedVersion Version0_97_4 = CreateFromString("0.97.4");

	public static ParsedVersion Version0_97_5 = CreateFromString("0.97.5");

	public static ParsedVersion Version0_97_10 = CreateFromString("0.97.10");

	public static ParsedVersion Version0_97_11 = CreateFromString("0.97.11");

	public static ParsedVersion Version0_97_12 = CreateFromString("0.97.12");

	public static ParsedVersion ModDefaultVersion = CreateFromString("1.0.0");

	public static ParsedVersion FailedParsingVersion = CreateFromString("0.0.0");

	public static ParsedVersion CurrentGameVersion => _currentGameVersion ?? (_currentGameVersion = CreateFromString(Application.version));

	public static string CurrentGameVersionString => Application.version;

	private ParsedVersion(int majorVersion, int minorVersion, int patchVersion)
	{
		MajorVersion = majorVersion;
		MinorVersion = minorVersion;
		PatchVersion = patchVersion;
		StringRepresentation = GenerateStringRepresentation();
	}

	public ParsedVersion(int majorVersion, int minorVersion, int patchVersion, string stringRepresentation)
	{
		MajorVersion = majorVersion;
		MinorVersion = minorVersion;
		PatchVersion = patchVersion;
		StringRepresentation = stringRepresentation;
	}

	public static ParsedVersion CreateInstance(int majorVersion, int minorVersion, int patchVersion)
	{
		return new ParsedVersion(majorVersion, minorVersion, patchVersion);
	}

	public static ParsedVersion CreateFromString(string versionString)
	{
		if (string.IsNullOrWhiteSpace(versionString))
		{
			return new ParsedVersion(0, 0, 0);
		}
		string[] array = _removeNonNumericRegex.Replace(versionString, "").Split('.');
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = array[i].Trim();
		}
		if (array.Length == 0 || !int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
		{
			result = 0;
		}
		if (array.Length <= 1 || !int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var result2))
		{
			result2 = 0;
		}
		if (array.Length <= 2 || !int.TryParse(array[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var result3))
		{
			result3 = 0;
		}
		return new ParsedVersion(result, result2, result3, versionString);
	}

	public bool Equals(ParsedVersion other)
	{
		if (other == null)
		{
			return false;
		}
		if (this == other)
		{
			return true;
		}
		return CompareWith(other).IsExactlyTheSame;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Equals((ParsedVersion)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(MajorVersion, MinorVersion, PatchVersion);
	}

	public VersionComparisonStruct CompareWith(ParsedVersion target)
	{
		return new VersionComparisonStruct
		{
			MajorDifference = target.MajorVersion - MajorVersion,
			MinorDifference = target.MinorVersion - MinorVersion,
			PatchDifference = target.PatchVersion - PatchVersion
		};
	}

	public bool IsExactlyTheSame(ParsedVersion parsedVersion)
	{
		return CompareWith(parsedVersion).IsExactlyTheSame;
	}

	public bool IsOlder(ParsedVersion parsedVersion)
	{
		return CompareWith(parsedVersion).IsOlder;
	}

	public bool IsNewer(ParsedVersion parsedVersion)
	{
		return CompareWith(parsedVersion).IsNewer;
	}

	public bool IsNewerThanGameVersion()
	{
		return IsNewer(CurrentGameVersion);
	}

	public bool IsOutdated()
	{
		return IsOlder(CurrentGameVersion);
	}

	public string GenerateStringRepresentation()
	{
		return $"{MajorVersion}.{MinorVersion}.{PatchVersion}";
	}

	public override string ToString()
	{
		return StringRepresentation;
	}
}
