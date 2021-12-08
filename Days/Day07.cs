using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode {


    class Day07 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt"));
            var crabsStr = lines.First().Split(',');
            var crabs = new int[crabsStr.Length];
            var minVal = Int32.MaxValue;
            int maxVal = 0;

            var minFuel = Int32.MaxValue;

            for (int i = 0; i < crabsStr.Length; i++) {
                crabs[i] = Convert.ToInt32(crabsStr[i]);
                if (crabs[i] > maxVal) maxVal = crabs[i];
                if (crabs[i] < minVal) minVal = crabs[i];
            }

            for (int i=minVal; i<=maxVal; i++) { 
                var fuel = 0;
                for (int j=0; j<crabsStr.Length; j++) {
                    fuel += Math.Abs(crabs[j]-i);
                    if (fuel >= minFuel) continue;
                }
                if (fuel < minFuel) minFuel = fuel;
            }
            Console.WriteLine("MinFuel is {0}", minFuel);
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day07.txt"));
            var crabsStr = lines.First().Split(',');
            var minFuel = Int32.MaxValue;
            var crabs = new int[crabsStr.Length];
            var minVal = Int32.MaxValue;
            int maxVal = 0;

            for (int i=0; i<crabsStr.Length; i++) {
                crabs[i] = Convert.ToInt32(crabsStr[i]);
                if (crabs[i] > maxVal) maxVal = crabs[i];
                if (crabs[i] < minVal) minVal = crabs[i];
            }
            for (int i = minVal; i <= maxVal; i++) {
                var fuel = 0;
                for (int j = 0; j < crabs.Length; j++) {
                    int moves = Math.Abs(Convert.ToInt32(crabsStr[j])-i);
                    for (int k=1; k<=moves; k++) {
                        fuel += k;
                    }
                    if (fuel >= minFuel) continue;
                }
                if (fuel < minFuel) minFuel = fuel;
            }
            Console.WriteLine("MinFuel is {0}", minFuel);
        }
    }
}
