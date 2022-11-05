namespace Isu.Extra.Tools;

public class ScheduleException : IsuExtraExceptions
{
    public ScheduleException(string message)
        : base(message) { }

    public static ScheduleException ScheduleIntersectionException()
    {
        throw new ScheduleException("Schedules intersection");
    }
}