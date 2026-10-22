using System;
using System.IO;
using GraphColoring.Models;

namespace GraphColoring.Generators;

public static class GraphReader
{
    public static Graph ReadFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        
        int verticesCount = int.Parse(lines[0].Trim());
        Graph graph = new Graph(verticesCount);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length >= 2)
            {
                int u = int.Parse(parts[0]) - 1;
                int v = int.Parse(parts[1]) - 1;
                
                graph.AddEdge(u, v);
            }
        }
        
        return graph;
    }
}