using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day04 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public static bool CheckPassport(Hashtable passport) {
            if (passport.ContainsKey("byr") 
                && passport.ContainsKey("iyr") 
                && passport.ContainsKey("eyr") 
                && passport.ContainsKey("hgt") 
                && passport.ContainsKey("hcl") 
                && passport.ContainsKey("ecl") 
                && passport.ContainsKey("pid")) {
                return true;
            }
            return false;
        }

        public static bool DoubleCheckPassport(Hashtable passport) {
            if ((passport.ContainsKey("byr") && int.Parse(passport["byr"].ToString()) >= 1920 && int.Parse(passport["byr"].ToString()) <= 2002)
                && (passport.ContainsKey("iyr") && int.Parse(passport["iyr"].ToString()) >= 2010 && int.Parse(passport["iyr"].ToString()) <= 2020)
                && (passport.ContainsKey("eyr") && int.Parse(passport["eyr"].ToString()) >= 2020 && int.Parse(passport["eyr"].ToString()) <= 2030)
                && (passport.ContainsKey("hgt"))
                && (passport.ContainsKey("hcl"))
                && (passport.ContainsKey("ecl"))
                && (passport.ContainsKey("pid") && passport["pid"].ToString().Length == 9)) {

                var hcl = passport["hcl"].ToString();
                if (!(hcl.StartsWith('#') && hcl.Length == 7)) {
                    return false;
                }
                var ecl = passport["ecl"].ToString();
                if (!(ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")) {
                    return false;
                }

                var hgt = passport["hgt"].ToString();
                if (hgt.EndsWith("cm") || hgt.EndsWith("in")) {
                    var isHclCm = hgt.EndsWith("cm");
                    var val = int.Parse(hgt.Substring(0, hgt.Length - 2));
                    if (isHclCm) {
                        return (val >= 150 && val <= 193);
                    } else {
                        return (val >= 59 && val <= 76);
                    }
                }

            }
            return false;
        }

        public static void First() {
            var s = Utils.ReadPassports(Path.Combine(Inputs, "Day04.txt"));
            int valids = 0, dblValids = 0;
            foreach(var passport in s) {
                if (CheckPassport(passport)) { valids++; }
                if (DoubleCheckPassport(passport)) { dblValids++; }
            }
            Console.WriteLine("Valid passports: {0}", valids);
        }
        public static void Second() {
            var s = Utils.ReadPassports(Path.Combine(Inputs, "Day04.txt"));
            int valids = 0, dblValids = 0;
            foreach (var passport in s) {
                if (CheckPassport(passport)) { valids++; }
                if (DoubleCheckPassport(passport)) { dblValids++; }
            }
            Console.WriteLine("Double valid passports: {0}", dblValids);
        }
    }
}
