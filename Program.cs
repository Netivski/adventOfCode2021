using AdventOfCode;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
Stopwatch sw = new Stopwatch();

sw.Start();
Day16.First();
sw.Stop();
Console.WriteLine("Elapsed time [{0}s  |  {1}ms]",sw.Elapsed.TotalSeconds, sw.Elapsed.TotalMilliseconds);