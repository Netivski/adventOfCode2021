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

            var increases = 1;
            for (int i = 1; i < lines.Length -1; i++) {
                if (lines[i] > lines[i - 1]) increases++;
            }
            Console.WriteLine("Number of increases: {0}", increases);
        }

        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day01.txt"));
            var vals = new HashSet<int>(lines);

            var increases = 0;
            for (int i = 0; i <= lines.Length - 4; i++)
            {
                if ((lines[i+1]+lines[i+2]+lines[i+3]) > (lines[i]+lines[i+1]+lines[i+2])) increases++;
            }
            Console.WriteLine("Number of increases: {0}", increases);
        }
    }
}