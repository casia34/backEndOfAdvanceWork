using System.Security.Claims;
using System.Security.Principal;

namespace BrackEndOfAdvanceWork
{
    public class AuthenticatedUser : IIdentity
    {
        public AuthenticatedUser(string? authenticationType, bool isAuthenticated, string? name)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
    }
}