using System;
using System.Collections.Generic;
using DataStructures.DisjointSet;

namespace Algorithms.Graph.MinimumSpanningTree;

    // Algoritmo para determinar a floresta geradora mínima de um grafo não direcionado.
    // O algoritmo de Kruskal é um algoritmo guloso que pode determinar a
    // árvore geradora mínima ou a floresta geradora mínima de qualquer grafo
    // não direcionado. Ao contrário do algoritmo de Prim, o algoritmo de
    // Kruskal funciona em grafos que não estão conectados. Este algoritmo
    // sempre terá um tempo de execução de O(E log V), onde E é o número de
    // arestas e V é o número de vértices/nós.
public static class Kruskal {
    public static float[,] Solve(float[,] adjacencyMatrix) {
        ValidateGraph(adjacencyMatrix);

        var numNodes = adjacencyMatrix.GetLength(0);
        var set = new DisjointSet<int>();
        var nodes = new Node<int>[numNodes];
        var edgeWeightList = new List<float>();
        var nodeConnectList = new List<(int, int)>();

        for (var i = 0; i < numNodes; i++) {
            nodes[i] = set.MakeSet(i);
        }
        for (var i = 0; i < numNodes - 1; i++) {
            for (var j = i + 1; j < numNodes; j++) {
                if (float.IsFinite(adjacencyMatrix[i, j])) {
                    edgeWeightList.Add(adjacencyMatrix[i, j]);
                    nodeConnectList.Add((i, j));
                }
            }
        }

        var edges = Solve(set, nodes, edgeWeightList.ToArray(), nodeConnectList.ToArray());

        // Initialize minimum spanning tree
        var mst = new float[numNodes, numNodes];
        for (var i = 0; i < numNodes; i++) {
            mst[i, i] = float.PositiveInfinity;
            for (var j = i + 1; j < numNodes; j++) {
                mst[i, j] = float.PositiveInfinity;
                mst[j, i] = float.PositiveInfinity;
            }
        }

        foreach (var (node1, node2) in edges) {
            mst[node1, node2] = adjacencyMatrix[node1, node2];
            mst[node2, node1] = adjacencyMatrix[node1, node2];
        }
        return mst;
    }
    public static Dictionary<int, float>[] Solve(Dictionary<int, float>[] adjacencyList) {
        ValidateGraph(adjacencyList);
        var numNodes = adjacencyList.Length;
        var set = new DisjointSet<int>();
        var nodes = new Node<int>[numNodes];
        var edgeWeightList = new List<float>();
        var nodeConnectList = new List<(int, int)>();

        for (var i = 0; i < numNodes; i++) {
            nodes[i] = set.MakeSet(i);

            foreach(var (node, weight) in adjacencyList[i]) {
                edgeWeightList.Add(weight);
                nodeConnectList.Add((i, node));
            }
        }

        var edges = Solve(set, nodes, edgeWeightList.ToArray(), nodeConnectList.ToArray());

        var mst = new Dictionary<int, float>[numNodes];
        for (var i = 0; i < numNodes; i++) {
            mst[i] = new Dictionary<int, float>();
        }

        foreach (var (node1, node2) in edges) {
            mst[node1].Add(node2, adjacencyList[node1][node2]);
            mst[node2].Add(node1, adjacencyList[node1][node2]);
        }
        return mst;
    }
    private static void ValidateGraph(float[,] adj) {
        if (adj.GetLength(0) != adj.GetLength(1)) {
            throw new ArgumentException("Matrix must be square!");
        }

        for (var i = 0; i < adj.GetLength(0) - 1; i++) {
            for (var j = i + 1; j < adj.GetLength(1); j++) {
                if (Math.Abs(adj[i, j] - adj[j, i]) > 1e-6) {
                    throw new ArgumentException("Matrix must be symmetric!");
                }
            }
        }
    }

    private static void ValidateGraph(Dictionary<int, float>[] adj) {
        for (var i = 0; i < adj.Length; i++) {
            foreach (var edge in adj[i]) {
                if (!adj[edge.Key].ContainsKey(i) || Math.Abs(edge.Value - adj[edge.Key][i]) > 1e-6) {
                    throw new ArgumentException("Graph must be undirected!");
                }
            }
        }
    }
    private static (int, int)[] Solve(DisjointSet<int> set, Node<int>[] nodes, float[] edgeWeights, (int, int)[] connections) {
        var edges = new List<(int, int)>();

        Array.Sort(edgeWeights, connections);

        foreach (var (node1, node2) in connections) {
            if (set.FindSet(nodes[node1]) != set.FindSet(nodes[node2])) {
                set.UnionSet(nodes[node1], nodes[node2]);
                edges.Add((node1, node2));
            }
        }
        return edges.ToArray();
    }
}
