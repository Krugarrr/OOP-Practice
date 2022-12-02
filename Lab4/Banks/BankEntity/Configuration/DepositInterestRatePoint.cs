namespace Banks;

public class DepositInterestPoint
{
    public DepositInterestPoint(decimal minSum, decimal maxSum, decimal interestRate)
    {
        MinSum = minSum;
        MaxSum = maxSum;
        InterestRate = interestRate;
    }

    public decimal MinSum { get; }
    public decimal MaxSum { get; }
    public decimal InterestRate { get; }
}