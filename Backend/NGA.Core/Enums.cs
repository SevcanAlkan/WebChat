using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Enum
{
    public enum UserStatus : byte
    {
        Online = 1,
        Idle = 2,
        DontDisturb = 3,
        Invisible = 4,
    }

    public enum HTTPMethodType : byte
    {
        Unknown = 1,
        GET = 2,
        POST = 3,
        PUT = 4,
        DELETE = 5
    }
}
