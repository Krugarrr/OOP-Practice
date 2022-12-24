using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public interface ILimitAlgorithm
{
    IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints);
}