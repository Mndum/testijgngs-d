using System.Text;
using TMPro;
using UnityEngine;

public class JoinUsItemScoreDisplay : MonoBehaviour
{
	[SerializeField]
	private BetterButton _upButton;

	[SerializeField]
	private TextMeshProUGUI _scoreText;

	[SerializeField]
	private BetterButton _downvoteButton;

	private JoinUsBlogItem _currentItem;

	private JoinUsItemSaveData.InteractionTypeEnum _currentInteraction;

	private void Awake()
	{
		_upButton.onClick.SetListener(UpVoteButtonClicked);
		_downvoteButton.onClick.SetListener(DownVoteButtonClicked);
	}

	private void UpVoteButtonClicked()
	{
		if (_currentItem != null)
		{
			GameVariables.Current.joinUsBlogManager.GetSaveData(_currentItem.MainBlog).UpvoteClicked(_currentItem);
			UpdateItem();
		}
	}

	private void DownVoteButtonClicked()
	{
		if (_currentItem != null)
		{
			GameVariables.Current.joinUsBlogManager.GetSaveData(_currentItem.MainBlog).DownVoteClicked(_currentItem);
			UpdateItem();
		}
	}

	private void UpdateButtonColors()
	{
		switch (_currentInteraction)
		{
		case JoinUsItemSaveData.InteractionTypeEnum.Upvote:
			_upButton.image.color = MyColor32Presets.Green;
			_downvoteButton.image.color = MyColor32Presets.MatrixGreen;
			break;
		case JoinUsItemSaveData.InteractionTypeEnum.Nothing:
			_upButton.image.color = MyColor32Presets.MatrixGreen;
			_downvoteButton.image.color = MyColor32Presets.MatrixGreen;
			break;
		case JoinUsItemSaveData.InteractionTypeEnum.DownVote:
			_upButton.image.color = MyColor32Presets.MatrixGreen;
			_downvoteButton.image.color = MyColor32Presets.Red;
			break;
		}
	}

	public void DisplayScore(JoinUsBlogItem blogItem)
	{
		_currentItem = blogItem;
		UpdateItem();
	}

	private void UpdateItem()
	{
		int num = _currentItem.CommentScore;
		StringBuilder stringBuilder;
		using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
		{
			if (GameVariables.Current.joinUsBlogManager.TryGetItemSaveData(_currentItem, out var saveData))
			{
				_currentInteraction = saveData.InteractionType;
				switch (_currentInteraction)
				{
				case JoinUsItemSaveData.InteractionTypeEnum.DownVote:
					num--;
					break;
				case JoinUsItemSaveData.InteractionTypeEnum.Upvote:
					num++;
					break;
				}
			}
			else
			{
				_currentInteraction = JoinUsItemSaveData.InteractionTypeEnum.Nothing;
			}
			stringBuilder.Append(num);
			_scoreText.SetText(stringBuilder);
			UpdateButtonColors();
		}
	}
}
