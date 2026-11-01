using UnityEngine;

public abstract class DataDisplay<T> : MonoBehaviour
{
	protected T _current;

	public T Current
	{
		get
		{
			return _current;
		}
		set
		{
			SetData(value);
		}
	}

	protected abstract void SetData(T newValue);
}
