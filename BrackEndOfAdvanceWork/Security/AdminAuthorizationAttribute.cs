using Microsoft.AspNetCore.Authorization;

namespace BrackEndOfAdvanceWork
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        public AdminAuthorizationAttribute()
        {
            Policy = "AdminAuthentication";
        }
    }
}
