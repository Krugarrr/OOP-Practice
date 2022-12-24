using Backups.Entities;

namespace Backups.Extra.LimitAlgorithms;

public class AmountLimitAlgorithm : ILimitAlgorithm
{
    public AmountLimitAlgorithm(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; }

    public IReadOnlyList<RestorePoint> Run(IReadOnlyList<RestorePoint> restorePoints)
        => restorePoints.Take(Amount).ToList();
}