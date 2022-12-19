using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Authorization
{
    public interface IOidcClientOptionsBuilder
    {
        public OidcClientOptions Options { get; }
        public IEnumerable<KeyValuePair<string, string>> LoginParameters { get; }
    }
}
