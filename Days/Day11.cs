using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Day11 {
        public static readonly string App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static readonly string Inputs = Path.Combine(App, "Inputs");

        static Point Up = new Point(0, -1);
        static Point Down = new Point(0, 1);
        static Point Left = new Point(-1, 0);
        static Point Right = new Point(1, 0);
        static Point UpLeft = new Point(-1, -1);
        static Point UpRight = new Point(1, -1);
        static Point DownLeft = new Point(-1, 1);
        static Point DownRight = new Point(1, 1);

        private static HashSet<Point> GetAdjacentPoints(Point currPt, int[][] matrix) {
            HashSet<Point> pts = new();
            int maxRows = matrix.Length;
            int maxCols = matrix[0].Length;

            pts.Add(new Point(currPt.X + Right.X, currPt.Y + Right.Y));
            pts.Add(new Point(currPt.X + Down.X, currPt.Y + Down.Y));
            pts.Add(new Point(currPt.X + Left.X, currPt.Y + Left.Y));
            pts.Add(new Point(currPt.X + Up.X, currPt.Y + Up.Y));
            pts.Add(new Point(currPt.X + UpRight.X, currPt.Y + UpRight.Y));
            pts.Add(new Point(currPt.X + DownRight.X, currPt.Y + DownRight.Y));
            pts.Add(new Point(currPt.X + DownLeft.X, currPt.Y + DownLeft.Y));
            pts.Add(new Point(currPt.X + UpLeft.X, currPt.Y + UpLeft.Y));

            HashSet<Point> final = new();
            foreach (Point p in pts) {
                if (p.X < 0 || p.X >= maxCols || p.Y < 0 || p.Y >= maxRows) continue;
                final.Add(p);
            }

            return final;
        }

        private static int X_LevelUp(int x, int y, int[][] matrix) {
            int flashes = 0;
            if (matrix[y][x] == 0) return 0;
            int newVal = (matrix[y][x] + 1) % 10;
            flashes += newVal == 0 ? 1 : 0;
            matrix[y][x] = newVal;
            if (newVal == 0) {
                var pts = GetAdjacentPoints(new Point(x, y), matrix);
                foreach (Point p in pts) {
                    if (matrix[p.Y][p.X] != 0) {
                        flashes += LevelUp(p.X, p.Y, matrix);
                    }
                }
            }

            return flashes;
        }

        private static int LevelUp(int x, int y, int[][] matrix) {
            int flashes = 0;
            matrix[y][x]++;
            flashes += matrix[y][x] == 10 ? 1 : 0;
            //Console.WriteLine("({0},{1})",x,y);
            //PrintMatrix(matrix);
            if (matrix[y][x] == 10) {
                var pts = GetAdjacentPoints(new Point(x, y), matrix);
                foreach (Point p in pts) {
                    flashes += LevelUp(p.X, p.Y, matrix);
                }
            }

            return flashes;
        }

        public static void PaintIt() {
            // Get an array with the values of ConsoleColor enumeration members.
            ConsoleColor[] colors = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));
            // Save the current background and foreground colors.
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            // Display all foreground colors except the one that matches the background.
            Console.WriteLine("All the foreground colors except {0}, the background color:",
                currentBackground);
            foreach (var color in colors) {
                if (color == currentBackground) continue;

                Console.ForegroundColor = color;
                Console.WriteLine("   The foreground color is {0}.", color);
            }

            Console.WriteLine();
            // Restore the foreground color.
            Console.ForegroundColor = currentForeground;

            // Display each background color except the one that matches the current foreground color.
            Console.WriteLine("All the background colors except {0}, the foreground color:",
                currentForeground);
            foreach (var color in colors) {
                if (color == currentForeground) continue;

                Console.BackgroundColor = color;
                Console.WriteLine("   The background color is {0}.", color);
            }

            // Restore the original console colors.
            Console.ResetColor();
            Console.WriteLine("\nOriginal colors restored...");
        }

        private static void PrintMatrix(int[][] matrix) {

            foreach (var line in matrix) {
                foreach (var col in line) {
                    Console.Write(col);
                }

                Console.WriteLine();
            }
        }

        public static void First() {
            var matrix = Utils.ReadIntMatrix(Path.Combine(Inputs, "Day11.txt"));
            int flashes = 0;
            int CYCLE_MAX = 100;
            int count = 1;
            while (count <= CYCLE_MAX) {
                int cycleFlashes = 0;
                for (int y = 0; y < matrix.Length; y++) {
                    for (int x = 0; x < matrix[y].Length; x++) {
                        cycleFlashes += LevelUp(x, y, matrix);
                    }
                }

                for (int y = 0; y < matrix.Length; y++) {
                    for (int x = 0; x < matrix[y].Length; x++) {
                        matrix[y][x] = matrix[y][x] >= 10 ? 0 : matrix[y][x];
                    }
                }

                PrintMatrix(matrix);
                flashes += cycleFlashes;
                count++;
            }

            Console.WriteLine("Total flashes after {0} cycles were {1}", count, flashes);
        }

        public static void Second() {
            var matrix = Utils.ReadIntMatrix(Path.Combine(Inputs, "Day11.txt"));
            int cycleFlashes = 0;
            int count = 0;
            while (cycleFlashes != matrix.Length * matrix[0].Length) {
                count++;
                cycleFlashes = 0;
                for (int y = 0; y < matrix.Length; y++) {
                    for (int x = 0; x < matrix[y].Length; x++) {
                        cycleFlashes += LevelUp(x, y, matrix);
                    }
                }

                for (int y = 0; y < matrix.Length; y++) {
                    for (int x = 0; x < matrix[y].Length; x++) {
                        matrix[y][x] = matrix[y][x] >= 10 ? 0 : matrix[y][x];
                    }
                }

                PrintMatrix(matrix);
                Console.ReadLine();
            }

            Console.WriteLine("Total flashes after {0} cycles were {1}", count-1, cycleFlashes);
        }
    }
}