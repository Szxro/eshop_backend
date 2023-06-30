using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Domain.DTOS
{
    public class CurrentUserDTO
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
