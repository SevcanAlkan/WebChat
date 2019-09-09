using NGA.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Core.Validation
{
    public static class Validation
    {
        public static bool IsNull(object value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(Guid? value)
        {
            return value == null || value == Guid.Empty;
        }

        public static bool IsNullOrEmpty(string value)
        {
            return String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(int? value)
        {
            return value == null || value == 0;
        }

        public static bool IsNullOrEmpty(DateTime? value)
        {
            return value == null || DateTime.MinValue == value;
        }

        private static bool IsNullOrEmptyEnum<T>(T value) where T : new()
        {
            byte val = Convert.ToByte(value);
            if (value == null || val == 0)
                return true;

            T result = new T();
            if (!Convert.TryParseEnum<T>(val, out result))
                return false;

            return false;
        }

        public static bool ResultIsTrue(IIsResultVM value)
        {
            if (IsNull(value))
                return false;

            return value.Result;
        }

        public static bool ResultIsNotTrue(IIsResultVM value)
        {
            if (IsNull(value))
                return true;

            return !value.Result;
        }
    }
}
