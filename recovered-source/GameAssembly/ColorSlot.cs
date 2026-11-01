using System;
using UnityEngine;

public class ColorSlot : LuaUtilities.ILuaTransferable, ICloneable<ColorSlot>, ICloneable
{
	public string Name { get; set; }

	public ColorPaletteManager.ColorPalette Palette { get; set; }

	public ColorSlotTransparencyEnum Transparency { get; set; }

	public ColorSlot(string name, ColorPaletteManager.ColorPalette palette = null, ColorSlotTransparencyEnum colorSlotTransparency = ColorSlotTransparencyEnum.Opaque)
	{
		Name = name;
		Palette = palette ?? ColorPaletteManager.DefaultPartPalette;
		Transparency = colorSlotTransparency;
	}

	public static ColorSlot CreateInstance(string name, ColorPaletteManager.ColorPalette palette, ColorSlotTransparencyEnum colorSlotTransparency = ColorSlotTransparencyEnum.Opaque)
	{
		return new ColorSlot(name, palette, colorSlotTransparency);
	}

	public Color EnforceTransparency(Color color)
	{
		return Transparency switch
		{
			ColorSlotTransparencyEnum.TransparencyForced80percent => color.SetAlpha(0.8f), 
			ColorSlotTransparencyEnum.TransparencyForced50percent => color.SetAlpha(0.5f), 
			_ => color.SetAlpha(1f), 
		};
	}

	public string ToLuaString()
	{
		if (Transparency != ColorSlotTransparencyEnum.Opaque)
		{
			return "ColorSlot.CreateInstance(" + Name.ToLuaString() + ", ColorPaletteManager.GetColorPaletteByName(" + Palette.Id.ToLuaString() + "), " + Transparency.ToLuaString() + ")";
		}
		return "ColorSlot.CreateInstance(" + Name.ToLuaString() + ", ColorPaletteManager.GetColorPaletteByName(" + Palette.Id.ToLuaString() + "))";
	}

	public ColorSlot Clone()
	{
		return (ColorSlot)MemberwiseClone();
	}
}
