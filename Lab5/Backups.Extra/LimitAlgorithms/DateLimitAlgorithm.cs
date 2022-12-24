using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class DateLimitAlgorithm : ILimitAlgorithm
{
    public DateLimitAlgorithm(DateTime interval)
    {
        Interval = interval;
    }

    public DateTime Interval { get; }

    public IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints)
        => restorePoints.Where(rp => rp.Date < Interval).ToList();
}