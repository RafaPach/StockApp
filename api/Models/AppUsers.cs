using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AppUsers : IdentityUser
    {
        // IdentityDbContext<AppUser>

        // Many to Many 
        public List<Portfolio> Porfolios {get; set;} = new List<Portfolio>();
        
    }
}