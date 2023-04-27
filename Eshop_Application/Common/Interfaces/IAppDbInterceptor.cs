using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IAppDbInterceptor
    {
        void UpdateAuditableEntities(DbContext? context);
    }
}
