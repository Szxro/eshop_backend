using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException() : base() { }

        public ProductException(string message) : base(message) { }

        public ProductException(string message, Exception ex) : base(message, ex) { }
    }
}
