using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class HybridLimitAlgorithm : ILimitAlgorithm
{
    public HybridLimitAlgorithm(IEnumerable<ILimitAlgorithm> algorithms, ILimitAlgorithmStrategy strategy)
    {
        Algorithms = algorithms;
        Strategy = strategy;
    }

    public ILimitAlgorithmStrategy Strategy { get; }
    public IEnumerable<ILimitAlgorithm> Algorithms { get; }

    public IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints) =>
        Strategy.Execute(Algorithms, restorePoints);
}