using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day13 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var directions = Utils.ReadLines(Path.Combine(Inputs, "test1.txt"));
            long earliestTimestamp = long.Parse(directions.First());
            List<long> ids = new List<long>();
            long earliestBus = 0;
            long earliestBusID = 0;

            foreach (string elem in directions.ElementAt(1).Split(',')) {
                if (elem == "x") { continue; }
                ids.Add(long.Parse(elem));
            }
            for (long i = earliestTimestamp; i < Math.Pow(earliestTimestamp, 2); i++) {
                foreach (long id in ids) {
                    if (i % id == 0) {
                        earliestBus = i;
                        earliestBusID = id;
                        break;
                    }
                }
                if (earliestBusID > 0) { break; }
            }
            Console.WriteLine("Earliest timestamp is {0} for bus id {1}. Result is {2}", earliestBus, earliestBusID, (earliestBus - earliestTimestamp) * earliestBusID);
        }

        public static void Second() {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            var directions = Utils.ReadLines(Path.Combine(Inputs, "Day13.txt"));
            string[] ids = directions.ElementAt(1).Split(',');
            List<long> busList = new List<long>();

            for (int i = 0; i < ids.Length; i++) {
                busList.Add(ids[i] == "x" ? 0 : long.Parse(ids[i]));
            }
            
            long time = busList.First();
            long step = busList.First();

            for (int i = 1; i < busList.Count; i++) {
                long b = busList[i];
                if (b == 0) continue;
                while ((time + i) % b != 0) {
                    time += step;
                }
                step = step * b;
            }
            Console.WriteLine("Earliest timestamp at which buses depart at proper offset is {0}", time);
        }
    }
}