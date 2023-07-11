using Eshop_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.Entities.UserEntities
{
    public class UserShippingInfo : AuditableEntity
    {
        public UserShippingInfo()
        {
            User = new User();
        }

        public string Address { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string CountryCode { get; set; } = null!;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
