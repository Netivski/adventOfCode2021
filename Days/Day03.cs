using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace AdventOfCode {
    class Day03 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        
        public class Counter
        {
            public double ones = 0;
            public double zeros = 0;
        }

        public static double[] FindVals(Counter[] counters) {
            var res = new double[2];
            for (int i=0;i< counters.Length; i++) {
                int idx = counters.Length - i - 1;
                double power = Math.Pow(2, idx);
                double min = 0, max = 0;
                if (counters[i].ones > counters[i].zeros) {
                    min += power * 0;
                    max += power * 1;
                } else {
                    min += power * 1;
                    max += power * 0;
                }
                res[0] += max;
                res[1] += min;
            }
            return res;
        }

        public static void First()
        {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day03.txt"));
            var counter = new Counter[lines.First().Length];
            var len = lines.First().Length;
            foreach (var l in lines)
            {
                for (int i = 0; i < l.Length; i++)
                {
                    if (counter[i] == null) counter[i] = new Counter();
                    if (l[i] == '1') counter[i].ones++;
                    else counter[i].zeros++;
                }
            }

            var res = FindVals(counter);
            Console.WriteLine("Gamma is {0}", res[0]);
            Console.WriteLine("Epsilon is {0}", res[1]);
            Console.WriteLine("Result is {0}", res[0] * res[1]);
        }

        public static List<string> GetRatingPerCol(List<string> lines, int col, Kind kind)
        {
            var ones = new List<string>();
            var zeros = new List<string>();
            foreach (var l in lines)
            {
                if (l[col] == '1') ones.Add(l);
                else zeros.Add(l);
            }
            if (kind is Kind.Oxygen) 
                return (ones.Count() >= zeros.Count()) ? ones : zeros;
            else
                return (zeros.Count() <= ones.Count()) ? zeros : ones;
        }

        public enum Kind { Oxygen, CO2 }

        public static string GetRating(List<string> lines, Kind kind)
        {
            int i = 0;
            var currLines = lines.ToArray();
            var wordCount = lines[0].Length;
            while (currLines.Length > 1 && i < wordCount)
            {
                currLines = GetRatingPerCol(currLines.ToList(), i, kind).ToArray();
                i++;
            }
            return currLines[0];
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day03.txt"));
            var oxVal = GetRating(lines.ToList(), Kind.Oxygen);
            var co2Val = GetRating(lines.ToList(), Kind.CO2);



            Console.WriteLine("Oxygen is {0}", Convert.ToInt64(oxVal, 2));
            Console.WriteLine("CO2 is {0}", Convert.ToInt64(co2Val, 2));
        }
    }
}
