using System.Collections.Generic;

namespace GraphColoring.Models;

public class Graph
{
    public int VerticesCount { get; }
    public List<int>[] AdjacencyList { get; }

    public Graph(int vertices)
    {
        VerticesCount = vertices;
        AdjacencyList = new List<int>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            AdjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        AdjacencyList[v].Add(w);
        AdjacencyList[w].Add(v);
    }
}