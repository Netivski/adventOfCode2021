using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode {
    class Day02 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static void MaskString(string str, out int min, out int max, out char ch, out string pass) {
            var hyphenPos = str.IndexOf('-');
            var semiColonPos = str.IndexOf(':');

            min = int.Parse(str.Substring(0, hyphenPos));
            max = int.Parse(str.Substring(hyphenPos + 1, semiColonPos - hyphenPos - 2));
            ch = str[semiColonPos - 1];
            pass = str.Substring(semiColonPos + 1).Trim();
        }

        static bool CompliesWithTobogan(string str, out int min, out int max, out char ch, out string pass) {
            MaskString(str, out min, out max, out ch, out pass);
            int diffSize = pass.Length - pass.Replace(ch.ToString(), string.Empty).Length;
            return (diffSize >= min && diffSize <= max);
        }

        static bool CompliesWithSled(string str, out int min, out int max, out char ch, out string pass) {
            MaskString(str, out min, out max, out ch, out pass);
            char left = pass[min - 1];
            char right = pass[max - 1];
            return (left == ch || right == ch) && (left != right);

        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day02.txt"));
            int valids = 0, min, max;
            char ch;
            string pass;

            foreach (var l in lines) {
                if (CompliesWithTobogan(l, out min, out max, out ch, out pass)) {
                    valids++;
                }
            }
            Console.WriteLine("There are {0} valid password!", valids);
        }
        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day02.txt"));
            int valids = 0, min, max;
            char ch;
            string pass;

            foreach (var l in lines) {
                if (CompliesWithSled(l, out min, out max, out ch, out pass)) {
                    valids++;
                }
            }
            Console.WriteLine("There are {0} valid password!", valids);
        }
    }
}
