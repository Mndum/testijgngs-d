using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameItemContainer : AdvancedItemListPrefab
{
	protected enum GameItemButtonState
	{
		Normal,
		Equipped
	}

	[SerializeField]
	private TextMeshProUGUI _description;

	[SerializeField]
	private Image _icon;

	private bool _isIconNotNull;

	[SerializeField]
	private Button _button;

	private Image _backgroundImage;

	private Item _current;

	public Item Current
	{
		get
		{
			return _current;
		}
		set
		{
			SetItem(value);
		}
	}

	public override bool HasDefaultButton => true;

	public void Awake()
	{
		_isIconNotNull = _icon != null;
		_backgroundImage = (Image)_button.targetGraphic;
	}

	public void SetItem(Item item, Selection<Item> referenceSelection = null)
	{
		_current = item;
		if (_current != null)
		{
			if (_isIconNotNull)
			{
				_icon.sprite = _current.GetItemPrefab().GetIcon();
			}
			StringBuilder stringBuilder;
			using (StringBuilderCache.AcquireWithUsing(out stringBuilder))
			{
				_description.fontStyle = (IsFocused ? FontStyles.Bold : FontStyles.Normal);
				if (_current.IsEquipped())
				{
					_current.GetNameStringBuilder(stringBuilder);
					stringBuilder.Append(" ");
					stringBuilder.Append(LOC.UI.Equipment_EquippedPostfix);
					SetState(GameItemButtonState.Equipped);
				}
				else
				{
					_current.GetNameStringBuilder(stringBuilder);
					SetState(GameItemButtonState.Normal);
				}
				referenceSelection?.DescribeItemSelection(stringBuilder, _current);
				_description.SetText(stringBuilder);
				return;
			}
		}
		if (_isIconNotNull)
		{
			_icon.sprite = null;
		}
		_description.text = null;
		_description.fontStyle = FontStyles.Normal;
		SetState(GameItemButtonState.Normal);
	}

	protected virtual void SetState(GameItemButtonState gameItemButtonState)
	{
		switch (gameItemButtonState)
		{
		case GameItemButtonState.Normal:
			_button.SetAllNormalColors(UiOverlay.Instance.NormalColor);
			break;
		case GameItemButtonState.Equipped:
			_button.SetAllNormalColors(UiOverlay.Instance.GreenColor);
			break;
		}
	}

	public override Button GetDefaultButton()
	{
		return _button;
	}

	public override void AfterReturnedToPool()
	{
		base.AfterReturnedToPool();
		_current = null;
		if (_isIconNotNull)
		{
			_icon.sprite = null;
		}
	}
}
