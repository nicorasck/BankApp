using BankApp.Data;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
/// <summary>
/// Using this file as a Service => Dependency (injection)
/// It will contain several of different classes or/and functions!
/// </summary>
public class DbContextService
{   // private field to store instances in the database
     private const string ClearingNumber = "1337"; // constant => Clearing Number for this BankApp
     private readonly ApplicationDbContext _context;
     // Constructor => injects ApplicationDbContext 
     public DbContextService(ApplicationDbContext context)
     {    // assigning context (injected in this ctor) to the private field _context
          _context = context;
     }

     // Method to create and save new account for each new user
     public async Task<Account> CreateAccountAsync(Account account, ApplicationUser user)
     {
          ///account.UserId = user.Id;

          if (account == null)
               throw new ArgumentNullException(nameof(account));

          // // Debugging
          // //Console.WriteLine($"The new account {account.AccountName} is now created.");

          account.UserId = user.Id; // Ensure the User Id will be assigned 

          if (string.IsNullOrEmpty(account.AccountNumber))
               account.AccountNumber = await GenerateAccountNumberAsync();
          // Adding the new account to the database
          _context.Accounts.Add(account);
          // Saving changes asynchronously in the database
          await _context.SaveChangesAsync();
          return account;
     }

     // Method for GenerateAccountNumberAsync with unique number as well (format "xxxx-xxx xxx xx")
     private async Task<string> GenerateAccountNumberAsync()
     {
          string accountNumber;
          bool exists;

          do
          {
               string combination1 = new Random().Next(100, 999).ToString();    // first block with three digits
               string combination2 = new Random().Next(100, 999).ToString();   // second block with three digits
               string combination3 = new Random().Next(10, 99).ToString();     // third block with two digits
               // the whole number combined => format "xxxx-xxx xxx xx"
               accountNumber = $"{ClearingNumber}-{combination1} {combination2} {combination3}";
               // error handling, if the number already exists in the database
               exists = await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
          }
          // generating until a unique number´comes.
          while (exists);

          return accountNumber;
     }

     // Looking for a specific Id (queries in Accounts DbSet)
     public async Task<Account?> GetAccountByIdAsync(int accountId)
     {
          return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
     }

     // Updating the database => account
     public async Task UpdateAccountAsync(Account account)
     {
          _context.Accounts.Update(account);
          await _context.SaveChangesAsync();
     }

     // Looking for a specific account and user Id, then deleting it
     public async Task DeleteAccountAsync(string userId, int accountId)
     {
          // User and account Id
          var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId);
          if (account != null)
          {    // Updating the database
               _context.Accounts.Remove(account);
               await _context.SaveChangesAsync();
          }
     }

     // To make a transaction (BankTransaction)
     public async Task CreateTransactionAsync(BankTransaction transaction)
     {
          // Updating the transaction
          _context.BankTransactions.Add(transaction);
          await _context.SaveChangesAsync();
     }

     // To look for transactions for each belonging account for a user
     public async Task<List<BankTransaction>> GetTransactionsForAccountAsync(int id, string accountNumber)
     {
          // listing all transactions for each account
          return await _context.BankTransactions
               .Include(b => b.FromAccount)
               .Where(b => b.FromAccountId == id || b.ToAccount == accountNumber) // => accountNumber in Account.cs
               .ToListAsync();
     }

     public async Task<Account?> GetMainAccountForUserAsync(string userId)
     {
          // Showing the 'Main' Account
          return await _context.Accounts
               .FirstOrDefaultAsync(a => a.UserId == userId && a.AccountType == AccountType.Main);
     }

     // Bringing all accounts from the database
    public async Task<List<Account>> GetAccountsByUserAsync(ApplicationUser user)
    {
        return await _context.Accounts.Where(a => a.User.Id == user.Id).ToListAsync();
    }

     // tested function of Jojje's (used it in CreateAccount)
    public async Task AddAccountAsync(ApplicationUser user, Account account){

        account.UserId = user.Id;
        account.AccountNumber = await GenerateAccountNumberAsync();
        // account.AccountType = SelectedAccountType;
        // account.IsActive = true;


        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

}