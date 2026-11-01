using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameUrl
{
	[field: SerializeField]
	public string Domain { get; set; }

	[field: SerializeField]
	public List<string> Rest { get; set; }

	public string Assembled => string.Join("/", new string[1] { Domain }.Concat(Rest));

	public GameUrl()
	{
		Domain = "";
		Rest = new List<string>();
	}

	public GameUrl(string str)
	{
		FromString(str);
	}

	protected void FromString(string str)
	{
		string[] array = str.Split('/', '\\');
		Domain = array[0].ToLower();
		Rest = MyCollectionPool<string>.GetList();
		Rest.AddRange(array.Skip(1));
		int num = Rest.Count - 1;
		while (num >= 0 && string.IsNullOrWhiteSpace(Rest[num]))
		{
			Rest.RemoveAt(num);
			num--;
		}
	}

	public void ChangeTo(GameUrl other)
	{
		Domain = other.Domain;
		Rest = MyCollectionPool<string>.GetList();
		Rest.AddRange(other.Rest);
	}

	public bool Equals(GameUrl other)
	{
		if (!string.Equals(Domain, other.Domain, StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
		if (Rest.Count != other.Rest.Count)
		{
			return false;
		}
		for (int i = 0; i < Rest.Count; i++)
		{
			if (Rest[i] != other.Rest[i])
			{
				return false;
			}
		}
		return true;
	}

	public int IsSubDomain(GameUrl other)
	{
		if (!string.Equals(Domain, other.Domain, StringComparison.OrdinalIgnoreCase))
		{
			return 0;
		}
		if (other.Rest.Count < Rest.Count)
		{
			return 0;
		}
		for (int i = 0; i < Rest.Count; i++)
		{
			if (Rest[i] != other.Rest[i])
			{
				return 0;
			}
		}
		return Rest.Count;
	}
}
