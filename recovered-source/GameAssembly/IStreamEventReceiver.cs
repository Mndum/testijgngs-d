public interface IStreamEventReceiver
{
	void StreamEvent(StreamManager streamManager, InteractionMemoryEventEnum tags);
}
