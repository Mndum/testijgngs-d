public interface IModificationPeriodButtonList
{
	ButtonListWrapper AddButton(int order = 0, CommonButtonColorType buttonColorType = CommonButtonColorType.Normal);

	ButtonListWrapper AddCommonSmall(int order = 0, CommonButtonColorType buttonColorType = CommonButtonColorType.Normal);

	void UpdateButtonSingle(ButtonListWrapper buttonListWrapper);
}
