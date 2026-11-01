public class FrickSceneTransitionData : ViewSceneTransitionDataBase<FrickScene>
{
	public FrickSceneInitFlags InitFlags;

	public Live2DLewdSceneController Controller;

	public Live2DLewdSceneController PreviousController;

	public Live2DPrefabHolder.ILive2DControllerUsage Usage;

	public FrickSceneTransitionData(FrickSceneInitFlags initFlags)
	{
		InitFlags = initFlags;
	}
}
