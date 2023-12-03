using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Room {
        public List<Room> Next { get; set; }
        public string Name { get; set; }

        public bool IsSmall => !IsEnd && !IsStart && char.IsLower(Name[0]);
        public bool IsEnd => Name == "end";
        public bool IsStart => Name == "start";

        public Room(string name) {
            Name = name;
            Next = new List<Room>();
        }
    }

    class Day12 {
        private static readonly string? App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        private static readonly string Inputs = Path.Combine(App ?? string.Empty, "Inputs");

        private static Dictionary<string, Room> BuildGraph(string[] input) {
            var graph = new Dictionary<string, Room>();
            foreach (string l in input) {
                var rooms = l.Split(('-'));
                Room left;
                Room right;
                if (graph.ContainsKey(rooms[0])) {
                    left = graph[rooms[0]];
                }
                else {
                    left = new Room(rooms[0]);
                    graph.Add(left.Name, left);
                }

                if (graph.ContainsKey(rooms[1])) {
                    right = graph[rooms[1]];
                }
                else {
                    right = new Room(rooms[1]);
                    graph.Add(right.Name, right);
                }

                left.Next.Add(right);
                if (left.Name != "start" && right.Name != "end") right.Next.Add(left);
            }

            return graph;
        }

        private static int CanReachEnd(Room root, List<Room> smallRooms, int cycle = 0) {
            if (root.IsStart && cycle > 0) return 0;
            if (smallRooms.Contains(root)) return 0;
            if (root.IsSmall) smallRooms.Add(root);
            Console.WriteLine("{0}{1}", new String(' ', cycle++), root.Name);
            if (root.IsEnd) return 1;

            var res = 0;
            foreach (var child in root.Next) {
                var path = CanReachEnd(child, smallRooms, cycle);
                res += path;
            }

            if (root.IsSmall) smallRooms.Remove(root);
            return res;
        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day12.txt"));
            var nodes = BuildGraph(lines.ToArray());

            Console.WriteLine("There are {0} possible paths", CanReachEnd(nodes["start"], new List<Room>()));
        }
        
        private static int CanReachEndSecond(Room room, Dictionary<string, int> smallRooms, int cycle = 0, string source="") {
            if (room.IsStart && cycle > 0) return 0;

            if (room.IsSmall) {
                if (smallRooms.ContainsKey(room.Name)) {
                    if (smallRooms[room.Name] == 2) return 0;
                    if (smallRooms[room.Name] == 1 && smallRooms.ContainsValue(2)) return 0;
                } else smallRooms.Add(room.Name, 0);
                smallRooms[room.Name] += 1;
            }

            if (room.IsEnd) {
                //Console.WriteLine(source + "-end"); 
                return 1;
            }

            var res = 0;
            foreach (var child in room.Next) {
                var path = CanReachEndSecond(child, smallRooms, cycle++, source + "-" + room.Name);
                res += path;
            }

            if (room.IsSmall) smallRooms[room.Name] -= 1;
            return res;
        }


        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day12.txt"));
            var nodes = BuildGraph(lines.ToArray());

            Console.WriteLine("There are {0} possible paths",
                CanReachEndSecond(nodes["start"], new Dictionary<string, int>()));
        }
    }
}