﻿using AwesomeBankAPI.Config;
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
        private readonly IAuthenticationService _authenticationService;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService, IAuthenticationService authenticationService)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _authenticationService = authenticationService;
        }

        public decimal CalDepositFee(decimal depositAmount)
        {
            return GlobalConfig.DEPOSIT_FEE * depositAmount / 100M;
        }

        public Transaction MakeDeposit(Guid accountId, decimal amount, Guid actionBy, bool applyFee)
        {
            try
            {
                Account account = _accountService.GetAccount(accountId);
                if (account == null)
                {
                    throw new Exception("Account not found");
                }

                bool hasAuthorize = _authenticationService.CheckAccountAuthorize(actionBy, account.Id);
                if (!hasAuthorize)
                {
                    throw new Exception("Unauthorize to make transaction");
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
                transaction.TransactionFee = applyFee ? CalDepositFee(transaction.Amount) : 0;
                transaction.AmountAfterFee = transaction.Amount - transaction.TransactionFee;

                transaction.Amount = Math.Round(transaction.Amount, 2);
                transaction.TransactionFee = Math.Round(transaction.TransactionFee, 2);
                transaction.AmountAfterFee = Math.Round(transaction.AmountAfterFee, 2);

                if (transaction.AmountAfterFee < 0)
                {
                    throw new Exception("Amount included fee can't less than zero.");
                }

                var result = _transactionRepository.Add(transaction);
                if (result == (int)GlobalConfig.Result.ERROR)
                {
                    throw new Exception("Unable to create transaction.");
                }

                var applyResult = _accountService.ApplyToBalance(account, transaction.AmountAfterFee);
                if (!applyResult)
                {
                    throw new Exception("Unable to update account balance amount.");
                }
                return transaction;

            }
            catch (Exception ex) { throw ex; }
            
        }
        public Transaction MakeDeposit(string accountIban, decimal amount, Guid actionBy, bool applyFee)
        {
            var account = _accountService.GetAccount(accountIban);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }
            var transaction = MakeDeposit(account.Id, amount, actionBy, applyFee);
            return transaction;
        }

        public Transaction MakeTransfer(Guid senderAccountId, Guid receiverAccountId, decimal amount, Guid ActionBy, bool applyFee)
        {
            var senderAccount = _accountService.GetAccount(senderAccountId);
            if (senderAccount == null)
            {
                throw new Exception("Sender account not found");
            }

            bool hasAuthorize = _authenticationService.CheckAccountAuthorize(ActionBy, senderAccount.Id);
            if (!hasAuthorize)
            {
                throw new Exception("Unauthorize to make transaction");
            }

            var receiverAccount = _accountService.GetAccount(receiverAccountId);
            if (receiverAccount == null)
            {
                throw new Exception("Receiver account not found.");
            }

            //Transfer amount must great than zero
            if (amount <= 0)
            { 
                throw new Exception("Transfer amount must great than zero");
            }

            //Validate balance amount of sender
            if (senderAccount.BalanceAmount < amount)
            {
                throw new Exception("Not enought balance to make transfer.");
            }

            //Create transfer transacetion 
            Transaction transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccoundId = senderAccount.Id,
                Amount = amount,
                ReceiverAccoundId = receiverAccount.Id,
                TransactionType = (int)GlobalConfig.TransactionType.TRANSFER,
                ActionByCustomerId = senderAccount.CustomerId,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };

            //money that deposit to receiver account must apply deposit fee same as deposit process.
            transaction.TransactionFee = applyFee ? CalDepositFee(transaction.Amount) : 0;
            transaction.AmountAfterFee = transaction.Amount - transaction.TransactionFee;

            transaction.Amount = Math.Round(transaction.Amount, 2);
            transaction.TransactionFee = Math.Round(transaction.TransactionFee, 2);
            transaction.AmountAfterFee = Math.Round(transaction.AmountAfterFee, 2);

            var resultTransacetion = _transactionRepository.Add(transaction);
            if (resultTransacetion == (int)GlobalConfig.Result.ERROR)
            {
                throw new Exception("Unable to create transaction.");
            }

            var applySenderResult = _accountService.ApplyToBalance(senderAccount, -1 * transaction.Amount);
            var applyRecieverResult = _accountService.ApplyToBalance(receiverAccount, transaction.AmountAfterFee);
            if (!applySenderResult || !applyRecieverResult)
            {
                throw new Exception("Cannot update account balance");
            }

            return transaction;
        }

        public Transaction MakeTransfer(string senderIban, string receiverIban, decimal amount, Guid ActionBy, bool applyFee)
        {
            var senderAccount = _accountService.GetAccount(senderIban);
            var receiverAccount = _accountService.GetAccount(receiverIban);
            if (senderAccount == null || receiverAccount == null)
            {
                throw new Exception("Account not found.");
            }
            var transaction = MakeTransfer(senderAccount.Id, receiverAccount.Id, amount, ActionBy, applyFee);
            return transaction;
        }
    }
}
