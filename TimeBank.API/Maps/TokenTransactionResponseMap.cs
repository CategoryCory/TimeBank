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
                    TokenTransactionId = transaction.TokenTransactionId,
                    SenderId = transaction.SenderId,
                    SenderName = $"{transaction.Sender.FirstName} {transaction.Sender.LastName}",
                    RecipientId = transaction.RecipientId,
                    RecipientName = $"{transaction.Recipient.FirstName} {transaction.Recipient.LastName}",
                    Amount = transaction.Amount,
                    ProcessedOn = transaction.ProcessedOn,
                });
            }

            return responseTransactions;
        }
    }
}
