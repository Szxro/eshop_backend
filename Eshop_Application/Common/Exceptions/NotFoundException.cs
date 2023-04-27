using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        //Constructor Overload

        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
