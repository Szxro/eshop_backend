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

    public DateTime TimeStampToUTCDate(long timeStamp)
    {
        return new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc).AddSeconds(timeStamp).ToUniversalTime();
    }
}
