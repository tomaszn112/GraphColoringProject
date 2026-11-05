using System;
using System.Collections.Generic;
using System.Linq;
using GraphColoring.Models;

namespace GraphColoring.Algorithms
{
    public class TabuSearch : IColoringAlgorithm
    {
        private readonly int _maxIterations;
        private readonly int _tabuTenure;
        private readonly Random _rand = new Random();
        int[] bestValidColoring;

        public TabuSearch(int[] initialColoring, int maxIterations = 10000, int tabuTenure = 15)
        {
            _maxIterations = maxIterations;
            _tabuTenure = tabuTenure;
            bestValidColoring = initialColoring;
        }

        public int[] ColorGraph(Graph graph)
        {
            int currentK = bestValidColoring.Max() + 1; 

            while (currentK > 1)
            {
                int targetK = currentK - 1;
                int[] currentColoring = (int[])bestValidColoring.Clone();

                for (int i = 0; i < graph.VerticesCount; i++)
                {
                    if (currentColoring[i] >= targetK)
                    {
                        currentColoring[i] = _rand.Next(targetK);
                    }
                }

                int[,] tabuList = new int[graph.VerticesCount, targetK];
                int bestConflicts = CountConflicts(graph, currentColoring);
                int iter = 0;

                while (iter < _maxIterations && bestConflicts > 0)
                {
                    List<int> conflictVertices = GetConflictVertices(graph, currentColoring);
                    if (conflictVertices.Count == 0) break;

                    int bestMoveV = -1;
                    int bestMoveC = -1;
                    int minNeighborConflicts = int.MaxValue;

                    foreach (int v in conflictVertices)
                    {
                        for (int c = 0; c < targetK; c++)
                        {
                            if (currentColoring[v] == c) continue;

                            int newConflicts = EvaluateMove(graph, currentColoring, v, c);
                            bool isTabu = tabuList[v, c] > iter;

                            if (newConflicts < bestConflicts || (!isTabu && newConflicts < minNeighborConflicts))
                            {
                                bestMoveV = v;
                                bestMoveC = c;
                                minNeighborConflicts = newConflicts;
                            }
                        }
                    }

                    if (bestMoveV == -1)
                    {
                        bestMoveV = conflictVertices[_rand.Next(conflictVertices.Count)];
                        bestMoveC = _rand.Next(targetK);
                    }

                    int oldColor = currentColoring[bestMoveV];
                    currentColoring[bestMoveV] = bestMoveC;

                    tabuList[bestMoveV, oldColor] = iter + _tabuTenure + _rand.Next(5);

                    if (minNeighborConflicts < bestConflicts)
                    {
                        bestConflicts = minNeighborConflicts;
                    }

                    iter++;
                }

                if (bestConflicts == 0)
                {
                    bestValidColoring = (int[])currentColoring.Clone();
                    currentK = targetK;
                }
                else 
                {
                    break;
                }
            }

            return bestValidColoring;
        }

        private int CountConflicts(Graph graph, int[] coloring)
        {
            int conflicts = 0;
            for (int u = 0; u < graph.VerticesCount; u++)
            {
                foreach (int neighbor in graph.AdjacencyList[u])
                {
                    if (u < neighbor && coloring[u] == coloring[neighbor])
                    {
                        conflicts++;
                    }
                }
            }
            return conflicts;
        }

        private List<int> GetConflictVertices(Graph graph, int[] coloring)
        {
            var conflictVertices = new HashSet<int>();
            for (int u = 0; u < graph.VerticesCount; u++)
            {
                foreach (int neighbor in graph.AdjacencyList[u])
                {
                    if (coloring[u] == coloring[neighbor])
                    {
                        conflictVertices.Add(u);
                        conflictVertices.Add(neighbor);
                    }
                }
            }
            return conflictVertices.ToList();
        }

        private int EvaluateMove(Graph graph, int[] coloring, int vertex, int newColor)
        {
            int currentConflicts = CountConflicts(graph, coloring);
            int oldColor = coloring[vertex];
            
            int delta = 0;
            foreach (int neighbor in graph.AdjacencyList[vertex])
            {
                if (coloring[neighbor] == oldColor) delta--;
                if (coloring[neighbor] == newColor) delta++;
            }

            return currentConflicts + delta;
        }
    }
}