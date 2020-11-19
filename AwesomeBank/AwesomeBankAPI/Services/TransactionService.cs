using AwesomeBankAPI.Config;
using AwesomeBankAPI.Models;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
        }

        public decimal CalDepositFee(decimal depositAmount)
        {
            return GlobalConfig.DEPOSIT_FEE * depositAmount / 100M;
        }

        public Transaction MakeDeposit(Guid accountId, decimal amount)
        {
            Account account = _accountService.GetAccount(accountId);
            if(account == null)
            {
                throw new Exception("Account not found");
            }

            if (amount <= 0)
            {
                throw new Exception("Amount can't less than zero.");
            }

            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccoundId = accountId,
                Amount = amount,
                TransactionType = (int)GlobalConfig.TransactionType.DEPOSIT,
                ActionByCustomerId = account.CustomerId,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };
            transaction.TransactionFee = CalDepositFee(transaction.Amount);
            transaction.AmountAfterFee = transaction.Amount - transaction.TransactionFee;

            var result = _transactionRepository.Add(transaction);
            if (result == (int)GlobalConfig.Result.ERROR)
            {
                throw new Exception("Unable to create transaction."); 
            }

            _accountService.ApplyToBalance(account, transaction.AmountAfterFee);
            return transaction;
        }

        public Transaction MakeTransfer(Guid senderAccountId, Guid receiverAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
