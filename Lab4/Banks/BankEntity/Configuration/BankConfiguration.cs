using System.Reflection.PortableExecutable;

namespace Banks;

public class BankConfiguration
{
    public BankConfiguration(
        decimal debitInterestRate,
        decimal transactionLimit,
        decimal creditLimit,
        decimal creditComission,
        DepositInterestRate depositInterestRate)
    {
        DebitInterestRate = debitInterestRate;
        TransactionLimit = transactionLimit;
        CreditLimit = creditLimit;
        CreditComission = creditComission;
        DepositInterestRate = depositInterestRate;
    }

    public decimal DebitInterestRate { get; }
    public decimal TransactionLimit { get; }

    // deposit days
    public decimal CreditLimit { get; }
    public decimal CreditComission { get; }
    public DepositInterestRate DepositInterestRate { get; }
}

public class BankConfigurationBuilder
{
    private decimal debitInterestRate;
    private decimal transactionLimit;
    private decimal creditLimit;
    private decimal creditComission;
    private DepositInterestRate depositInterestRate;

    public BankConfigurationBuilder()
    {
        debitInterestRate = 0;
        transactionLimit = 0;
        creditLimit = 0;
        creditComission = 0;
        depositInterestRate = null;
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

    public BankConfiguration Build()
    {
        return new BankConfiguration(
            debitInterestRate,
            transactionLimit,
            creditLimit,
            creditComission,
            depositInterestRate);
    }
}