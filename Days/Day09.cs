using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode {
    class Day09 {
        private static readonly string App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private static readonly string Inputs = Path.Combine(App, "Inputs");

        private static readonly Point Up = new Point(0, -1);
        private static readonly Point Down = new Point(0, 1);
        private static readonly Point Left = new Point(-1, 0);
        private static readonly Point Right = new Point(1, 0);

        private static HashSet<Point> GetMinPoints(char[][] cave) {
            var minPts = new HashSet<Point>();
            for (int y = 0; y < cave.Length; y++) {
                for (int x = 0; x < cave[y].Length; x++) {
                    var adjacent = GetAdjacentPoints(new Point(x, y), cave);
                    var isMin = true;
                    foreach (Point adj in adjacent) {
                        if ((int) cave[y][x] < (int) cave[adj.Y][adj.X]) continue;
                        isMin = false;
                        break;
                    }

                    if (isMin) {
                        minPts.Add(new Point(x, y));
                    }
                }
            }

            return minPts;
        }

        private static HashSet<Point> GetAdjacentPoints(Point currPt, char[][] cave, bool forBasin = false) {
            HashSet<Point> pts = new();
            int maxRows = cave.Length;
            int maxCols = cave[0].Length;

            pts.Add(new Point(currPt.X + Right.X, currPt.Y + Right.Y));
            pts.Add(new Point(currPt.X + Down.X, currPt.Y + Down.Y));
            pts.Add(new Point(currPt.X + Left.X, currPt.Y + Left.Y));
            pts.Add(new Point(currPt.X + Up.X, currPt.Y + Up.Y));

            HashSet<Point> final = new();
            foreach (Point p in pts) {
                if (p.X < 0 || p.X >= maxCols || p.Y < 0 || p.Y >= maxRows) continue;
                if (forBasin && cave[p.Y][p.X] == '9') continue;
                final.Add(p);
            }

            return final;
        }

        private static HashSet<Point> GetBasinPoints(Point start, HashSet<Point> currPoints, char[][] cave) {
            var adj = GetAdjacentPoints(start, cave, true);
            var hasNew = false;
            foreach (Point p in adj) {
                if (currPoints.Contains(p)) continue;
                hasNew = true;
                currPoints.Add(p);
            }

            if (!hasNew) return currPoints;
            foreach (Point p in adj) {
                currPoints.UnionWith(GetBasinPoints(p, currPoints, cave));
            }

            return currPoints;
        }

        public static void First() {
            var lines = Utils.ReadCharMatrix(Path.Combine(Inputs, "Day09.txt"));
            var minPts = GetMinPoints(lines);
            var total = 0;
            foreach (Point p in minPts) {
                total += lines[p.Y][p.X] - '0';
            }

            Console.WriteLine("Min total is {0}, number of mins is {1}, total is {2}", total, minPts.Count,
                total + minPts.Count);
        }

        public static void Second() {
            var lines = Utils.ReadCharMatrix(Path.Combine(Inputs, "Day09.txt"));
            var minPts = GetMinPoints(lines);
            List<int> basinPos = new();
            foreach (Point p in minPts) {
                HashSet<Point> basinPts = new();
                basinPos.Add(GetBasinPoints(p, basinPts, lines).Count);
            }

            basinPos.Sort();
            Console.WriteLine("There are {0} basins", basinPos.Count);
            Console.WriteLine("Top 3 basins are {0}, {1}, {2}", basinPos[^1], basinPos[^2], basinPos[^3]);
            Console.WriteLine("Total is {0}", basinPos[^1] * basinPos[^2] * basinPos[^3]);
        }
    }
}