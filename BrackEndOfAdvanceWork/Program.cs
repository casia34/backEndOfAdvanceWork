using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BrackEndOfAdvanceWork;
using BrackEndOfAdvanceWork.Controllers;
using BrackEndOfAdvanceWork.Models;

namespace BrackEndOfAdvanceWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

          
            builder.Services.AddAuthentication()
  .AddScheme<AuthenticationSchemeOptions, AdminAuthenticationHandler>("BasicAuthentication", opt => { });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build());
            });

            builder.Services.AddAuthentication()
              .AddScheme<AuthenticationSchemeOptions, AdminAuthenticationHandler>("AdminAuthentication", opt => { });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAuthentication", new AuthorizationPolicyBuilder("AdminAuthentication").RequireAuthenticatedUser().Build());
            });
            builder.Services.AddDbContext<AdventureWorks2019Context>();
            builder.Services.AddHttpContextAccessor();
           
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}