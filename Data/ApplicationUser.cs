using System.Transactions;
using BankApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Data;

// Add profile data for application users by adding properties to the ApplicationUser class

public class ApplicationUser : IdentityUser
{
    public string? Handle { get; set; }
    public string SocialSecurityNumber { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>(); // avoiding null references
    public string Address { get; set; }
}


