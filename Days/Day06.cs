using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day06 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var groupYesses = new HashSet<char>();
            int totalYesses = 0;

            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day06.txt"));
            foreach(var l in lines) {
                if (l.Length==0) {
                    totalYesses += groupYesses.Count;
                    groupYesses.Clear();
                }
                for (int i=0; i<l.Length;i++) {
                    groupYesses.Add(l[i]);
                }
            }
            totalYesses += groupYesses.Count;
            Console.WriteLine("Number of yesses are {0}", totalYesses);
        }

        static int CountMaxedEntries(Dictionary<char, int> dict, int maxValue) {
            int maxedEntries = 0;
            foreach (char ch in dict.Keys) {
                if (dict[ch] == maxValue) {
                    maxedEntries++;
                }
            }
            return maxedEntries;
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day06.txt"));

            var yessesCount = new Dictionary<char, int>();
            int groupTotal = 0, peopleInGroup = 0;

            foreach (var l in lines) {
                if (l.Length == 0) {
                    groupTotal += CountMaxedEntries(yessesCount, peopleInGroup);
                    peopleInGroup = 0;
                    yessesCount.Clear();
                    continue;
                }
                for (int i = 0; i < l.Length; i++) {
                    if (!yessesCount.ContainsKey(l[i])) {
                        yessesCount.Add(l[i], 0);
                    }
                    yessesCount[l[i]]++;
                }
                peopleInGroup++;
            }
            groupTotal += CountMaxedEntries(yessesCount, peopleInGroup);
            Console.WriteLine("Number of consistent yesses across groups are {0}", groupTotal);
        }
    }
}
