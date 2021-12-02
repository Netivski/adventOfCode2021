using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode {

    class Bag {
        public string Color;
        public HashSet<Bag> Parents;
        public Dictionary<Bag, int> Children;

        public Bag(String color) {
            Color = color;
            Parents = new HashSet<Bag>();
            Children = new Dictionary<Bag, int>();
        }

        public override int GetHashCode() {
            return Color.GetHashCode();
        }

        public override bool Equals(object obj) {
            return this.Equals(obj as Bag);
        }

        public bool Equals(Bag bag) {
            return this.Color.Equals(bag.Color);
        }
    }

    class Day07 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static HashSet<Bag> GetListOfBags(IEnumerable<string> lines) {
            HashSet<Bag> bags = new HashSet<Bag>();

            foreach (string line in lines) {
                string parsedLine = line.Replace("bags", "").Replace("bag", "").Replace(".", "");
                var rule = parsedLine.Split("contain");
                var rootColor = rule[0].Substring(0).Trim();
                Bag root;
                if (!bags.TryGetValue(new Bag(rootColor), out root)) {
                    root = new Bag(rootColor);
                }
                bags.Add(root);

                var bagsInside = rule[1].Split(',');

                for (int i = 0; i < bagsInside.Length; i++) {
                    if (bagsInside[i].Trim().StartsWith("no other")) { continue; }
                    var color = bagsInside[i].Trim().Substring(1).Trim();
                    var quant = int.Parse(bagsInside[i].Trim().Substring(0, 1));
                    Bag innerBag;
                    if (!bags.TryGetValue(new Bag(color), out innerBag)) {
                        innerBag = new Bag(color);
                    }
                    bags.Add(innerBag);
                    innerBag.Parents.Add(root);
                    root.Children.Add(innerBag, quant);
                }
            }
            return bags;
        }

        public static HashSet<Bag> GetTransitiveParents(Bag bag) {
            HashSet<Bag> p = new HashSet<Bag>();

            foreach (Bag b in bag.Parents) {
                p.Add(b);
                p.UnionWith(GetTransitiveParents(b));
            }
            return p;
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day07.txt"));
            var bags = GetListOfBags(lines);
            Bag dummy;
            if (bags.TryGetValue(new Bag("shiny gold"), out dummy)) {
                var x = GetTransitiveParents(dummy);
                Console.WriteLine("Number of root bags: {0}", x.Count);
            }
        }

        public static int CountBags(Bag bag) {
            int total = 0;
            if (bag.Children.Count == 0) { return 1; }
            total += 1;
            foreach (Bag b in bag.Children.Keys) {
                int c = CountBags(b);
                total = total + (bag.Children[b] * (c));
            }
            return total;
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day07.txt"));
            var bags = GetListOfBags(lines);
            Bag dummy;
            if (bags.TryGetValue(new Bag("shiny gold"), out dummy)) {
                var x = CountBags(dummy);
                Console.WriteLine("Number of bags: {0}", x - 1);
            }
        }
    }
}
