using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Utils {
        public static IEnumerable<string> ReadLines(string path) {
            return File.ReadLines(path);
        }

        public static int[] ReadIntLines(string path) {

            var lines = File.ReadAllLines(path);
            int[] results = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                results[i] = int.Parse(lines[i]);
            }
            return results;
        }

        public static long[] ReadLongLines(string path) {

            var lines = File.ReadAllLines(path);
            long[] results = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                results[i] = long.Parse(lines[i]);
            }
            return results;
        }

        public static char[][] ReadCharMatrix(string path) {

            IList<char[]> lines = new List<char[]>();
            int numCols;

            using (var s = new StreamReader(path)) {
                string line;
                while ((line = s.ReadLine()) != null) {
                    numCols = line.Length;
                    lines.Add(line.ToCharArray());
                }
            }
            return lines.ToArray();
        }

        public static IList<Hashtable> ReadPassports(string path) {

            IList<Hashtable> list = new List<Hashtable>();

            var lines = File.ReadAllText(path).Split("\n\n");
            foreach (string l in lines) {
                var h = new Hashtable();
                var passport = l.Split("\n");
                foreach (string p in passport) {
                    var fields = p.Split(" ");
                    foreach (string f in fields) {
                        h.Add(f.Substring(0, 3), f.Substring(4));
                    }
                }
                list.Add(h);
            }
            return list;
        }
    }
}
