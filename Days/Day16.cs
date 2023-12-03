using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode {
    class Day16 {
        public static readonly string App =
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static readonly string Inputs = Path.Combine(App, "Inputs");


        /*
         * 3 - packet version
         * 3 - packet type id
         *
         * id
         *  4 - literal value (values with multiples of 4 bits)
         *
         * 110 100 1 0111 1 1110 0 0101 000
         * VVV TTT A AAAA B BBBB C CCCC
         *
         *  6 - operator packet
         *
         * 001 110 0 000000000011011 11010001010 0101001000100100 0000000
         * VVV TTT I LLLLLLLLLLLLLLL AAAAAAAAAAA BBBBBBBBBBBBBBBB
         *
         *      Type ID
         *          0 (15bits = length of sub-packets contained)
         *          1 (11bits = number of subpackets
         *
         * 111 011 1 00000000011 01010000001 10010000010 00110000011 00000
         * VVV TTT I LLLLLLLLLLL AAAAAAAAAAA BBBBBBBBBBB CCCCCCCCCCC
         */

        public static string Decode(string hexInput) {
            string bin = "";
            foreach (char hex in hexInput) {
                bin = String.Concat(bin, Convert.ToString(Convert.ToInt32(hex.ToString(), 16), 2).PadLeft(4, '0'));
            }

            return bin;
        }

        public static long DecodeToInt(string hexInput) {
            return Convert.ToInt64(hexInput, 2);
        }

        public static KeyValuePair<string, bool> ProcessNibble(string nibble) {
            return new((nibble.Substring(1, 4)), nibble[0] == '1');
        }

        public static KeyValuePair<int, List<Packet>> Parse(string bin, bool singlePacket = false) {
            var packets = new List<Packet>();
            bool more = true;
            int pointer = 0;
            while (more) {
                int version = Convert.ToInt32(bin.Substring(pointer, 3), 2);
                int type = Convert.ToInt32(bin.Substring(pointer + 3, 3), 2);
                pointer += 6;
                if (type == 4) { // literal
                    Console.WriteLine("Literal packet with version {0}", version);
                    string value = "";
                    bool hasMore = true;
                    int groupCount = 0;
                    while (hasMore) {
                        if (bin.Length - pointer < 5) break;
                        groupCount++;
                        var group = ProcessNibble(bin.Substring(pointer, 5));
                        value = String.Concat(value, group.Key);
                        hasMore = group.Value;
                        pointer += 5;
                    }

                    long val = DecodeToInt(value);
                    packets.Add(new Packet() {Type = PacketType.Literal, Value = val, Version = version, TypeId = type});
                }
                else {
                    int id = int.Parse(bin[pointer].ToString());
                    var currPacket = new Packet() {Type = PacketType.Operator, Value = 0, Version = version, Id = id, TypeId = type};
                    packets.Add(currPacket);
                    pointer++;
                    if (id == 0) {
                        Console.WriteLine("Operator packet type 0 with version {0}", version);
                        int len = Convert.ToInt32(bin.Substring(pointer, 15), 2);
                        pointer += 15;
                        var inner = Parse(bin.Substring(pointer, len));
                        currPacket.child.AddRange(inner.Value);
                        //packets.AddRange(inner.Value);
                        pointer += inner.Key;
                    }
                    else {
                        Console.WriteLine("Operator packet type 1 with version {0}", version);
                        int len = Convert.ToInt32(bin.Substring(pointer, 11), 2);
                        pointer += 11;
                        for (int i = 0; i < len; i++) {
                            var inner = Parse(bin.Substring(pointer), true);
                            currPacket.child.AddRange(inner.Value);
                            //packets.AddRange(inner.Value);
                            pointer += inner.Key;
                        }
                    }
                }

                if (singlePacket) break;
                if (pointer + 7 >= bin.Length) more = false;
            }

            return new(pointer, packets);
        }


        public enum PacketType {
            Literal,
            Operator
        }

        public class Packet {
            public List<Packet> child;
            public int Version { get; set; }
            public int Id { get; set; }
            public int TypeId { get; set; }
            public PacketType Type { get; set; }

            private long val = 0;

            public long Value {
                get {
                    switch (TypeId) {
                        case 0: //sum
                            foreach (var p in child) val += p.Value;
                            break;
                        case 1: //product
                            val = 1;
                            foreach (var p in child) val *= p.Value;
                            break;
                        case 2: //minimum
                            val = long.MaxValue;
                            foreach (var p in child)  {
                                if (p.Value < val) val = p.Value;
                            }
                            break;
                        case 3: //maximum
                            foreach (var p in child) {
                                if (p.Value > val) val = p.Value;
                            }
                            break;
                        case 5: //greater than
                            val = (child[0].Value > child[1].Value) ? 1 : 0;
                            break;
                        case 6: //less than
                            val = (child[0].Value < child[1].Value) ? 1 : 0;
                            break;
                        case 7: //equal to
                            val = (child[0].Value == child[1].Value) ? 1 : 0;
                            break;
                    }

                    return val;
                }
                set { val = value; }
            }

            public Packet() {
                child = new();
            }
        }


        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "Day16.txt")).ToArray();
            var bin = Decode(lines[0]);

            var packets = Parse(bin).Value;
            int acc = 0;
            foreach (var p in packets) {
                Console.WriteLine("Found packet of type {0} with version {1} and value {2}",
                    p.Type == PacketType.Literal ? "Literal" : "Operator", p.Version, p.Value);
                acc += p.Version;
            }

            Console.WriteLine("Total is {0}", acc);
        }

        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "test3.txt")).ToArray();
            var bin = Decode(lines[0]);

            var packets = Parse(bin).Value;
            int acc = 0;
            Console.WriteLine("Outermost packet has value {0}", packets[0].Value);
        }
    }
}