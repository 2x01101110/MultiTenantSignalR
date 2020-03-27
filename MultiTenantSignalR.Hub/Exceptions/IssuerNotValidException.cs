using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Exceptions
{
    public class IssuerNotValidException : Exception
    {
        public IssuerNotValidException(string message) : base(message)
        {

        }
    }
}
