using MemberManagement.Domain.Interfaces;
using MemberManagement.Infrastructure;
using MemberManagement.Infrastructure.Repositories;
using MemberManagement.Application.Services;
using MemberManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MemberManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Add DbContext
            builder.Services.AddDbContext<MMSDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Dependency Injection for repositories/services
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<LoginService>();

            // Add Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";          // Redirect to login if unauthorized
                    options.LogoutPath = "/Account/Logout";       // Logout path
                    options.AccessDeniedPath = "/Account/Login";  // Access denied redirects to login
                    options.ExpireTimeSpan = TimeSpan.FromHours(1); // Cookie expiration
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // <-- must be before UseAuthorization
            app.UseAuthorization();

            // Default route goes to login page
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
