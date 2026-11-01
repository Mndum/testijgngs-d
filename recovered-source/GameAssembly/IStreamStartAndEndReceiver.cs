public interface IStreamStartAndEndReceiver
{
	void StreamStarted(StreamManager streamManager);

	void StreamFinished(StreamManager streamManager);

	void StreamLateFinished(StreamManager streamManager);
}
