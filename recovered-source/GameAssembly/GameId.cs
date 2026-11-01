using System;
using GameItems;
using UnityEngine;

[Serializable]
public struct GameId : IComparable<GameId>, LuaUtilities.ILuaTransferable
{
	[SerializeField]
	private SerializableGuid _guid;

	[SerializeField]
	private int _id;

	public static char Delimiter = '+';

	public bool IsVanilla => _guid.RealGuid == Guid.Empty;

	public int Id => _id;

	public Guid Guid => _guid;

	public bool IsModded => !IsVanilla;

	private GameId(SerializableGuid guid, int id)
	{
		_guid = guid;
		_id = id;
	}

	public static implicit operator GameId(int id)
	{
		return CreateVanilla(id);
	}

	public static GameId CreateVanilla(ItemEnum id)
	{
		return new GameId(Guid.Empty, (int)id);
	}

	public static GameId CreateVanilla(CookingSongEnumGI id)
	{
		return new GameId(Guid.Empty, (int)id);
	}

	public static GameId CreateVanilla(FishingLocationEnumGI id)
	{
		return new GameId(Guid.Empty, (int)id);
	}

	public static GameId CreateVanilla(ItemModifierEnumGI id)
	{
		return new GameId(Guid.Empty, (int)id);
	}

	public static GameId CreateVanilla(int id)
	{
		return new GameId(Guid.Empty, id);
	}

	public static GameId CreateModded(Mod mod, int id)
	{
		return new GameId(mod.MainModInfoFile.guid, id);
	}

	public static GameId CreateGeneric(Guid guid, int id)
	{
		return new GameId(guid, id);
	}

	public static bool operator ==(GameId b1, GameId b2)
	{
		return b1.Equals(b2);
	}

	public static bool operator !=(GameId b1, GameId b2)
	{
		return !(b1 == b2);
	}

	public override bool Equals(object obj)
	{
		if (obj is GameId gameId)
		{
			if (Guid == gameId.Guid)
			{
				return Id == gameId.Id;
			}
			return false;
		}
		return false;
	}

	public bool Equals(GameId b2)
	{
		if (Guid == b2.Guid)
		{
			return Id == b2.Id;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return _id;
	}

	public override string ToString()
	{
		if (IsVanilla)
		{
			return $"Vanilla Id:{Id}";
		}
		return $"Mod: {Guid} Id:{Id}";
	}

	public string ToLuaString()
	{
		if (IsVanilla)
		{
			return "GameId.CreateVanilla(" + Id.ToLuaString() + ")";
		}
		return "GameId.CreateGeneric(" + Guid.ToLuaString() + "," + Id.ToLuaString() + ")";
	}

	public int CompareTo(GameId other)
	{
		int num = _guid.RealGuid.CompareTo(other._guid.RealGuid);
		if (num != 0)
		{
			return num;
		}
		return _id.CompareTo(other._id);
	}

	public string ToShortString()
	{
		if (IsVanilla)
		{
			return Id.ToString();
		}
		return $"{Guid}{Delimiter}{Id}";
	}

	public static bool FromShortString(string s, out GameId gameId)
	{
		if (string.IsNullOrEmpty(s))
		{
			gameId = CreateVanilla(0);
			return false;
		}
		int num = s.IndexOf(Delimiter);
		Guid result2;
		int result3;
		if (num == -1)
		{
			if (int.TryParse(s, out var result))
			{
				gameId = CreateVanilla(result);
				return true;
			}
		}
		else if (Guid.TryParse(s.AsSpan(0, num), out result2) && int.TryParse(s.AsSpan(num + 1), out result3))
		{
			gameId = CreateGeneric(result2, result3);
			return true;
		}
		gameId = CreateVanilla(0);
		return false;
	}
}
