using System.Transactions;
using BankApp.Data;

namespace BankApp.Models;

public enum AccountType
{
    Main = 1, Savings, Salary, Portfolio, ISK, KF
}

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string? AccountName { get; set; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; set; }
    public  List<BankTransaction> BankTransactions { get; set; } = new List<BankTransaction>();
    // public List<BankTransaction> IncomingTransactions { get; set; } = new List<BankTransaction>(); // Navigation property
    public bool IsActive { get; set; }
    public bool DisplayTransactions { get; set; } // this is to display transaction history with a toggle-function!
    public string? CardNumber { get; set; }
    public string UserId { get; set; } = string.Empty;
    public  ApplicationUser? User { get; set; }
}

