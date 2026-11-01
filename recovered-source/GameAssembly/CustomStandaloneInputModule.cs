using UnityEngine.EventSystems;

public class CustomStandaloneInputModule : StandaloneInputModule
{
	public PointerEventData GetPointerData()
	{
		return m_PointerData[-1];
	}

	public bool TryGetPointerData(out PointerEventData pointerEventData)
	{
		return m_PointerData.TryGetValue(-1, out pointerEventData);
	}
}
