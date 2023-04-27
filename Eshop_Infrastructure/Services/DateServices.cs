using Eshop_Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Services;

public class DateService : IDateTime
{
    public DateTime Now() => DateTime.Now;
}
