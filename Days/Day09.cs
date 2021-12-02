using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode {


    class Day09 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadLongLines(Path.Combine(Inputs, "Day09.txt"));
            bool foundIt;

            for (int i = 25; i < lines.Length; i++) {
                foundIt = false;
                for (int j = i - 25; j < i - 1; j++) {
                    for (int k = j + 1; k < i; k++) {
                        if (lines[i] == lines[j] + lines[k]) {
                            foundIt = true;
                        }
                    }
                }
                if (!foundIt) {
                    Console.WriteLine("Number with non matching sum is {0}", lines[i]);
                    return;
                }
            }
        }

        static long AddSmallAndLarge(long[] lines, long val) {
            HashSet<long> vals = new HashSet<long>();

            for (int i = 0; i < lines.Length; i++) {
                vals.Add(lines[i]);
                if (vals.Sum() == val) {
                    long min = vals.Min();
                    long max = vals.Max();
                    Console.WriteLine("Found sequence with Min {0} and Max {0}. Result is {2}",
                        min, max, min + max);
                    return min + max;
                } else if (vals.Sum() > val) {
                    i = i - vals.Count() + 1;
                    vals.Clear();
                }

            }
            return 0;
        }

        public static void Second() {
            var lines = Utils.ReadLongLines(Path.Combine(Inputs, "Day09.txt"));
            AddSmallAndLarge(lines, 756008079);


        }
    }
}