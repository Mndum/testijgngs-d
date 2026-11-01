using System;
using UnityEngine;

[Serializable]
public class JoinUsBlogManager : GameVariablesSubClass
{
	public enum LoginStatusEnum
	{
		NotLoggedIn,
		LoggedOutForSafety,
		LoggedIn
	}

	[SerializeField]
	private PrefabManagerDataOneToOneLazy<string, JoinUsBlog, JoinUsSaveDataBlog>.Serialized _blogSaves = new PrefabManagerDataOneToOneLazy<string, JoinUsBlog, JoinUsSaveDataBlog>.Serialized((string x) => new JoinUsSaveDataBlog(x));

	public bool TryGetSaveData(JoinUsBlog blog, out JoinUsSaveDataBlog data)
	{
		return _blogSaves.TryGet(blog, out data);
	}

	public JoinUsSaveDataBlog GetSaveData(JoinUsBlog blog)
	{
		return _blogSaves.Get(blog);
	}

	public bool TryGetItemSaveData(JoinUsBlogItem item, out JoinUsItemSaveData saveData)
	{
		if (!TryGetSaveData(item.MainBlog, out var data))
		{
			saveData = null;
			return false;
		}
		return data.TryGetSaveData(item, out saveData);
	}

	public void ClearUnnecessary()
	{
		foreach (JoinUsSaveDataBlog allSafe in _blogSaves.AllSaves)
		{
			allSafe.ClearUnnecessary();
		}
	}

	public void Logout()
	{
		RelatedGameVariables.RemoveFlag("joinusebloglogin");
	}

	public void Login()
	{
		RelatedGameVariables.AddFlag("joinusebloglogin");
	}

	public LoginStatusEnum GetLoginStatus()
	{
		if (RelatedGameVariables.TryGetFlag("joinusebloglogin", out var flag))
		{
			if (GameVariables.HappenedRecently(flag.LastTimeAdded, RelatedGameVariables.time))
			{
				return LoginStatusEnum.LoggedIn;
			}
			return LoginStatusEnum.LoggedOutForSafety;
		}
		return LoginStatusEnum.NotLoggedIn;
	}

	public bool CanEnterWebsite()
	{
		if (RelatedGameVariables.CheckFlag("joinusemail3"))
		{
			return !RelatedGameVariables.CheckFlag("joinus6mission_bailoutend");
		}
		return false;
	}

	public float GetPlanProgress()
	{
		GameVariables relatedGameVariables = RelatedGameVariables;
		if (!relatedGameVariables.CheckFlag("joinusemail3"))
		{
			return 0f;
		}
		if (!relatedGameVariables.CheckFlag("joinus4"))
		{
			return 0.5f;
		}
		if (!relatedGameVariables.CheckFlag("joinus5"))
		{
			return 0.3f;
		}
		if (!relatedGameVariables.CheckFlag("joinus6"))
		{
			return 0.3f;
		}
		bool flag = relatedGameVariables.CheckFlag("joinus6mission_bigdaddychosen");
		bool flag2 = relatedGameVariables.CheckFlag("joinus6mission_richardchosen");
		if (!flag && !flag2)
		{
			return 0.8f;
		}
		if (flag)
		{
			if (!relatedGameVariables.CheckFlag("joinus7_bigdaddy4"))
			{
				return 1f;
			}
			if (!relatedGameVariables.CheckFlag("joinus7_bigdaddy8_sidewithadmin"))
			{
				return 0.1f;
			}
			return 0.2f;
		}
		if (!relatedGameVariables.CheckFlag("joinus7_richard4email"))
		{
			return 1f;
		}
		return 0.1f;
	}
}
