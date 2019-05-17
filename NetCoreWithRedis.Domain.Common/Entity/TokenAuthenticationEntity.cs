using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWithRedis.Domain.Common.Entity
{
    public class TokenAuthenticationEntity
    {
        public int UserId { get; set; }
        public string TokenKey { get; set; }
        public long ExpireDate { get; set; }
    }
}
