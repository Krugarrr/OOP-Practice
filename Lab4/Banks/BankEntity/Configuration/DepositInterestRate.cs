using System.Runtime.InteropServices;

namespace Banks;

public class DepositInterestRate
{
    public DepositInterestRate(IReadOnlyList<DepositInterestPoint> points, decimal maxInterestRate)
    {
        DepositInterestPoints = points;
        MaxInterestRate = maxInterestRate;
    }

    public static DepositInterestRateBuilder Builder => new DepositInterestRateBuilder();
    public IReadOnlyList<DepositInterestPoint> DepositInterestPoints { get; }
    public decimal MaxInterestRate { get; }

    public class DepositInterestRateBuilder
    {
        private readonly List<DepositInterestPoint> _points;
        private decimal maxRate;
        public DepositInterestRateBuilder()
        {
            _points = new List<DepositInterestPoint>();
            maxRate = 0;
        }

        public DepositInterestRateBuilder AddInterestPoint(DepositInterestPoint point)
        {
            _points.Add(point);
            return this;
        }

        public DepositInterestRateBuilder AddMaxRate(DepositInterestPoint point)
        {
            _points.Add(point);
            return this;
        }

        public DepositInterestRate Build()
        {
            return new DepositInterestRate(_points, maxRate);
        }
    }
}