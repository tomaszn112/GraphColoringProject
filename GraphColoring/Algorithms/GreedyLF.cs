using System;
using System.Linq;
using GraphColoring.Models;

namespace GraphColoring.Algorithms;

public class GreedyLF : IColoringAlgorithm
{
    public int[] ColorGraph(Graph graph)
    {
        int n = graph.VerticesCount;
        int[] resultColors = new int[n];
        Array.Fill(resultColors, -1);

        var sortedVertices = Enumerable.Range(0, n)
            .OrderByDescending(v => graph.AdjacencyList[v].Count)
            .ToArray();

        bool[] available = new bool[n];
        Array.Fill(available, true);

        foreach (int u in sortedVertices)
        {
            foreach (int neighbor in graph.AdjacencyList[u])
            {
                if (resultColors[neighbor] != -1)
                {
                    available[resultColors[neighbor]] = false;
                }
            }

            int color;
            for (color = 0; color < n; color++)
            {
                if (available[color]) break;
            }

            resultColors[u] = color;

            Array.Fill(available, true);
        }

        return resultColors;
    }
}