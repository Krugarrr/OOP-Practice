namespace Banks.TransactionEntity;

public class AddnTakeTypeAbstractTransacion : AbstractTransacion
{
    public AddnTakeTypeAbstractTransacion(int id, decimal sum, TransactionType type)
        : base(id, sum, type)
    {
    }
}