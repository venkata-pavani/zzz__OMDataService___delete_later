using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Extensions.Options;

namespace OMSDataService
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LdapConfig config;

        public LdapAuthenticationService(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }

        public LdapUser Login(string userName, string password)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                        searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                        searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);

                        var result = searcher.FindOne();

                        if (result != null)
                        {
                            var displayName = result.Properties[DisplayNameAttribute];
                            var samAccountName = result.Properties[SAMAccountNameAttribute];

                            return new LdapUser
                            {
                                IsValidUser = true,
                                DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                                Username = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
                            };
                        }

                        else
                        {
                            return new LdapUser
                            {
                                IsValidUser = false,
                                DisplayName = "",
                                Username = ""
                            };
                        }
                    }
                }
            }

            catch (Exception)
            {
                return new LdapUser
                {
                    IsValidUser = false,
                    DisplayName = "",
                    Username = ""
                };
            }
        }
    }
}
