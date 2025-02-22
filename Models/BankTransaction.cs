using System.Transactions;
using BankApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models
{
    public enum Currency
    {
        SEK = 1, EUR, USD
    }

    public class BankTransaction
    {
        public int Id { get; set; }
        // public int ToAccountId { get; set; } // Foreign Key to Account
        public string ToAccount { get; set; }
        public int FromAccountId { get; set; } // Foreign Key to the entity of Account
        public Account FromAccount { get; set; } // Navigation property to Account
        public DateTime TransactionDate { get; set; }
        public decimal OutgoingBalance { get; set; }
        public Currency Currency { get; set; }
        public bool IsReserved { get; set; }
        public string? Message { get; set; }
    }
}