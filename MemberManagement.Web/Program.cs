using MemberManagement.Domain.Interfaces;
using MemberManagement.Infrastructure;
using MemberManagement.Infrastructure.Repositories;
using MemberManagement.Application.Services;
using Microsoft.EntityFrameworkCore;

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
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Dependency Injection
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();

            // Add DI for Branch
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            builder.Services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();

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

            // ?? No authentication / authorization

            // Default route (go straight to Home)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Member}/{action=MemberListPage}/{id?}");

            app.Run();
        }
    }
}
