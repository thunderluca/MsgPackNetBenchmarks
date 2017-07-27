using MsgPackNetBenchmarks.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static MsgPackNetBenchmarks.Shared.Helpers;

namespace MsgPackNetBenchmarks.Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            var times = GetTimes(args);

            LogBuilder.AppendLine(Environment.NewLine);
            LogBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            LogBuilder.AppendLine("********************************");
            LogBuilder.AppendLine("*********.NET FRAMEWORK*********");
            LogBuilder.AppendLine("********************************");

            LogBuilder.AppendLine("--------------------------------");
            LogBuilder.AppendLine("Testing JSON (" + times + " objects)");
            LogBuilder.AppendLine("--------------------------------");

            var totalToJson = default(double);
            var totalFromJson = default(double);

            for (int i = 0; i < times; i++)
            {
                var random = new Random(Environment.TickCount);
                var address = new Address
                {
                    Street = DefaultBigInteger.ToString("D32"),
                    City = DefaultBigInteger.ToString("D32"),
                    ZipCode = DefaultBigInteger.ToString("D32"),
                    CivicNumber = random.Next()
                };

                var person = new Person
                {
                    Name = DefaultBigInteger.ToString("D32"),
                    Surname = DefaultBigInteger.ToString("D32"),
                    Age = random.Next(),
                    Address = address
                };

                double startT = DateTime.UtcNow.ToMilliseconds();
                var b = JsonConvert.SerializeObject(person);
                double finishT = DateTime.UtcNow.ToMilliseconds();
                totalToJson = totalToJson + (finishT - startT);

                double startF = DateTime.UtcNow.ToMilliseconds();
                var obj = JsonConvert.DeserializeObject<Person>(b);
                double finishF = DateTime.UtcNow.ToMilliseconds();
                totalFromJson = totalFromJson + (finishF - startF);
            }

            LogBuilder.AppendLine($"To JSON:\n    Total: {Math.Floor(totalToJson).ToString("F2")} ms\n    Average: {Math.Round(totalToJson / times, DefaultDecimalCount).ToString($"F{DefaultDecimalCount}")} ms");
            LogBuilder.AppendLine($"From JSON:\n    Total: {Math.Floor(totalFromJson).ToString("F2")} ms\n    Average: {Math.Round(totalFromJson / times, DefaultDecimalCount).ToString($"F{DefaultDecimalCount}")} ms");

            LogBuilder.AppendLine(Environment.NewLine);

            LogBuilder.AppendLine("--------------------------------");
            LogBuilder.AppendLine("Testing MessagePack (" + times + " objects)");
            LogBuilder.AppendLine("--------------------------------");

            var totalToMsgPack = default(double);
            var totalFromMsgPack = default(double);

            for (int i = 0; i < times; i++)
            {
                var random = new Random(Environment.TickCount);
                var address = new Address
                {
                    Street = DefaultBigInteger.ToString("D32"),
                    City = DefaultBigInteger.ToString("D32"),
                    ZipCode = DefaultBigInteger.ToString("D32"),
                    CivicNumber = random.Next()
                };

                var person = new Person
                {
                    Name = DefaultBigInteger.ToString("D32"),
                    Surname = DefaultBigInteger.ToString("D32"),
                    Age = random.Next(),
                    Address = address
                };
                double startT = DateTime.UtcNow.ToMilliseconds();
                byte[] b = MsgPackSerializer.PackSingleObject(person);
                double finishT = DateTime.UtcNow.ToMilliseconds();
                totalToMsgPack = totalToMsgPack + (finishT - startT);

                double startF = DateTime.UtcNow.ToMilliseconds();
                Person obj = MsgPackSerializer.UnpackSingleObject(b);
                double finishF = DateTime.UtcNow.ToMilliseconds();
                totalFromMsgPack = totalFromMsgPack + (finishF - startF);
            }

            LogBuilder.AppendLine($"To MsgPack:\n    Total: {Math.Floor(totalToMsgPack).ToString("F2")} ms\n    Average: {Math.Round(totalToMsgPack / times, DefaultDecimalCount).ToString($"F{DefaultDecimalCount}")} ms");
            LogBuilder.AppendLine($"From MsgPack:\n    Total: {Math.Floor(totalFromMsgPack).ToString("F2")} ms\n    Average: {Math.Round(totalFromMsgPack / times, DefaultDecimalCount).ToString($"F{DefaultDecimalCount}")} ms");

            LogBuilder.AppendLine(Environment.NewLine);

            string userRoot = Environment.GetEnvironmentVariable("USERPROFILE");

            File.AppendAllText(Path.Combine(userRoot, "Desktop", "msgPackLog.txt"), LogBuilder.ToString());
        }
    }
}