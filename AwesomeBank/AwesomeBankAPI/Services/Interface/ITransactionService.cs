using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface ITransactionService
    {
        Transaction MakeTransfer(Guid senderAccountId, Guid receiverAccountId, decimal amount);
        Transaction MakeDeposit(Guid accountId, decimal amount);

        decimal CalDepositFee(decimal depositAmount);
    }
}
