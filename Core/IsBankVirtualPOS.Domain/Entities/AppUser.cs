using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public bool IsMerchant { get; set; }
    }
}
