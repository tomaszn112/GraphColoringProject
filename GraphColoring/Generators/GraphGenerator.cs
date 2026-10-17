using System;
using GraphColoring.Models;

namespace GraphColoring.Generators;

public static class GraphGenerator
{
    public static Graph GenerateRandom(int vertices, double density)
    {
        var graph = new Graph(vertices);
        var rand = new Random();

        for (int i = 0; i < vertices; i++)
        {
            for (int j = i + 1; j < vertices; j++)
            {
                if (rand.NextDouble() < density)
                {
                    graph.AddEdge(i, j);
                }
            }
        }
        return graph;
    }
}