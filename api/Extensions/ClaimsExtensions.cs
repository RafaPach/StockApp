using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){

        return user.Claims.SingleOrDefault(x=> x.Type.Equals("https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;

        // The above is just how we reach into the claims...precoded. 
        //And these claims come from our TokenService - Email and Username Claims
        }
    }
}