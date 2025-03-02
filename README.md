Task: EF Identity Bank
________________________________________
We are creating a banking app with the following features:


Authentication:
- Login
- Logout
- Access control

User Profile:
- View Profile
- Edit
- (A- view certain fields)
- Change password
- Delete account (A)

Account Admin (A)
- Create user's 
- Delete user's 
- Edit user's

Account Management
- Create account
- Name/Rename account
- Lock/Unlock account
- Delete account
 
Payments
- Create transactions
 
Some functions and fields should only be available to an admin role, such as editing personal identification numbers and deleting accounts.
These are marked with (A).

We identify the following classes:

Transaction:
- Id
- Date
- Balance
- Currency
- IsReserved
- Message
- Amount
- FK Account - FromAccount
- string ToAccount

Account:
- Id
- AccountNumber
- AccountName
- Balance
- AccountType
- IsActive
- CardNumber?
- List<Transaction>
- FK-ApplicationUser

ApplicationUser:
- SocialSecurityNr
- Address
- PhoneNumber
- Email
- List<Account>

Note: We use ApplicationUser in Identity but add properties that do not already exist.

Banking Task:
1. Create properties for users (ApplicationUser, - Can have multiple accounts).
2. Create a new class for Account (Can have multiple transactions, belongs to an ApplicationUser).
3. Create a new class for Transaction (Belongs to an Account).
4. Create DBSet in DbContext, then migrate and update the database.
5. Modify the registration menu so it matches our modified ApplicationUser.
6. Modify the user profile page (Use Bootstrap).
7. Create an admin page (Restricted to admins with functions related to AccountAdmin, e.g., adding/removing user roles).
8. Create a page or component for bank accounts where account functions are included, including viewing transactions.

Reminder:
Use built-in Identity classes such as UserManager and RoleManager. If roles haven't been handled.