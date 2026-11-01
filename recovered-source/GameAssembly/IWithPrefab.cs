public interface IWithPrefab<TPrefab>
{
	TPrefab Prefab { get; }

	void InitPrefab(TPrefab prefab);
}
