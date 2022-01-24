using System.Collections.Generic;
using TimeBank.API.Dtos;
using TimeBank.Repository.Models;

namespace TimeBank.API.Maps
{
    public static class TokenTransactionResponseMap
    {
        public static List<TokenTransactionResponseDto> MapToDto(List<TokenTransaction> sourceTransactions)
        {
            List<TokenTransactionResponseDto> responseTransactions = new();

            foreach (var transaction in sourceTransactions)
            {
                responseTransactions.Add(new TokenTransactionResponseDto
                {
                    SenderName = $"{transaction.Sender.FirstName} {transaction.Sender.LastName}",
                    RecipientName = $"{transaction.Recipient.FirstName} {transaction.Recipient.LastName}",
                    TransactionAmount = transaction.Amount,
                    ProcessedOn = transaction.ProcessedOn,
                    ActionDescription = ""
                });
            }

            return responseTransactions;
        }
    }
}
