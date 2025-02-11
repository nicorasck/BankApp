using System.Transactions;
using BankApp.Models;
using Microsoft.AspNetCore.Identity;
using BankApp.Data;

// Add profile data for application users by adding properties to the ApplicationUser class

public class ApplicationUser : IdentityUser
{   
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nickname { get; set; }
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new List<Account>(); // avoiding null references
    public string Address { get; set; } = string.Empty;
}


