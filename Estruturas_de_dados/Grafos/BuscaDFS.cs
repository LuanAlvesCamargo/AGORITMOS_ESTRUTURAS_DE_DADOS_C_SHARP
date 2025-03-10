using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algoritmos.Grafos;

// Busca em Profundidade (Depth First Search - DFS) - algoritmo para percorrer um grafo.
// O algoritmo começa a partir de um nó raiz selecionado pelo usuário.
// O algoritmo explora o máximo possível ao longo de cada ramo antes de retroceder (backtracking).

public class BuscaEmProfundidade<T> : IBuscaGrafo<T> where T : IComparable<T> {
    public void VisitarTodos(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao = default) {
        Dfs(grafo, verticeInicial, acao, new HashSet<Vertice<T>>());
    }

    private void Dfs(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao, HashSet<Vertice<T>> visitados) {
        acao?.Invoke(verticeInicial);

        visitados.Add(verticeInicial);

        foreach (var vertice in grafo.ObterVizinhos(verticeInicial)) {
            if (vertice == null || visitados.Contains(vertice)) {
                continue;
            }

            Dfs(grafo, vertice!, acao, visitados);
        }
    }
}
public interface IBuscaGrafo<T> where T : IComparable<T> {
    void VisitarTodos(IDirecionadoPonderadoGrafo<T> grafo, Vertice<T> verticeInicial, Action<Vertice<T>>? acao = default);
}

public interface IDirecionadoPonderadoGrafo<T> where T : IComparable<T> {
    IEnumerable<Vertice<T>> ObterVizinhos(Vertice<T> vertice);
}

public class Vertice<T> where T : IComparable<T> {
    public T Dados { get; set; }
    public Vertice(T dados) {
        Dados = dados;
    }
}