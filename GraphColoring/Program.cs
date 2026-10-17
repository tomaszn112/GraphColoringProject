using System;
using System.Diagnostics;
using System.Linq;
using GraphColoring.Models;
using GraphColoring.Generators;
using GraphColoring.Algorithms;

int vertices = 500;
double density = 0.5;

Console.WriteLine($"Generowanie grafu: {vertices} wierzchołków, gęstość {density}...");
Graph graph = GraphGenerator.GenerateRandom(vertices, density);

Console.WriteLine("Uruchamianie algorytmu LF...");
IColoringAlgorithm solver = new GreedyLF();

Stopwatch sw = Stopwatch.StartNew();
int[] colors = solver.ColorGraph(graph);
sw.Stop();

int usedColors = colors.Max() + 1;

Console.WriteLine($"Zakończono. Użyte kolory: {usedColors}");
Console.WriteLine($"Czas wykonania: {sw.ElapsedMilliseconds} ms");