using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Components;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Components.Account;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Interfaces;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Data.Services;
using Tyuiu.Hits.Oop.FinalProject.KorobeinikovaDD.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // DbContext (единственный вызов)
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Регистрация сервисов приложения
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<ILessonService, LessonService>();
        builder.Services.AddScoped<ITestService, TestService>();

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

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // <-- Важно: добавляем поддержку ролей
        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>() // <-- добавлено
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var app = builder.Build();

        // Применяем миграции при старте (чтобы схема/данные применялись в БД)
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate(); // применяет все миграции

                // ---- Посев ролей и тестовых пользователей ----
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                string[] roles = new[] { "Teacher", "Student" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                async Task CreateUserIfNotExists(string email, string password, string role)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = email,
                            Email = email,
                            EmailConfirmed = true
                        };
                        var res = await userManager.CreateAsync(user, password);
                        if (res.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, role);
                        }
                        else
                        {
                            var logger = services.GetRequiredService<ILogger<Program>>();
                            logger.LogWarning("Ошибка при создании пользователя {Email}: {Errors}", email, string.Join(", ", res.Errors.Select(e => e.Description)));
                        }
                    }
                    else
                    {
                        if (!await userManager.IsInRoleAsync(user, role))
                            await userManager.AddToRoleAsync(user, role);
                    }
                }

                // Пароли в примере простые — для учебного проекта допустимо, в реальном — сильные пароли и безопасное хранение
                await CreateUserIfNotExists("teacher@example.com", "Password123!", "Teacher");
                await CreateUserIfNotExists("student@example.com", "Password123!", "Student");
                // ----------------------------------------------
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                // при необходимости можно пробросить исключение
                // throw;
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}

