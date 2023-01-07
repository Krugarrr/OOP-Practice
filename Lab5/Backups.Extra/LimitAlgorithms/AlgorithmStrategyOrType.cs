using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class AlgorithmStrategyOrType : ILimitAlgorithmStrategy
{
    public IReadOnlyList<RestorePoint> Execute(IEnumerable<ILimitAlgorithm> limitAlgorithms, IReadOnlyList<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> filter = new List<RestorePoint>();
        return limitAlgorithms.Aggregate(filter, (current, algo) => algo.Run(restorePoints).Union(current).ToList());
    }
}