using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChangingRoomGui : StaticGuiBase
{
	[Header("Manual")]
	[SerializeField]
	private CompoundItemList itemList;

	private SortableItemList.ItemListAdapter<Item> itemListAdapter;

	[FormerlySerializedAs("itemViewScript")]
	[SerializeField]
	private ItemStats itemStats;

	[SerializeField]
	private MoneyInfo moneyInfo;

	[Header("Auto")]
	[SerializeField]
	private Button returnButton;

	[SerializeField]
	private FreeCamera freeCamera;

	[SerializeField]
	private ButtonList sideButtonList;

	[SerializeField]
	private ButtonList itemActionButtonList;

	[SerializeField]
	private ButtonList sectionButtonList;

	public CompoundItemList ItemList => itemList;

	public ItemStats ItemStats => itemStats;

	public MoneyInfo MoneyInfo => moneyInfo;

	public Button ReturnButton => returnButton;

	public FreeCamera FreeCamera => freeCamera;

	public ButtonList SideButtonList => sideButtonList;

	public ButtonList ItemActionButtonList => itemActionButtonList;

	public ButtonList SectionButtonList => sectionButtonList;

	protected override void FillReferencesInherited()
	{
		Transform transform = base.transform;
		transform.EnsureComponentInChild<Button>(out returnButton, "Return Button");
		transform.EnsureComponentInChild<ButtonList>(out sideButtonList, "Button List");
		if (transform.EnsureChildrenNested(out var child, "Current Equipment", "Sections"))
		{
			child.EnsureComponent<ButtonList>(out sectionButtonList);
		}
		itemStats.transform.parent.EnsureComponentInChild<ButtonList>(out itemActionButtonList, "Buttons");
		transform.EnsureComponent<FreeCamera>(out freeCamera);
	}
}
