using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Enum
{
    public enum AnimalStatus : byte
    {
        NoInfo = 1,
        Good = 2,
        Normal = 3,
        Poor = 4,
        Died = 5,
    }

    public enum NestStatus : byte
    {
        NoInfo = 1,
        Good = 2,
        Normal = 3,
        Poor = 4
    }

    public enum Gender : byte
    {
        NoInfo = 1,
        Male = 2,
        Female = 3
    }

    public enum UserRole : byte
    {
        Standart = 1,
        Moderator = 2,
        Admin = 3
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
