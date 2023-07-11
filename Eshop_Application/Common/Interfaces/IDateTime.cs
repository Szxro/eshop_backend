using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IDateRepository
    {
        DateTime Now();

        DateTime TimeStampToUTCDate(long timeStamp);
    }
}
