using System;
using UnityEngine;

[Serializable]
public class JoinUsSaveDataBlog : IKey<string>
{
	[SerializeField]
	private string _key;

	[SerializeField]
	private SerializableDictionary<int, JoinUsItemSaveData> _data = new SerializableDictionary<int, JoinUsItemSaveData>();

	public string Key => _key;

	public JoinUsSaveDataBlog(string key)
	{
		_key = key;
	}

	public bool TryGetSaveData(JoinUsBlogItem blog, out JoinUsItemSaveData data)
	{
		return _data.TryGetValue(blog.Index, out data);
	}

	public void ClearUnnecessary()
	{
		_data.PooledListDictionaryRemoveAll((int key, JoinUsItemSaveData value) => value.CanBeDeleted);
	}

	private JoinUsItemSaveData GetSaveData(JoinUsBlogItem blog)
	{
		if (_data.TryGetValue(blog.Index, out var value))
		{
			return value;
		}
		return _data[blog.Index] = new JoinUsItemSaveData();
	}

	public void UpvoteClicked(JoinUsBlogItem blog)
	{
		JoinUsItemSaveData saveData = GetSaveData(blog);
		saveData.SetInteraction((saveData.InteractionType != JoinUsItemSaveData.InteractionTypeEnum.Upvote) ? JoinUsItemSaveData.InteractionTypeEnum.Upvote : JoinUsItemSaveData.InteractionTypeEnum.Nothing);
		if (saveData.CanBeDeleted)
		{
			_data.Remove(blog.Index);
		}
	}

	public void DownVoteClicked(JoinUsBlogItem blog)
	{
		JoinUsItemSaveData saveData = GetSaveData(blog);
		saveData.SetInteraction((saveData.InteractionType != JoinUsItemSaveData.InteractionTypeEnum.DownVote) ? JoinUsItemSaveData.InteractionTypeEnum.DownVote : JoinUsItemSaveData.InteractionTypeEnum.Nothing);
		if (saveData.CanBeDeleted)
		{
			_data.Remove(blog.Index);
		}
	}
}
