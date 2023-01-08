using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class DateLimitAlgorithm : ILimitAlgorithm
{
    public DateLimitAlgorithm(TimeSpan interval)
    {
        Interval = interval;
    }

    public TimeSpan Interval { get; }

    public IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints)
        => restorePoints.Where(rp => DateTime.Now - rp.Date >= Interval).ToList();
}