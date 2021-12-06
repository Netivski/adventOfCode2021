using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day05 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static List<Point> ListOfPoints(String inputLine, bool hasDiagonals=false) {
            var ptStr = inputLine.Split(" -> ");
            var ptAStr = ptStr[0].Split(',');
            var ptBStr = ptStr[1].Split(',');
            Point ptA = new Point(Convert.ToInt32(ptAStr[0]), Convert.ToInt32(ptAStr[1]));
            Point ptB = new Point(Convert.ToInt32(ptBStr[0]), Convert.ToInt32(ptBStr[1]));

            List<Point> res = new List<Point>();

            if (!hasDiagonals && !(ptA.X == ptB.X || ptA.Y == ptB.Y)) {
                return res;
            }
                
            int fromY = Math.Min(ptA.Y, ptB.Y);
            int toY = Math.Max(ptA.Y, ptB.Y);
            int fromX = Math.Min(ptA.X, ptB.X);
            int toX = Math.Max(ptA.X, ptB.X);

            var leftPt = ptA.X <= ptB.X ? ptA : ptB;
            var rightPt = leftPt == ptA ? ptB : ptA;
            while (leftPt.X <= rightPt.X) {
                res.Add(leftPt);
                if (leftPt == rightPt) break;
                if (leftPt.Y < rightPt.Y) leftPt.Y++;
                else if (leftPt.Y > rightPt.Y) leftPt.Y--;
                if (leftPt.X < rightPt.X) leftPt.X++;
            }
            return res;
        }

        public static int CountIntersections(bool hasDiagonals) {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day05.txt"));
            Dictionary<Point, int> pts = new();
            foreach (var l in lines) {
                var listOfPoints = ListOfPoints(l, hasDiagonals);
                foreach (var p in listOfPoints) {
                    if (pts.ContainsKey(p)) pts[p]++;
                    else
                    {
                        pts.Add(p, 1);
                    }
                }
            }
            int count = 0;
            foreach (var p in pts) {
                if (p.Value > 1) count++;
            }
            return count;
        }

        public static void First() {
            Console.WriteLine("Number of intersections is {0}", CountIntersections(false /*hasDiagonals=false*/));
        }

        public static void Second() {
            Console.WriteLine("Number of intersections is {0}", CountIntersections(true /*hasDiagonals=true*/));
        }
    }
}
