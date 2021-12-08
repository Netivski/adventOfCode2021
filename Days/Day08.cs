using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode {


    class Day08 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day08.txt"));
            int[] counter = new int[9];
            foreach (string l in lines)
            {
                var outputValue = l.Split(" | ")[1].Split(' ');
                foreach (string o in outputValue)
                {
                    counter[o.Length]+=1;
                }
            }
            int total = counter[2] + counter[4] + counter[3] + counter[7];
            Console.WriteLine("Number of times of 1, 4, 7 and 8 is {0}", total);
        }

        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        /*
        1 segment
            -
        2 segments
            1
        3 sements
            7
        4 segments
            4
        5 segments (2, 3, 5)
            coreSegments = 3
            sameAsFour without CoreSegments = 5
        6 segments (0, 6, 9)
            !coreSegments = 6
            sameAsFour = 9
        7 segments
            8
        */

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day08.txt"));
            var segments = new List<string>();
            segments.AddRange(new string(' ', 10).Split(' '));
            int total = 0;

            foreach (string line in lines) {
                Dictionary<int, List<String>> uniqueCount = new();
                string[] input  = line.Split(" | ");
                foreach (var val in input[0].Split(" ")) {
                    if (!uniqueCount.ContainsKey(val.Length)) { uniqueCount.Add(val.Length, new List<string>()); }
                    uniqueCount[val.Length].Add(SortString(val));
                }
                var coreSegments = uniqueCount[2].First();
                segments[1] = coreSegments;
                segments[4] = uniqueCount[4].First();
                segments[7] = uniqueCount[3].First();
                segments[8] = uniqueCount[7].First();

                var fourWithoutCore = segments[4].Replace(coreSegments.ToCharArray()[0], ' ');
                fourWithoutCore = fourWithoutCore.Replace(coreSegments.ToCharArray()[1], ' ');
                fourWithoutCore = fourWithoutCore.Replace(" ", "");
                foreach (string digit in uniqueCount[5]) {
                    if (digit.Contains(coreSegments[0]) && digit.Contains(coreSegments[1])) {
                        segments[3] = digit;
                    }
                    else if (digit.Contains(fourWithoutCore[0]) && digit.Contains(fourWithoutCore[1])) {
                        segments[5] = digit;
                    } else {
                        segments[2] = digit;
                    }
                }
                foreach(string digit in uniqueCount[6]) {
                    if (digit.Contains(coreSegments[0]) ^ digit.Contains(coreSegments[1])) {
                        segments[6] = digit;
                    }
                    else if (segments[4].All(c => digit.Contains(c))) {
                        segments[9] = digit;
                    } else {
                        segments[0] = digit;
                    }
                }
                var inputDigits = input[1].Split(" ");
                for (int i=0; i< inputDigits.Length; i++) {
                    var digit = segments.Single(s => s == SortString(inputDigits[i]));
                    inputDigits[i] = segments.IndexOf(digit).ToString();
                }
                int digitVal = Convert.ToInt32(inputDigits[0]) * 1000
                    + Convert.ToInt32(inputDigits[1]) * 100
                    + Convert.ToInt32(inputDigits[2]) * 10
                    + Convert.ToInt32(inputDigits[3]) * 1;
                total += digitVal;
            }
            Console.WriteLine("Total is {0}", total);
        }
    }
}
