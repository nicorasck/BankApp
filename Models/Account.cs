using System.Transactions;
using BankApp.Data;

namespace BankApp.Models;

// Add profile data for application users by adding properties to the ApplicationUser class

public enum AccountType
{
    Main = 1, Savings, Salary, Portfolio, ISK, KF
}

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string? AccountName { get; set; }
    public decimal Balance { get; set; }
    public AccountType AccountType { get; set; }
    public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>(); // Navigation property
    // public List<BankTransaction> IncomingTransactions { get; set; } = new List<BankTransaction>(); // Navigation property
    public bool IsActive { get; set; }
    public string? CardNumber { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

