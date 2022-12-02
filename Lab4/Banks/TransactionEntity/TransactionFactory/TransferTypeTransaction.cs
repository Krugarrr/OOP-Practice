namespace Banks.TransactionEntity;

public class TransferTypeAbstractTransacion : AbstractTransacion
{
    public TransferTypeAbstractTransacion(Bank transferBank, int transferAccountId)
        : base()
    {
        Type = TransactionType.Transfer;
        TransferBank = transferBank;
        TransferAccountId = transferAccountId;
    }

    public new Bank TransferBank { get; }
    public new int TransferAccountId { get; }
}