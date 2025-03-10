using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algoritmos.Grafo;

// Algoritmo de Bellman-Ford em grafo direcionado ponderado.

public class BellmanFord<T> {
    private readonly GrafoDirecionadoPonderado<T> grafo;
    private readonly Dictionary<Vertice<T>, double> distancias;
    private readonly Dictionary<Vertice<T>, Vertice<T>?> predecessores;
    public BellmanFord(GrafoDirecionadoPonderado<T> grafo, Dictionary<Vertice<T>, double> distancias, Dictionary<Vertice<T>, Vertice<T>?> predecessores) {
        this.grafo = grafo;
        this.distancias = distancias;
        this.predecessores = predecessores;
    }
    public Dictionary<Vertice<T>, double> Executar(Vertice<T> verticeOrigem) {
        InicializarDistancias(verticeOrigem);
        RelaxarArestas();
        VerificarCiclosNegativos();
        return distancias;
    }

    private void InicializarDistancias(Vertice<T> verticeOrigem) {
        foreach (var vertice in grafo.Vertices) {
            if (vertice != null) {
                distancias[vertice] = double.PositiveInfinity;
                predecessores[vertice] = null;
            }
        }
        distancias[verticeOrigem] = 0;
    }

    private void RelaxarArestas() {
        int contagemVertices = grafo.Contagem;

        for (int i = 0; i < contagemVertices - 1; i++) {
            foreach (var vertice in grafo.Vertices) {
                if (vertice != null) {
                    RelaxarArestasParaVertice(vertice);
                }
            }
        }
    }

    private void RelaxarArestasParaVertice(Vertice<T> u) {
        foreach (var vizinho in grafo.ObterVizinhos(u)) {
            if (vizinho == null) {
                continue;
            }

            var v = vizinho;
            var peso = grafo.DistanciaAdjacente(u, v);

            if (distancias[u] + peso < distancias[v]) {
                distancias[v] = distancias[u] + peso;
                predecessores[v] = u;
            }
        }
    }

    private void VerificarCiclosNegativos() {
        foreach (var vertice in grafo.Vertices) {
            if (vertice != null) {
                VerificarCiclosNegativosParaVertice(vertice);
            }
        }
    }

    private void VerificarCiclosNegativosParaVertice(Vertice<T> u) {
        foreach (var vizinho in grafo.ObterVizinhos(u)) {
            if (vizinho == null) {
                continue;
            }
            var v = vizinho;
            var peso = grafo.DistanciaAdjacente(u, v);
            if (distancias[u] + peso < distancias[v]) {
                throw new InvalidOperationException("O grafo contém um ciclo de peso negativo.");
            }
        }
    }
}