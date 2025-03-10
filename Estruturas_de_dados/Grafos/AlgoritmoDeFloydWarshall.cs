using System;
using DataStructures.Graph;

namespace Algoritmos.Grafos;

    // Algoritmo de Floyd-Warshall em um grafo direcionado ponderado.

public class FloydWarshall<T> {

    // Executa o algoritmo de Floyd-Warshall para encontrar os caminhos mais curtos entre todos os pares de vértices.
    // Grafo direcionado ponderado no qual executar o algoritmo.
    // Uma matriz 2D que representa os caminhos mais curtos entre quaisquer dois vértices.
    // Se não houver caminho entre dois vértices, o valor double.PositiveInfinity é colocado na matriz.
    

    public double[,] Executar(GrafoDirecionadoPonderado<T> grafo) {
        // Configura a matriz de distâncias iniciais com base no grafo de entrada
        var distancias = ConfigurarDistancias(grafo);
        // Obtém o número de vértices no grafo
        var quantidadeVertices = distancias.GetLength(0);
        // Loop triplo para calcular os caminhos mais curtos
        for (var k = 0; k < quantidadeVertices; k++) {
            for (var i = 0; i < quantidadeVertices; i++) {
                for (var j = 0; j < quantidadeVertices; j++) {
                    // Verifica se o caminho através do vértice k é mais curto do que o caminho atual entre i e j
                    distancias[i, j] = distancias[i, j] > distancias[i, k] + distancias[k, j]
                        ? distancias[i, k] + distancias[k, j]
                        : distancias[i, j];
                }
            }
        }
        // Retorna a matriz de distâncias com os caminhos mais curtos calculados
        return distancias;
    }


    private double[,] ConfigurarDistancias(GrafoDirecionadoPonderado<T> grafo) {
        // Cria uma nova matriz de distâncias com o mesmo tamanho que o número de vértices no grafo
        var distancias = new double[grafo.Contagem, grafo.Contagem];

        // Preenche a matriz de distâncias com os pesos das arestas do grafo
        for (int i = 0; i < distancias.GetLength(0); i++) {
            for (var j = 0; j < distancias.GetLength(0); j++) {
                // Obtém a distância entre os vértices i e j do grafo
                var distancia = grafo.DistanciaAdjacente(grafo.Vertices[i]!, grafo.Vertices[j]!);

                // Se houver uma aresta entre os vértices, a distância é igual ao peso da aresta
                // Caso contrário, a distância é inicializada com double.PositiveInfinity
                distancias[i, j] = distancia != 0 ? distancia : double.PositiveInfinity;
            }
        }
        // Define a distância de um vértice para ele mesmo como 0
        for (var i = 0; i < distancias.GetLength(0); i++) {
            distancias[i, i] = 0;
        }
        // Retorna a matriz de distâncias inicializada
        return distancias;
    }
}