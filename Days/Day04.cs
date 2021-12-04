using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day04 {

        public static readonly string App = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static readonly string Inputs = Path.Combine(App, "Inputs");

        public class Card {
            public List<int> nums = new List<int>();
            public string index = new string(' ', 25);
            public int calledTotal = 0;
            public int cardTotal = 0;
            public bool alreadyWon = false;

            public bool HasBingo() {
                string idx = new string(index);
                
                for (int i = 0; i < 25; i += 5) {
                    if (idx.Substring(i, 5) == "xxxxx") return true;
                }
                for (int i = 0; i < 5; i++) {
                    if (String.Concat(index[i], index[i + 5], index[i + 10], index[i + 15], index[i + 20]) == "xxxxx") return true; ;
                }
                return false;
            }
        }

        public static List<Card> FillCards(string[] input) {
            List<Card> cards = new();
            Card card;

            for (int i = 2; i < input.Length; i++) {
                card = new Card();
                int count = 0;
                while (count < 5) {
                    foreach (string num in input[i + count].Split(' ')) {
                        if (num == String.Empty) continue;
                        int val = Convert.ToInt32(num);
                        card.nums.Add(val);
                        card.cardTotal += val;
                    }
                    count++;
                }
                cards.Add(card);
                i += 5;
            }
            return cards;

        }

        public static void First() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day04.txt")).ToArray();
            var numbers = lines[0].Split(',');
            List<Card> cards = FillCards(lines);

            foreach (string n in numbers) {
                int val = Convert.ToInt32(n);
                foreach(Card c in cards) {
                    if (c.nums.Contains(val)) {
                        int cardIdx = c.nums.IndexOf(val);
                        c.index = c.index.Remove(cardIdx, 1).Insert(cardIdx, "x");
                        c.calledTotal += val;
                        if (c.HasBingo()){
                            Console.WriteLine("BINGO in card {0}!", cards.IndexOf(c));
                            Console.WriteLine("Card total is {0}!", c.cardTotal-c.calledTotal);
                            Console.WriteLine("Called number is {0}!", val);
                            Console.WriteLine("Response is {0}!", (c.cardTotal-c.calledTotal)*val);
                            return;
                        }
                    }
                }
            }
        }


        public static void Second() {
            var lines = Utils.ReadLines(Path.Combine(Inputs, "day04.txt")).ToArray();
            var numbers = lines[0].Split(',');
            List<Card> cards = FillCards(lines);
            int cardsWon = 0;

            foreach (string n in numbers) {
                int val = Convert.ToInt32(n);
                foreach (Card c in cards) {
                    if (c.nums.Contains(val)) {
                        int cardIdx = c.nums.IndexOf(val);
                        c.index = c.index.Remove(cardIdx, 1).Insert(cardIdx, "x");
                        c.calledTotal += val;
                        if (c.HasBingo() && !c.alreadyWon){
                            c.alreadyWon = true;
                            if (++cardsWon == cards.Count()) {
                                Console.WriteLine("Card: {0} | Remaining numbers: {1} | Called number {2} | Result: {3}",
                                    cards.IndexOf(c), c.cardTotal - c.calledTotal, val, (c.cardTotal - c.calledTotal) * val);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
