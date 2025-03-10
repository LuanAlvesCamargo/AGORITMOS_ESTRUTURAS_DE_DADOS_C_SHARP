using System;

namespace Algorithms.Graph.MinimumSpanningTree;

// Classe que utiliza o algoritmo de Prim (algoritmo de Jarník) para determinar a
// árvore geradora mínima (AGM) de um grafo dado. O algoritmo de Prim é um algoritmo
// guloso que pode determinar a AGM de um grafo não direcionado ponderado em tempo
// O(V^2), onde V é o número de nós/vértices, quando se utiliza uma representação de
// matriz de adjacência.

public static class PrimMatrix {

    public static float[,] Solve(float[,] adjacencyMatrix, int start) {
        ValidateMatrix(adjacencyMatrix);

        var numNodes = adjacencyMatrix.GetLength(0);
        var mst = new float[numNodes, numNodes];
        var added = new bool[numNodes];
        var key = new float[numNodes];
        var parent = new int[numNodes];
        for (var i = 0; i < numNodes; i++) {
            mst[i, i] = float.PositiveInfinity;
            key[i] = float.PositiveInfinity;

            for (var j = i + 1; j < numNodes; j++) {
                mst[i, j] = float.PositiveInfinity;
                mst[j, i] = float.PositiveInfinity;
            }
        }

        key[start] = 0;
        for (var i = 0; i < numNodes - 1; i++) {
            GetNextNode(adjacencyMatrix, key, added, parent);
        }
        for (var i = 0; i < numNodes; i++) {
            if (i == start) {
                continue;
            }
            mst[i, parent[i]] = adjacencyMatrix[i, parent[i]];
            mst[parent[i], i] = adjacencyMatrix[i, parent[i]];
        }
        return mst;
    }
    private static void ValidateMatrix(float[,] adjacencyMatrix) {
        // Matrix should be square
        if (adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1)) {
            throw new ArgumentException("Adjacency matrix must be square!");
        }
        for (var i = 0; i < adjacencyMatrix.GetLength(0); i++) {
            var connection = false;
            for (var j = 0; j < adjacencyMatrix.GetLength(0); j++) {
                if (Math.Abs(adjacencyMatrix[i, j] - adjacencyMatrix[j, i]) > 1e-6) {
                    throw new ArgumentException("Adjacency matrix must be symmetric!");
                }

                if (!connection && float.IsFinite(adjacencyMatrix[i, j])) {
                    connection = true;
                }
            }

            if (!connection) {
                throw new ArgumentException("Graph must be connected!");
            }
        }
    }
    private static void GetNextNode(float[,] adjacencyMatrix, float[] key, bool[] added, int[] parent) {
        var numNodes = adjacencyMatrix.GetLength(0);
        var minWeight = float.PositiveInfinity;
        var node = -1;

        for (var i = 0; i < numNodes; i++) {
            if (!added[i] && key[i] < minWeight) {
                minWeight = key[i];
                node = i;
            }
        }
        added[node] = true;

        for (var i = 0; i < numNodes; i++) {
            if (!added[i] && adjacencyMatrix[node, i] < key[i]) {
                key[i] = adjacencyMatrix[node, i];
                parent[i] = node;
            }
        }
    }
}
