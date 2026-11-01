using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class ModStatusExtensions
{
	public static Color StatusToColor(ModStatus status)
	{
		return status switch
		{
			ModStatus.Not_Loaded => Color.gray, 
			ModStatus.Ready => Color.yellow, 
			ModStatus.Active => Color.green, 
			ModStatus.ActiveButOutdated => (Color)MyColor32Presets.DarkGreen, 
			ModStatus.ActiveButGameOutdated => (Color)new Color32(153, 50, 204, 255), 
			ModStatus.Failed => (Color)new Color32(153, 50, 204, 255), 
			_ => Color.red, 
		};
	}

	public static bool IsStatusActive(ModStatus status)
	{
		if (status != ModStatus.Active && status != ModStatus.ActiveButOutdated)
		{
			return status == ModStatus.ActiveButGameOutdated;
		}
		return true;
	}

	public static string StatusToInfo(ModStatus status)
	{
		return status switch
		{
			ModStatus.Exception => LOC.UI.Mods_Status_ExceptionDescription, 
			ModStatus.Not_Loaded => LOC.UI.Mods_Status_NotLoadedDescription, 
			ModStatus.Ready => LOC.UI.Mods_Status_ReadyDescription, 
			ModStatus.Active => LOC.UI.Mods_Status_ActiveDescription, 
			ModStatus.ActiveButOutdated => LOC.UI.Mods_Status_ActiveButOutdatedDescription, 
			ModStatus.ActiveButGameOutdated => LOC.UI.Mods_Status_ActiveButGameOutdatedDescription, 
			ModStatus.Failed => LOC.UI.Mods_Status_FailedDescription, 
			_ => "Unknown status", 
		};
	}

	public static string StatusToName(ModStatus status)
	{
		return status switch
		{
			ModStatus.Exception => LOC.UI.Mods_Status_Exception, 
			ModStatus.Not_Loaded => LOC.UI.Mods_Status_NotLoaded, 
			ModStatus.Ready => LOC.UI.Mods_Status_Ready, 
			ModStatus.Active => LOC.UI.Mods_Status_Active, 
			ModStatus.ActiveButOutdated => LOC.UI.Mods_Status_ActiveButOutdated, 
			ModStatus.ActiveButGameOutdated => LOC.UI.Mods_Status_ActiveButGameOutdated, 
			ModStatus.Failed => LOC.UI.Mods_Status_Failed, 
			_ => "Unknown status", 
		};
	}

	public static string GenerateStatusExplaination()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			foreach (ModStatus item in Enum.GetValues(typeof(ModStatus)).Cast<ModStatus>())
			{
				using (stringBuilder.StartColorTagUsing(StatusToColor(item)))
				{
					stringBuilder.Append(StatusToName(item));
				}
				stringBuilder.Append("-");
				stringBuilder.Append(StatusToInfo(item));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
	}
}
