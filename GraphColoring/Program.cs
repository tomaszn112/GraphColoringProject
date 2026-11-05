using System;
using System.Diagnostics;
using System.Linq;
using GraphColoring.Models;
using GraphColoring.Generators;
using GraphColoring.Algorithms;

GraphInsertion choice = GraphInsertion.GENERATOR;
Graph graph;

if(choice == GraphInsertion.GENERATOR)
{
    Console.WriteLine($"Generowanie grafu...");
    graph = GraphGenerator.GenerateRandom(500, 0.7);
    Console.WriteLine($"Wygenerowano pomyślnie. Liczba wierzchołków: {graph.VerticesCount}\n");
}
else
{
    string filePath = "instancje.txt";
    Console.WriteLine($"Wczytywanie grafu z pliku: {filePath}...");
    graph = GraphReader.ReadFromFile(filePath);
    Console.WriteLine($"Wczytano pomyślnie. Liczba wierzchołków: {graph.VerticesCount}\n");
}


// Algorytm LF:
Console.WriteLine("Uruchamianie algorytmu LF...");

IColoringAlgorithm solver = new GreedyLF();

Stopwatch sw = Stopwatch.StartNew();
int[] colors = solver.ColorGraph(graph);
sw.Stop();

int usedColors = colors.Max() + 1;

Console.WriteLine($"Zakończono. Użyte kolory: {usedColors}");
Console.WriteLine($"Czas wykonania: {sw.ElapsedMilliseconds} ms\n");

// Metaheurystyka:
Console.WriteLine("Uruchamianie Tabu Search...");

solver = new TabuSearch(colors, 1000, 15);

sw = Stopwatch.StartNew();
int[] colors2 = solver.ColorGraph(graph);
sw.Stop();

int usedColors2 = colors2.Max() + 1;

Console.WriteLine($"Zakończono. Użyte kolory: {usedColors2}");
Console.WriteLine($"Czas wykonania: {sw.ElapsedMilliseconds} ms");

enum GraphInsertion
{
    GENERATOR, READER
};