using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Day13 {
        public static readonly string App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static readonly string Inputs = Path.Combine(App, "Inputs");

        class Fold {
            public int X { get; set; }
            public int Y { get; set; }

            public Fold(int x, int y) {
                X = x;
                Y = y;
            }
        }

        private static void PrintSheet(char[][] sheet) {
            for (int y = 0; y < sheet.Length; y++) {
                for (int x = 0; x < sheet[y].Length; x++) {
                    Console.Write(sheet[y][x]);
                }
                Console.WriteLine();
            }
        }

        private static char[][] GetSheet(List<Point> points, int maxRows, int maxCols) {
            List<char[]> lines = new();
            ;
            for (int i = 0; i < maxRows + 1; i++) {
                char[] row = new String(' ',maxCols+1).ToCharArray();
                lines.Add(row);
            }

            char[][] sheet = lines.ToArray();
            foreach (var point in points) {
                sheet[point.Y][point.X] = '#';
            }

            return sheet;
        }

        public static char[][] FoldH(char[][] sheet, int posY) {
            List<char[]> lines = new();
            int numRows = sheet.Length-1;

            for (int y = 0; y < posY; y++) {
                char[] row = new String(' ',sheet[0].Length).ToCharArray();
                for (int x = 0; x < sheet[y].Length; x++) {
                    if (sheet[y][x] == '#' || sheet[numRows - y][x] == '#') row[x] = '#';
                }
                lines.Add(row);
            }

            return lines.ToArray();
        }
        
        public static char[][] FoldV(char[][] sheet, int posX) {
            List<char[]> lines = new(); 
            int numCols = sheet[0].Length-1;

            
            for (int y = 0; y < sheet.Length; y++) {
                char[] row = new String(' ',posX).ToCharArray();
                for (int x = 0; x < posX; x++) {
                    if (sheet[y][x] == '#' || sheet[y][numCols-x] == '#') row[x] = '#';
                }
                lines.Add(row);
            }

            return lines.ToArray();
        }

        public static int CountDots(char[][] sheet) {
            int total = 0;
            for (int y = 0; y < sheet.Length; y++) {
                for (int x = 0; x < sheet[y].Length; x++) {
                    total += sheet[y][x] == '#' ? 1 : 0;
                }
            }

            return total;
        }

        public static void First() {
            var input = Utils.ReadLines(Path.Combine(Inputs, "Day13.txt"));
            List<Point> pts = new();
            int maxX = 0, maxY = 0;
            List<Fold> folds = new();
            foreach (string line in input) {
                if (String.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("fold")) {
                    var instruct = line.Split('=');
                    int x = instruct[0].ElementAt(instruct[0].Length-1) == 'x' ? int.Parse(instruct[1]) : 0;;
                    int y = instruct[0].ElementAt(instruct[0].Length-1) == 'y' ? int.Parse(instruct[1]) : 0;
                    folds.Add(new Fold(x, y));
                }
                else {
                    var coords = line.Split(',');
                    Point p = new Point(Int32.Parse(coords[0]), Int32.Parse(coords[1]));
                    pts.Add(p);
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y > maxY) maxY = p.Y;
                }
            }

            char[][] sheet = GetSheet(pts, maxY, maxX);
            foreach (Fold f in folds) {
                if (f.X != 0) {
                    sheet = FoldV(sheet, f.X);
                }
                else {
                    sheet = FoldH(sheet, f.Y);
                }
                Console.WriteLine("# of dots after fold is {0}", CountDots(sheet));
                
            }
            PrintSheet(sheet);
            Console.ReadLine();
        }

        public static void Second() { }
    }
}