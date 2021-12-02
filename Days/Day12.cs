using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {


    class Day12 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        class Ship {

            // Coordinate related members
            static readonly Point North = new Point(0, -1);
            static readonly Point East = new Point(1, 0);
            static readonly Point South = new Point(0, 1);
            static readonly Point West = new Point(-1, 0);
            Point[] CoordArr = new Point[] { North, East, South, West };
            Dictionary<char, Point> Coords = new Dictionary<char, Point> {
                { 'N', North }, { 'E', East }, { 'S', South }, { 'W', West }
            };

            public Point location;
            public int direction;
            public Point waypoint;
            public bool useWaypoint;

            public Ship(bool useWayPoint = false) {
                direction = 1; //default is 'East'
                location = new Point(0, 0);
                waypoint = new Point(10, -1);
                this.useWaypoint = useWayPoint;
            }

            public Point Direction { get { return CoordArr[direction]; } }

            public void SetNextDirection(bool clockwise, int numTimes) {
                int next = clockwise ? direction + numTimes : direction - numTimes;
                direction = ((next % CoordArr.Length) + CoordArr.Length) % CoordArr.Length;
            }

            public void ProcessAction(char action, int val) {
                if (!useWaypoint) {
                    if (action == 'L') {
                        Turn(false, val);
                    } else if (action == 'R') {
                        Turn(true, val);
                    } else if (action == 'F') {
                        location.Y += CoordArr[direction].Y * val;
                        location.X += CoordArr[direction].X * val;
                    } else {
                        location.X += Coords[action].X * val;
                        location.Y += Coords[action].Y * val;
                    }
                } else {
                    ProcessActionWithWayPoint(action, val);
                }
            }

            public void Turn(bool clockwise, int degrees) {
                if (!useWaypoint) {
                    SetNextDirection(clockwise, degrees / 90);
                } else {
                    TurnWithWayPoint(clockwise, degrees);
                }
            }

            private void ProcessActionWithWayPoint(char action, int val) {
                if (action == 'L') {
                    Turn(false, val);
                } else if (action == 'R') {
                    Turn(true, val);
                } else if (action == 'F') {
                    location.Y += waypoint.Y * val;
                    location.X += waypoint.X * val;
                } else {
                    waypoint.X += Coords[action].X * val;
                    waypoint.Y += Coords[action].Y * val;
                }
            }

            private void TurnWithWayPoint(bool clockwise, int degrees) {
                Point currWaypoint = new Point(waypoint.X, waypoint.Y);
                if (degrees == 90) {
                    waypoint.X = currWaypoint.Y * (clockwise ? -1 : 1);
                    waypoint.Y = currWaypoint.X * (clockwise ? 1 : -1);
                } else if (degrees == 180) {
                    waypoint.X = currWaypoint.X * -1;
                    waypoint.Y = currWaypoint.Y * -1;
                } else if (degrees == 270) {
                    waypoint.X = currWaypoint.Y * (clockwise ? 1 : -1);
                    waypoint.Y = currWaypoint.X * (clockwise ? -1 : 1);
                }
            }
        }

        public static void First() {
            var directions = Utils.ReadLines(Path.Combine(Inputs, "Day12.txt"));
            Ship ship = new Ship();

            foreach (string line in directions) {
                char action = line[0];
                int val = int.Parse(line.Substring(1));
                ship.ProcessAction(action, val);
            }
            Console.WriteLine("Ship position is: {0}", ship.location);
            Console.WriteLine("Ship direction is: {0}", ship.Direction);
            Console.WriteLine("Manhattan distance: {0}", Math.Abs(ship.location.X) + Math.Abs(ship.location.Y));
        }

        public static void Second() {
            var directions = Utils.ReadLines(Path.Combine(Inputs, "Day12.txt"));
            Ship ship = new Ship(true);

            foreach (string line in directions) {
                char action = line[0];
                int val = int.Parse(line.Substring(1));
                ship.ProcessAction(action, val);
            }
            Console.WriteLine("Ship position is: {0}", ship.location);
            Console.WriteLine("Ship waypoint is: {0}", ship.waypoint);
            Console.WriteLine("Manhattan distance: {0}", Math.Abs(ship.location.X) + Math.Abs(ship.location.Y));
        }
    }
}