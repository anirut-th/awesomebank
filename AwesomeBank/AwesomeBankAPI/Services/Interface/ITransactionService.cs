using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface ITransactionService
    {
        Transaction MakeTransfer(Guid senderAccountId, Guid receiverAccountId, decimal amount, Guid ActionBy, bool applyFee);
        Transaction MakeTransfer(string senderIban, string receiverIban, decimal amount, Guid ActionBy, bool applyFee);
        Transaction MakeDeposit(Guid accountId, decimal amount, Guid ActionBy, bool applyFee);
        Transaction MakeDeposit(string accountIban, decimal amount, Guid ActionBy, bool applyFee);

        decimal CalDepositFee(decimal depositAmount);
    }
}
