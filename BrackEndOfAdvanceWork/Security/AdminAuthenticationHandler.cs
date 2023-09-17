using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using BrackEndOfAdvanceWork.Models;

namespace BrackEndOfAdvanceWork
{
    public class AdminAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //private readonly SocietaContext _context;

        public AdminAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            var authoHeaderRegEx = new Regex("Basic (.*)");

            if(!authoHeaderRegEx.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Code, not properly formatted"));
            }

            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authoHeaderRegEx.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);

            var authUser = authSplit[0];
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get Password");

            using (var ctx = new AdventureWorks2019Context())
            {
                var User = ctx.People.Find(authUser); //_context.Impiegatis.Find(authUser);
                //ctx.Impiegatis.Where(employee => employee.Matricola == authUser);

                //if (User == null || authPassword != User.Password.ToString())
                //{
                //    return Task.FromResult(AuthenticateResult.Fail("User e/o Passowrd errata/e"));
                //}
            }
            var authenticatedUser = new AuthenticatedUser("AdminAuthentication", true, authUser);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
