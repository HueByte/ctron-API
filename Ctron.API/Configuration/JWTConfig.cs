using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ctron.API.Configuration
{
    public class JWTConfig
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}
