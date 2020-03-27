using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Auth
{
    public interface IIssuerValidator
    {
        string ValidateIssuer(string issuer);
    }
}
