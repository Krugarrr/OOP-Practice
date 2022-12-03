using System.Reflection.PortableExecutable;

namespace Banks;

public class BankConfiguration : IEquatable<BankConfiguration>
{
    public BankConfiguration(
        decimal debitInterestRate,
        decimal transactionLimit,
        decimal creditLimit,
        decimal creditComission,
        DepositInterestRate depositInterestRate,
        int depositAccountTime)
    {
        DebitInterestRate = debitInterestRate;
        TransactionLimit = transactionLimit;
        CreditLimit = creditLimit;
        CreditComission = creditComission;
        DepositInterestRate = depositInterestRate;
        DepositAccountTime = depositAccountTime;
    }

    public decimal DebitInterestRate { get; }
    public decimal TransactionLimit { get; }

    // deposit days
    public decimal CreditLimit { get; }
    public decimal CreditComission { get; }
    public DepositInterestRate DepositInterestRate { get; }
    public int DepositAccountTime { get; }

    public bool Equals(BankConfiguration other)
    {
        return other is not null
               && DebitInterestRate == other.DebitInterestRate
               && TransactionLimit == other.TransactionLimit
               && CreditLimit == other.CreditLimit
               && CreditComission == other.CreditComission
               && Equals(DepositInterestRate, other.DepositInterestRate);
    }

    public override bool Equals(object obj)
    {
        if (obj is BankConfiguration configuration)
        {
            return Equals(configuration);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DebitInterestRate, TransactionLimit, CreditLimit, CreditComission, DepositInterestRate);
    }
}

public class BankConfigurationBuilder
{
    private decimal debitInterestRate;
    private decimal transactionLimit;
    private decimal creditLimit;
    private decimal creditComission;
    private DepositInterestRate depositInterestRate;
    private int depositAccountTime;

    public BankConfigurationBuilder()
    {
        debitInterestRate = 0;
        transactionLimit = 0;
        creditLimit = 0;
        creditComission = 0;
        depositInterestRate = null;
        depositAccountTime = 0;
    }

    public BankConfigurationBuilder WithDebitInterestRate(decimal rate)
    {
        debitInterestRate = rate;
        return this;
    }

    public BankConfigurationBuilder WithTransactionLimit(decimal limit)
    {
        transactionLimit = limit;
        return this;
    }

    public BankConfigurationBuilder WithCreditLimit(decimal limit)
    {
        creditLimit = limit;
        return this;
    }

    public BankConfigurationBuilder WithCreditComission(decimal comission)
    {
        creditComission = comission;
        return this;
    }

    public BankConfigurationBuilder WithDepositInterestRate(DepositInterestRate rate)
    {
        depositInterestRate = rate;
        return this;
    }

    public BankConfigurationBuilder WithDepositAccountTime(int time)
    {
        depositAccountTime = time;
        return this;
    }

    public BankConfiguration Build()
    {
        return new BankConfiguration(
            debitInterestRate,
            transactionLimit,
            creditLimit,
            creditComission,
            depositInterestRate,
            depositAccountTime);
    }
}