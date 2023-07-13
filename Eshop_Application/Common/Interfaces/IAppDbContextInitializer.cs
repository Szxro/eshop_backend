using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IAppDbContextInitializer
    {
        Task InitializeAsync();

        Task SeedAsync();
    }
}
