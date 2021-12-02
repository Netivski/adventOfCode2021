using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day10 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static void First() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "Day10.txt"));
            HashSet<int> l = new HashSet<int>(lines);

            List<int> list = new List<int>(lines);
            list.Sort();

            int currJolts = 0;
            int[] diffCounter = new int[3];

            for (int i = 0; i < list.Count(); i++) {
                if (list[i] == currJolts + 1) {
                    diffCounter[0]++;
                    currJolts = list[i];
                } else if (list[i] == currJolts + 2) {
                    diffCounter[1]++;
                    currJolts = list[i];
                } else if (list[i] == currJolts + 3) {
                    diffCounter[2]++;
                    currJolts = list[i];
                }
            }
            Console.WriteLine("Difference of 1 count is {0}", diffCounter[0]);
            Console.WriteLine("Difference of 3 count is {0}", diffCounter[2] + 1);
        }
        static long CountIt(List<int> list) {
            Dictionary<int, long> sumOfParentsConns = new Dictionary<int, long>();
            sumOfParentsConns[list.Count() - 1] = 1;

            for (int i = list.Count - 2; i >= 0; i--) {
                long currCount = 0;
                for (int j = i + 1; j < list.Count; j++) {
                    if (!(list[j] - list[i] <= 3)) { break; }
                    currCount += sumOfParentsConns[j];
                }
                sumOfParentsConns[i] = currCount;
            }
            return sumOfParentsConns[0];
        }

        public static void Second() {
            var lines = Utils.ReadIntLines(Path.Combine(Inputs, "test1.txt"));
            HashSet<int> l = new HashSet<int>(lines);

            List<int> list = new List<int>(lines);
            list.Add(0);
            list.Sort();
            list.Add(list.Last() + 3);

            long total = 0;
            total = CountIt(list);

            Console.WriteLine("Total sequences: {0}", total);

        }
    }
}