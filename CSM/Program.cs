using CSM.Application.Services;
using CSM.Application.Services.TaskServices;
using CSM.Core.Interfaces;
using CSM.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Infrastructure.Data.ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Infrastructure.Data.ApplicationDbContext>();

            // Add additional services
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();

            #region Injection
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register repositories with their concrete implementations
            builder.Services.AddScoped<Core.Interfaces.ITaskCommandRepository, Infrastructure.Data.TaskCommandRepository>();
            builder.Services.AddScoped<Core.Interfaces.ITaskQueryRepository, Infrastructure.Data.TaskQueryRepository>();

            // Register use case services
            builder.Services.AddScoped<Core.Interfaces.ITaskCommandUseCase, TaskCommandService>();
            builder.Services.AddScoped<Core.Interfaces.ITaskQueryUseCase,   TaskQueryService>();

            // Register MediatR with the Core assembly where handlers reside
            builder.Services.AddMediatR(typeof(CSM.Core.UseCases.Queries.GetTaskListQuery).Assembly);
            #endregion

            // Configure Identity options
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            // Configure application cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            // Configure session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}