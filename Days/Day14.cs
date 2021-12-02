using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day14 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static ulong MaskInput(string currMask, ulong value) {
            ulong newValue = value & Convert.ToUInt64(currMask.Replace('X', '1'), 2);
            newValue = newValue | Convert.ToUInt64(currMask.Replace('X', '0'), 2);
            return newValue;
        }
        static void SetMemory(Dictionary<int, ulong> mem, string memLine, string currMask) {
            memLine = memLine.Replace("mem[", "").Replace("] = ", "=");
            int idx = int.Parse(memLine.Substring(0, memLine.IndexOf('=')));
            ulong newValue = ulong.Parse(memLine.Substring(memLine.IndexOf('=') + 1));
            if (!mem.ContainsKey(idx)) { mem.Add(idx, 0); }
            mem[idx] = MaskInput(currMask, newValue);
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day14.txt")).ToArray();
            string currMask = "";
            ulong bit36 = 68719476735;
            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();
            for (int i = 0; i < lines.Count(); i++) {
                if (lines[i].StartsWith("mask")) {
                    currMask = lines[i].Substring(7);
                } else {
                    SetMemory(memory, lines[i], currMask);
                }
            }
            ulong total = 0;
            foreach (int idx in memory.Keys) {
                total = total + (memory[idx] & bit36);
            }
            Console.WriteLine("Total is {0}", total);
        }

        static List<string> ExpandXBit(string xValue) {
            List<string> res = new List<string>();
            int xIndex = xValue.IndexOf('X');

            if (xIndex >= 0) {
                char[] str = xValue.ToCharArray();
                str[xIndex] = '1';
                string withOne = new string(str);
                str[xIndex] = '0';
                string withZero = new string(str);

                res.AddRange(ExpandXBit(withOne));
                res.AddRange(ExpandXBit(withZero));
            } else {
                res.Add(xValue);
            }
            return res;
        }

        static string AddXsToValue(string mask, ulong value) {
            char[] idxWithXs = new char[36];
            string idxAsString = Convert.ToString((long)value, 2);
            int j = idxAsString.Length - 1;

            for (int i = 35; i >= 0; i--) {
                char newChar = j < 0 ? '0' : idxAsString[j];
                if (mask[i] == 'X') {
                    idxWithXs[i] = 'X';
                } else {
                    idxWithXs[i] = newChar;
                }
                j--;
            }
            return new string(idxWithXs);
        }

        static void WriteMemoryMasked(string currMask, Dictionary<ulong, ulong> mem, string memLine) {
            memLine = memLine.Replace("mem[", "").Replace("] = ", "=");
            ulong memIndex = ulong.Parse(memLine.Substring(0, memLine.IndexOf('='))); // Memory index
            ulong value = ulong.Parse(memLine.Substring(memLine.IndexOf('=') + 1)); // Value to write
            
            int numXs = currMask.Count(c => c == 'X');
            ulong memIdxWithOneFromMask = memIndex | Convert.ToUInt64(currMask.Replace('X', '0'), 2); // Index with 1s set from mask

            var finalIndexWithXs = AddXsToValue(currMask, memIdxWithOneFromMask);
            List<string> affectedIndexes = ExpandXBit(finalIndexWithXs);

            foreach (string idxToSet in affectedIndexes) {
                mem[Convert.ToUInt64(idxToSet, 2)] = value;
            }
        }

        public static void Second() {

            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day14.txt")).ToArray();
            string currMask = "";
            Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

            for (int i = 0; i < lines.Count(); i++) {
                if (lines[i].StartsWith("mask")) {
                    currMask = lines[i].Substring(7);
                } else {
                    WriteMemoryMasked(currMask, memory, lines[i]);
                }
            }
            ulong total = 0;

            foreach (ulong idx in memory.Keys) {
                total += memory[idx];
            }
            Console.WriteLine("Total is {0}", total);
        }
    }
}