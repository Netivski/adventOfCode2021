using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode {


    class Day08 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day08.txt"));
            HashSet<int> iVals = new HashSet<int>();
            var acc = 0;

            for (int i = 0; i < lines.Count(); i++) {
                if (iVals.Contains(i)) {
                    Console.WriteLine("Value is {0}", acc);
                    return;
                }
                iVals.Add(i);

                var instr = lines.ElementAt(i).Split(" ");
                var offset = int.Parse(instr[1].Substring(1));
                offset = offset * (instr[1][0] == '-' ? -1 : 1);

                switch (instr[0]) {
                    case "acc":
                        acc += offset;
                        break;
                    case "jmp":
                        i += offset - 1;
                        break;
                    case "nop":
                        break;
                }
            }
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day08.txt"));
            Dictionary<int, int> iVals = new Dictionary<int, int>();
            int maxLoop = 1;
            var acc = 0;
            bool broken;


            for (int j = 0; j < lines.Count(); j++) {
                var outterInstr = lines.ElementAt(j).Split(" ");
                string[] newLines;

                if (outterInstr[0] == "acc") {
                    continue;
                } else {
                    newLines = new string[lines.Count()];
                    lines.ToList().CopyTo(newLines);
                    acc = 0;
                    iVals.Clear();
                    broken = false;

                    if (outterInstr[0] == "nop") {
                        newLines[j] = newLines[j].Replace("nop", "jmp");
                    } else if (outterInstr[0] == "jmp") {
                        newLines[j] = newLines[j].Replace("jmp", "nop");
                    }
                }

                for (int i = 0; i < newLines.Length; i++) {
                    iVals.TryAdd(i, 0);
                    iVals[i] += 1;
                    if (iVals[i] > maxLoop) {
                        broken = true;
                        break;
                    }

                    var op = newLines[i].Split(" ");
                    var offset = int.Parse(op[1].Substring(1));
                    offset = offset * (op[1][0] == '-' ? -1 : 1);

                    switch (op[0]) {
                        case "acc":
                            acc += offset;
                            break;
                        case "jmp":
                            i += offset - 1;
                            break;
                    }
                }
                if (!broken) { Console.WriteLine("Acc value is {0}", acc); return; }
            }

        }


    }
}
