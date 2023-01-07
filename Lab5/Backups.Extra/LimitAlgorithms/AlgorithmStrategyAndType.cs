using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class AlgorithmStrategyAndType : ILimitAlgorithmStrategy
{
    public IReadOnlyList<RestorePoint> Execute(IEnumerable<ILimitAlgorithm> limitAlgorithms, IReadOnlyList<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> filter;
        filter = restorePoints;
        return limitAlgorithms.Aggregate(filter, (current, algo) => algo.Run(restorePoints).Intersect(current).ToList());
    }
}