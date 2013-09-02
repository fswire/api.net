using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.fswire.streaming.api
{
    public class PrivateChannel : Channel
    {
        public PrivateChannel(string channelName, Pusher pusher) : base(channelName, pusher) { }
    }
}
