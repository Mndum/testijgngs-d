using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SpecialTextureHolder
{
	public List<PackedTexturesContainer> textures;

	public bool AllEmpty()
	{
		return textures.Count == 0;
	}

	public IEnumerable<PackedTexturesContainer> IterateValidTextures()
	{
		return textures.Where((PackedTexturesContainer x) => x != null);
	}

	public Result Check()
	{
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			bool flag = false;
			for (int i = 0; i < textures.Count; i++)
			{
				if (textures[i] == null)
				{
					flag = true;
					stringBuilder.AppendLine($"Texture {i} is null!");
				}
			}
			return Result.CreateFromBool(!flag, stringBuilder.ToString());
		}
	}
}
