using System.Collections.Generic;
using System.Linq;
using EstruturasDeDados.Grafos;

namespace Algoritmos.Grafos;

    // Implementação do algoritmo de Kosaraju-Sharir (também conhecido como algoritmo de Kosaraju)
    // para encontrar os componentes fortemente conectados (CFC) de um grafo direcionado.
public static class Kosaraju<T> {

    public static void Visitar(Vertice<T> vertice, IDirecionadoPonderadoGrafo<T> grafo, HashSet<Vertice<T>> visitados, Stack<Vertice<T>> ordenadoInverso) {
        // Se o vértice já foi visitado, retorna.
        if (visitados.Contains(vertice)) {
            return;
        }

        // Marca o vértice como visitado.
        visitados.Add(vertice);

        // Adiciona o vértice à pilha de ordem inversa.
        // A pilha garante que os vértices sejam processados na ordem inversa de término da DFS.
        ordenadoInverso.Push(vertice);

        // Visita recursivamente os vizinhos do vértice.
        foreach (var vizinho in grafo.ObterVizinhos(vertice)) {
            Visitar(vizinho!, grafo, visitados, ordenadoInverso);
        }
    }
    public static void Atribuir(Vertice<T> vertice, Vertice<T> raiz, IDirecionadoPonderadoGrafo<T> grafo, Dictionary<Vertice<T>, Vertice<T>> representantes) {
        // Se o vértice já tiver um vértice representativo (raiz) atribuído, retorna.
        if (representantes.ContainsKey(vertice)) {
            return;
        }

        // Atribui a raiz ao vértice.
        representantes.Add(vertice, raiz);

        // Atribui recursivamente o vértice raiz atual aos vizinhos do vértice.
        foreach (var vizinho in grafo.ObterVizinhos(vertice)) {
            Atribuir(vizinho!, raiz, grafo, representantes);
        }
    }

    public static Dictionary<Vertice<T>, Vertice<T>> ObterRepresentantes(IDirecionadoPonderadoGrafo<T> grafo) {
        // Conjunto para armazenar os vértices visitados durante a primeira DFS.
        HashSet<Vertice<T>> visitados = new HashSet<Vertice<T>>();

        // Pilha para armazenar os vértices em ordem inversa de término da primeira DFS.
        Stack<Vertice<T>> ordenadoInverso = new Stack<Vertice<T>>();

        // Dicionário para armazenar os vértices representativos (raízes) dos CFCs.
        Dictionary<Vertice<T>, Vertice<T>> representantes = new Dictionary<Vertice<T>, Vertice<T>>();

        // Primeira DFS: percorre o grafo e preenche a pilha ordenadoInverso.
        foreach (var vertice in grafo.Vertices) {
            if (vertice != null) {
                Visitar(vertice, grafo, visitados, ordenadoInverso);
            }
        }

        // Limpa o conjunto de vértices visitados para a segunda DFS.
        visitados.Clear();
        // Segunda DFS: percorre o grafo na ordem inversa de ordenadoInverso e atribui as raízes dos CFCs.
        while (ordenadoInverso.Count > 0) {
            Vertice<T> vertice = ordenadoInverso.Pop();
            Atribuir(vertice, vertice, grafo, representantes);
        }
        // Retorna o dicionário de vértices representativos.
        return representantes;
    }
    public static IEnumerable<Vertice<T>>[] ObterCFCs(IDirecionadoPonderadoGrafo<T> grafo) {
        // Obtém os vértices representativos dos CFCs.
        var representantes = ObterRepresentantes(grafo);

        // Dicionário para agrupar os vértices por seus respectivos CFCs.
        Dictionary<Vertice<T>, List<Vertice<T>>> cfc = new Dictionary<Vertice<T>, List<Vertice<T>>>();

        // Agrupa os vértices por seus vértices representativos (raízes).
        foreach (var parChaveValor in representantes) {
            // Se o vértice representativo já existe no dicionário cfc, adiciona o vértice à lista correspondente.
            if (cfc.ContainsKey(parChaveValor.Value)) {
                cfc[parChaveValor.Value].Add(parChaveValor.Key);
            }  else { // Caso contrário, cria uma nova lista com o vértice e adiciona ao dicionário cfc.
                cfc.Add(parChaveValor.Value, new List<Vertice<T>> { parChaveValor.Key });
            }
        }
        // Retorna um array de IEnumerable de vértices, onde cada IEnumerable representa um CFC.
        return cfc.Values.ToArray();
    }
}