using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.fswire.streaming.api
{
    public class PusherOptions
    {
        public bool Encrypted = false;
        public IAuthorizer Authorizer = null;
    }
}
