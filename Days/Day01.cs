using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
    class Day01 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day01.txt"));
            var vals = new HashSet<int>(lines);
            var sumTotal = 2020;

            for (int i = 0; i < lines.Length; i++) {
                int match = sumTotal - lines[i];
                if (vals.Contains(match)) {
                    Console.WriteLine("Found it! {0}+{1}={2}", lines[i], match, sumTotal);
                    Console.WriteLine("Answer is {0}*{1}={2}", lines[i], match, lines[i] * match);
                    return;
                }
            }
        }

        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day01.txt"));

            int sumTotal = 2020;
            Array.Sort(lines);
            for (int i = 0; i < lines.Length; i++) {
                var first = lines[i];
                var left = i + 1;
                var right = lines.Length - 1;
                while (left < right) {
                    int tempTotal = first + lines[left] + lines[right];
                    if (tempTotal == sumTotal) {
                        Console.WriteLine("Found it! {0}+{1}+{2}={3}", first, lines[left], lines[right], sumTotal);
                        Console.WriteLine("Answer is {0}*{1}*{2}={3}", first, lines[left], lines[right], first * lines[left] * lines[right]);
                        return;
                    } else if (tempTotal > sumTotal) {
                        right--;
                    } else if (tempTotal < sumTotal) {
                        left++;
                    }
                }

            }
        }
    }
}
