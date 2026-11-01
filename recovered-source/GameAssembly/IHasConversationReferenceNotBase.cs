public interface IHasConversationReferenceNotBase<TKeyNameGenerator>
{
	bool CreateEmptyKeys(TKeyNameGenerator x);

	bool UpdateKeyNames(TKeyNameGenerator x);

	void ClearWithoutDeletingEntries();

	void ClearAndDeleteEntries();
}
