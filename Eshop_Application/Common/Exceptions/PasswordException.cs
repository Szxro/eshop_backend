using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Exceptions
{
    public class PasswordException : Exception
    {
        public PasswordException():base() { }
        
        public PasswordException(string message) : base(message) { }

        public PasswordException(string message,Exception exception):base(message,exception) { }
    }
}
