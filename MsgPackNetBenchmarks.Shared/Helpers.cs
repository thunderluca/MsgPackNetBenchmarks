using MsgPackNetBenchmarks.Shared.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MsgPack.Serialization;

namespace MsgPackNetBenchmarks.Shared
{
    public static class Helpers
    {
        public static MessagePackSerializer<Person> MsgPackSerializer = SerializationContext.Default.GetSerializer<Person>();
        public const int DefaultTimes = 200000;

        public static BigInteger DefaultBigInteger = BigInteger.Parse("19031457927716431901821234567890");

        public static int DefaultDecimalCount = 4;

        public static int GetTimes(string[] args)
        {
            if (args == null || args.Length == 0) return DefaultTimes;

            var result = int.TryParse(args[0], out int times);

            return result ? times : DefaultTimes;
        }

        public static StringBuilder LogBuilder = new StringBuilder();

        public static double ToMilliseconds(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
