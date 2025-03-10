using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algoritmos.Grafo;

// Busca em Largura (Breadth-First Search - BFS) - algoritmo para percorrer um grafo.
// O algoritmo começa a partir de um nó raiz selecionado pelo usuário.
// O algoritmo explora todos os nós na profundidade atual antes de passar para a próxima profundidade.

public class BuscaEmLargura<T> : IBuscaGrafo<T> where T : IComparable<T> {
    public void VisitarTodos(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao = default) {
        BFS(grafo, verticeInicial, acao, new HashSet<Vertice<T>>());
    }

    private void BFS(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao, HashSet<Vertice<T>> visitados) {
        var fila = new Queue<Vertice<T>>();
        fila.Enqueue(verticeInicial);
        while (fila.Count > 0) {
            var verticeAtual = fila.Dequeue();
            if (verticeAtual == null || visitados.Contains(verticeAtual)) {
                continue;
            }
            foreach (var vertice in grafo.ObterVizinhos(verticeAtual)) {
                fila.Enqueue(vertice!);
            }
            acao?.Invoke(verticeAtual);
            visitados.Add(verticeAtual);
        }
    }
}

public interface IBuscaGrafo<T> where T : IComparable<T> {
    void VisitarTodos(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao = default);
}