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
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test.txt"));
            List<Int64> fishes = new();
            foreach (string fish in lines.First().Split(',')) {
                fishes.Add(Convert.ToInt64(fish));
            }
            int count = 160;
            for (int i=0; i<count; i++)
            {
                List<Int64> newFishes = new();
                foreach (Int64 fish in fishes) {
                    if (fish == 0) {
                        newFishes.Add(6);
                        newFishes.Add(8);
                    }
                    else {
                        newFishes.Add(fish - 1);
                    }
                }
                fishes = newFishes;
            }
            Console.WriteLine("Total fishes after {0} are {1}", count, fishes.Count());
        }

        public static int GetCount(long[] fishArr, int cycles)
        {
            // First "simplification"
            for (int i = 0; i < cycles; i++)
            {
                int numNews = 0;
                for (int j = 0; j < fishArr.Length; j++)
                {
                    if (fishArr[j] == 0)
                    {
                        numNews++;
                        fishArr[j] = 6;
                    }
                    else
                    {
                        fishArr[j] -= 1;
                    }
                }
                var newFishes = new Int64[numNews];
                var oldFishes = fishArr.Length;
                Array.Resize(ref fishArr, fishArr.Length + numNews);
                while (numNews > 0)
                {
                    newFishes[numNews - 1] = 8;
                    numNews--;
                }

                newFishes.CopyTo(fishArr, oldFishes);
            }
            return fishArr.Length;
        }

        public static Dictionary<int, long> GetAgeDict() {
            return new() {
                {0,0}, {1,0}, {2,0}, {3,0}, {4,0}, {5,0}, {6,0}, {7,0}, {8,0}
            };
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day06.txt"));
            var strFishes = lines.First().Split(',');

            const int MAX_COUNT = 256;
            int count = MAX_COUNT;

            Dictionary<int, long> fishByAge = GetAgeDict();
            for (int i = 0; i < strFishes.Length; i++) {
                var val = Convert.ToInt32(strFishes[i]);
                fishByAge[val]++;
            }

            while (count > 0) {
                var newFishCount = GetAgeDict();
                foreach (var k in fishByAge) {
                    if (k.Key == 0) {
                        newFishCount[6] += k.Value;
                        newFishCount[8] += k.Value;

                    } else {
                        newFishCount[k.Key-1] += k.Value;
                    }
                }
                fishByAge = newFishCount;
                count--;
            }
            long total = 0;
            foreach(var k in fishByAge) {
                total += k.Value;
            }
            Console.WriteLine("Total fishes after {0} are {1}", count, total);
        }
    }
}
