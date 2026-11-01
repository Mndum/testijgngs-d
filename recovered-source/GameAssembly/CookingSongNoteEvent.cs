public class CookingSongNoteEvent
{
	public struct Builder(NoteButtonTypeEnum noteButtonType, double startTime, double endTime)
	{
		public NoteButtonTypeEnum NoteButtonType = noteButtonType;

		public double StartTime = startTime;

		public double EndTime = endTime;

		public double Duration => EndTime - StartTime;

		public bool IsDuration => StartTime != EndTime;

		public CookingSongNoteEvent Build()
		{
			return new CookingSongNoteEvent(NoteButtonType, StartTime, EndTime);
		}
	}

	public readonly NoteButtonTypeEnum NoteButtonType;

	public readonly double StartTime;

	public readonly double EndTime;

	public double Duration => EndTime - StartTime;

	public bool IsDuration => StartTime != EndTime;

	public CookingSongNoteEvent(NoteButtonTypeEnum noteButtonType, double startTime, double endTime)
	{
		NoteButtonType = noteButtonType;
		StartTime = startTime;
		EndTime = endTime;
	}
}
