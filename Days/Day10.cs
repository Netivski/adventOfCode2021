using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Day10 {
        private static readonly string? App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadCharMatrix(Path.Combine(Inputs, "day10.txt"));
            
            Dictionary<char, char> counterParts = new() {{'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}}; 
            Dictionary<char, int> failPts = new() {{')', 3}, {']', 57}, {'}', 1197}, {'>', 25137}};
            Dictionary<char, int> completePts = new() {{')', 1}, {']', 2}, {'}', 3}, {'>', 4}};

            int total = 0;
            List<long> totalComplete = new();

            foreach (var line in lines) {
                Stack<char> stack = new();
                bool isConsistent = true;
                foreach (var chr in line) {
                    switch (chr) {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(counterParts[chr]);
                            break;
                        case ')':
                        case ']':
                        case '}':
                        case '>':
                            if (stack.Peek() != chr) {
                                total += failPts[chr];
                                isConsistent = false;
                            }
                            else {
                                stack.Pop();
                            }
                            break;
                    }
                    if (!isConsistent) break;
                }

                if (!isConsistent) continue;
                long acc = 0;
                while (stack.Count > 0) {
                    acc = (acc * 5) + completePts[stack.Pop()];
                }
                totalComplete.Add(acc);
            }
            totalComplete.Sort();
            Console.WriteLine("Total is {0}", total);
            Console.WriteLine("Total autocomplete is {0}", totalComplete.ElementAt(totalComplete.Count/2));
        }

        public static void Second() { }
    }
}