using MemberManagement.Domain.Interfaces;
using MemberManagement.Infrastructure;
using MemberManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MemberManagement.Application.Services;


namespace MemberManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add DbContext with SQL Server
            builder.Services.AddDbContext<MMSDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            //Add Dependency Injection
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();



            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Member}/{action=MemberListPage}/{id?}"); app.Run();

        }
    }
}
