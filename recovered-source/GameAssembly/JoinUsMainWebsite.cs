using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinUsMainWebsite : JoinUsWebsiteBase
{
	[SerializeField]
	private TextMeshProUGUI _leftRecipe;

	[SerializeField]
	private TextMeshProUGUI _rightRecipe;

	private string GetRecipe(int index)
	{
		return index switch
		{
			1 => LOC.UI.Blog_ChiliRecipe2, 
			2 => LOC.UI.Blog_ChiliRecipe3, 
			_ => LOC.UI.Blog_ChiliRecipe1, 
		};
	}

	private void FillRecipes()
	{
		List<int> list;
		using (MyCollectionPool<int>.GetList(out list))
		{
			while (list.Count < 2)
			{
				int item = Random.Range(0, 3);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			_leftRecipe.SetText(GetRecipe(list[0]));
			_rightRecipe.SetText(GetRecipe(list[1]));
		}
	}

	public override IEnumerator Open()
	{
		if (GameVariables.Current.joinUsBlogManager.GetLoginStatus() != JoinUsBlogManager.LoginStatusEnum.LoggedIn)
		{
			BrowserPopup.ChangeWebsite("patriciascookingblog.food/login");
			yield break;
		}
		SingletonMonoBehaviour<SoundSingleton>.Instance.PlayNextSong();
		FillRecipes();
		BrowserPopup.FinishLoading();
	}
}
