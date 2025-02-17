using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BankApp.Components;
using BankApp.Components.Account;
using BankApp.Data;

namespace BankApp;

public class Program
{   
     public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source =app.db"));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddScoped<DbContextService>();


        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>() //adding role
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        


        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();


        //creating a scope
            using(var scope = app.Services.CreateScope()){ //this will run in a separate environment

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 

                var roles = new[] {"Admin", "User"};

                foreach(var role in roles){
                    if(!await roleManager.RoleExistsAsync(role)){
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

        using(var scope = app.Services.CreateScope()){ //this will run in a separate environment

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); 

            string email = "admin@admin.se";
            string password = "Asd123.";

            if(await userManager.FindByEmailAsync(email) == null){
                var user = new ApplicationUser();
                user.UserName = email;
                user.Email = email;
                user.SocialSecurityNumber = "20000101";

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user,"Admin");
            }
            
        }

        app.Run();
    }
    }