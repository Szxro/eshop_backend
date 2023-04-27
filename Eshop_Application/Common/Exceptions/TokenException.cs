using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException() { }

        public TokenException(string message) : base(message) { }

        public TokenException(string message, Exception ex): base(message, ex) { }
    }
}
