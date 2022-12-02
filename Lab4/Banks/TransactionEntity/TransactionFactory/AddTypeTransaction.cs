﻿using System.Transactions;

namespace Banks.TransactionEntity;

public class AddTypeTransaction : AbstractTransacion
{
    public AddTypeTransaction(decimal money)
        : base(money)
    {
        Type = TransactionType.Add;
    }

    protected override void ChangeStatus()
    {
        Status = TransactionStatus.Aborted;
    }
}