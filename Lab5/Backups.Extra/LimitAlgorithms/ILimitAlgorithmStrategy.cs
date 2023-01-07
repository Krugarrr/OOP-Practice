using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public interface ILimitAlgorithmStrategy
{
    IReadOnlyList<RestorePoint> Execute(IEnumerable<ILimitAlgorithm> limitAlgorithms, IReadOnlyList<RestorePoint> restorePoints);
}