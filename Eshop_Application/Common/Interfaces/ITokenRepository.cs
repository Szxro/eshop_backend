using Eshop_Domain.Entities.TokenEntities;
using Eshop_Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
