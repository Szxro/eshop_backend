using Eshop_Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories;

public class DateRepository : IDateRepository
{
    public DateTime Now() => DateTime.Now;
}
