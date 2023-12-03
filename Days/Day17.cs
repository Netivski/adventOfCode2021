using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day17 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");


        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt")).ToArray();
        }

        

        public static void Second() {

            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt")).ToArray();
            
        }
    }
}