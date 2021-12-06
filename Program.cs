using AdventOfCode;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
Stopwatch sw = new Stopwatch();

sw.Start();
Day06.Second();
System.Threading.Thread.Sleep(1000);
sw.Stop();
Console.WriteLine("Elapsed time [{0}s  |  {1}ms]",sw.Elapsed.TotalSeconds, sw.Elapsed.TotalMilliseconds);