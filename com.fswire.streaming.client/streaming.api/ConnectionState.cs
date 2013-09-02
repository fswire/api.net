﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.fswire.streaming.api
{
    public enum ConnectionState
    {
        Initialized,
        Connecting,
        Connected,
        Unavailable,
        Failed,
        Disconnected
    }
}
