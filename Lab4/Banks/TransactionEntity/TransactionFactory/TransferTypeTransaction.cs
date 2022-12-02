namespace Banks.TransactionEntity;

public class TransferTypeAbstractTransacion : AbstractTransacion
{
    public TransferTypeAbstractTransacion(int id, decimal sum, TransactionType type, Bank transferBank, int transferAccountId)
        : base(id, sum, type)
    {
        TransferBank = transferBank;
        TransferAccountId = transferAccountId;
    }

    public new Bank TransferBank { get; }
    public new int TransferAccountId { get; }
}