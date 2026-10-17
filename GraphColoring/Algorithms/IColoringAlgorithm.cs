using GraphColoring.Models;

namespace GraphColoring.Algorithms;

public interface IColoringAlgorithm
{
    int[] ColorGraph(Graph graph);
}