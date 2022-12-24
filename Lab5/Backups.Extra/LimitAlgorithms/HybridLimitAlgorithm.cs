using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class HybridLimitAlgorithm : ILimitAlgorithm
{
    public HybridLimitAlgorithm(HybridType type, IEnumerable<ILimitAlgorithm> algorithms)
    {
        Type = type;
        Algorithms = algorithms;
    }

    public HybridType Type { get; }
    public IEnumerable<ILimitAlgorithm> Algorithms { get; }
    public IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> filter = new List<RestorePoint>();
        if (Type is HybridType.And)
        {
            filter = restorePoints;
            filter = Algorithms.Aggregate(filter, (current, algo) => algo.Run(restorePoints).Intersect(current).ToList());
        }
        else if (Type is HybridType.Or)
        {
            filter = filter = Algorithms.Aggregate(filter, (current, algo) => algo.Run(restorePoints).Union(current).ToList());
        }

        return filter;
    }
}