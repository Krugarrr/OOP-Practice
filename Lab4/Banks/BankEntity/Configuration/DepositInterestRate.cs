using System.Runtime.InteropServices;

namespace Banks;

public class DepositInterestRate : IEquatable<DepositInterestRate>
{
    public DepositInterestRate(IReadOnlyList<DepositInterestPoint> points, decimal maxInterestRate)
    {
        DepositInterestPoints = points;
        MaxInterestRate = maxInterestRate;
    }

    public static DepositInterestRateBuilder Builder => new DepositInterestRateBuilder();
    public IReadOnlyList<DepositInterestPoint> DepositInterestPoints { get; }
    public decimal MaxInterestRate { get; }

    public decimal FindSuitRate(decimal balance)
    {
        DepositInterestPoint point = DepositInterestPoints.First(p => (balance >= p.MinSum && balance <= p.MaxSum));
        if (point is null)
            return MaxInterestRate;
        return point.InterestRate;
    }

    public bool Equals(DepositInterestRate other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(DepositInterestPoints, other.DepositInterestPoints) && MaxInterestRate == other.MaxInterestRate;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DepositInterestRate)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DepositInterestPoints, MaxInterestRate);
    }

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

        public DepositInterestRateBuilder AddMaxRate(decimal rate)
        {
            maxRate = rate;
            return this;
        }

        public DepositInterestRate Build()
        {
            return new DepositInterestRate(_points, maxRate);
        }
    }
}