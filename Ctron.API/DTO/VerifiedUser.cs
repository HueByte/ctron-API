using System.Collections.Generic;

namespace Ctron.API.DTO
{
    public class VerifiedUser
    {
        public string UserName { get; set; }
        public IList<string> Role { get; set; }
        public string Token { get; set; }
    }
}