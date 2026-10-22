using System;
using System.Diagnostics;
using System.Linq;
using GraphColoring.Models;
using GraphColoring.Generators;
using GraphColoring.Algorithms;

string filePath = "instancje.txt";

Console.WriteLine($"Wczytywanie grafu z pliku: {filePath}...");
Graph graph = GraphReader.ReadFromFile(filePath);

Console.WriteLine($"Wczytano pomyślnie. Liczba wierzchołków: {graph.VerticesCount}");
Console.WriteLine("Uruchamianie algorytmu LF...");

IColoringAlgorithm solver = new GreedyLF();

Stopwatch sw = Stopwatch.StartNew();
int[] colors = solver.ColorGraph(graph);
sw.Stop();

int usedColors = colors.Max() + 1;

Console.WriteLine($"Zakończono. Użyte kolory: {usedColors}");
Console.WriteLine($"Czas wykonania: {sw.ElapsedMilliseconds} ms");