using System;
using System.Collections;
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


        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt")).ToArray();
            var template = lines[0];
            Dictionary<String, char> table = new();
            for (int i = 2; i < lines.Length; i++) {
                var l = lines[i].Split(" -> ");
                table.Add(l[0], l[1][0]);
            }

            Dictionary<char, long> elemCount = new();
            long count = 40;
            for (int j = 0; j < count; j++) {
                var newTemplate = "";
                for (int i = 1; i < template.Length; i++) {
                    var pair = String.Concat(template[i - 1], template[i]);
                    newTemplate = String.Concat(newTemplate, pair[0], table[pair]);
                    if (j == 9) {
                        if (!elemCount.ContainsKey(pair[0])) elemCount.Add(pair[0], 0);
                        if (!elemCount.ContainsKey((char) table[pair])) elemCount.Add((char) table[pair], 0);
                        elemCount[pair[0]]++;
                        elemCount[(char) table[pair]]++;
                    }

                    if (i == template.Length - 1) {
                        newTemplate = String.Concat(newTemplate, pair[1]);
                        if (j == 9) {
                            if (!elemCount.ContainsKey(pair[1])) elemCount.Add(pair[1], 0);
                            elemCount[pair[1]]++;
                        }
                    }
                }
                //Console.WriteLine("After step {0}: length is {1}", j+1, newTemplate.Length );
                template = newTemplate;
            }
            
            var list = elemCount.ToList();
            list.Sort((left, right) => left.Value.CompareTo(right.Value));
            Console.WriteLine(list.Last().Value-list.First().Value);
        }

        

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day14.txt")).ToArray();
            Dictionary<String, char> table = new();
            for (int i = 2; i < lines.Length; i++) {
                var l = lines[i].Split(" -> ");
                table.Add(l[0], l[1][0]);
            }
            /*
             * Count elements
             * Count pairs
             * Iterate pairs
             *  Find elements originated
             *  Count elements (onsidering) X pairs like this generate X elements like this 
             * 
             * 
             */

            Dictionary<String, long> pairsCount = new();
            for (int i = 1; i < lines[0].Length; i++) {
                string pair = String.Concat(lines[0][i - 1], lines[0][i]);
                pairsCount[pair] = pairsCount.ContainsKey(pair) ? pairsCount[pair] + 1 : 1;
            }

            Dictionary<char, long> elemCount = new();
            foreach (char c in lines[0]) {
                elemCount[c] = elemCount.ContainsKey(c) ? elemCount[c] + 1 : 1;
            }

            int MAX_ITER = 40;
            for (int i = 1; i <= MAX_ITER; i++) {
                Dictionary<string, long> newPairs = new();
                foreach (var kvp in pairsCount) {
                    var elem = table[kvp.Key];
                    elemCount[elem] = elemCount.ContainsKey(elem) ? elemCount[elem] + kvp.Value : kvp.Value;

                    string left = String.Concat(kvp.Key[0], elem);
                    string right = String.Concat(elem, kvp.Key[1]);
                    newPairs[left] = newPairs.ContainsKey(left) ? newPairs[left] + kvp.Value : kvp.Value;
                    newPairs[right] = newPairs.ContainsKey(right) ? newPairs[right] + kvp.Value : kvp.Value;
                }

                pairsCount = newPairs;
            }
            Console.WriteLine("After {0} iterations the result is {1}", MAX_ITER, elemCount.Values.Max() - elemCount.Values.Min());
        }
    }
}