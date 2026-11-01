using System;

[Serializable]
public class EventHolder
{
	public string data;

	public EventEnum eventEnum;

	public override string ToString()
	{
		return "EventHolder: data=\"" + data + "\" EventEnum=" + eventEnum;
	}
}
