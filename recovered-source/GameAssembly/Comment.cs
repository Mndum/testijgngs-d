using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Comment : DialogueChainStreamReplyable
{
	private RectTransform _rt;

	private TextMeshProUGUI _tmp;

	private Button _button;

	public float Position => _rt.anchoredPosition.y;

	public float Height => _rt.sizeDelta.y;

	public Comment(GameObject commentPrefab, Transform parent)
	{
		_rt = Object.Instantiate(commentPrefab, parent).GetComponent<RectTransform>();
		_tmp = _rt.GetComponentInChildren<TextMeshProUGUI>();
		_button = _rt.GetComponent<Button>();
	}

	public float SetDialogueChain(DialogueChain dialogueChain)
	{
		InnerSetDialogueChain(dialogueChain);
		_tmp.text = StringUtils.GreyColorTagStart + GameVariables.TimeToHourString(SingletonLoadableMonoBehaviour<GameScript>.Instance.GameVariables.time) + "</color>\n" + dialogueChain.GetText();
		Vector2 preferredValues = _tmp.GetPreferredValues();
		Vector2 sizeDelta = _rt.sizeDelta;
		sizeDelta.y = preferredValues.y + _tmp.margin.z + _tmp.margin.w;
		_rt.sizeDelta = sizeDelta;
		return sizeDelta.y;
	}

	public float Replace(DialogueChain newDialogueChain)
	{
		float height = Height;
		return SetDialogueChain(newDialogueChain) - height;
	}

	public void AddOnClick(UnityAction<Comment> onClick)
	{
		_button.onClick.AddListener(() =>
		{
			onClick(this);
		});
	}

	public void RemoveOnClick()
	{
		_button.onClick.RemoveAllListeners();
	}

	public void SetActive(bool value)
	{
		_rt.gameObject.SetActive(value);
	}

	public void ResetPosition()
	{
		_rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x, 0f);
	}

	public void Destroy()
	{
		Object.Destroy(_rt.gameObject);
	}

	public void MoveY(float by)
	{
		Vector2 anchoredPosition = _rt.anchoredPosition;
		anchoredPosition.y += by;
		_rt.anchoredPosition = anchoredPosition;
	}
}
