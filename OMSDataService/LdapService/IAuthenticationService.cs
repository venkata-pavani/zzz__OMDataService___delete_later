using System;

namespace OMSDataService
{
    public interface IAuthenticationService
    {
        LdapUser Login(string userName, string password);
    }
}
