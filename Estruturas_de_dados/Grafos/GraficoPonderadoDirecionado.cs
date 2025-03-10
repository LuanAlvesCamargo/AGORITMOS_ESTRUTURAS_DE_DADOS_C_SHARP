using System;
using System.Collections.Generic;

namespace DataStructures.Graph;

// Implementação de um grafo direcionado ponderado usando matriz de adjacência. Tipo genérico dos vértices do grafo.
public class GrafoDirecionadoPonderado<T> : IGrafoDirecionadoPonderado<T> {
    private readonly int capacidade;
    private readonly double[,] matrizAdjacencia;
    public GrafoDirecionadoPonderado(int capacidade) {
        LancarExcecaoSeCapacidadeNegativa(capacidade);

        this.capacidade = capacidade;
        Vertices = new Vertice<T>[capacidade];
        matrizAdjacencia = new double[capacidade, capacidade];
        Contagem = 0;
    }
    public Vertice<T>?[] Vertices { get; private set; }
    public int Contagem { get; private set; }
    public Vertice<T> AdicionarVertice(T dados) {
        LancarExcecaoSeOverflow();
        var vertice = new Vertice<T>(dados, Contagem, this);
        Vertices[Contagem] = vertice;
        Contagem++;
        return vertice;
    }
    public void AdicionarAresta(Vertice<T> verticeInicial, Vertice<T> verticeFinal, double peso) {
        LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeInicial);
        LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeFinal);
        LancarExcecaoSePesoZero(peso);
        var pesoArestaAtual = matrizAdjacencia[verticeInicial.Indice, verticeFinal.Indice];
        LancarExcecaoSeArestaExiste(pesoArestaAtual);
        matrizAdjacencia[verticeInicial.Indice, verticeFinal.Indice] = peso;
    }
    public void RemoverVertice(Vertice<T> vertice) {
        LancarExcecaoSeVerticeNaoEstaNoGrafo(vertice);
        // Implementar a lógica de remoção de vértice e arestas adjacentes.
        // Isso envolve atualizar a matriz de adjacência e o array de vértices.
    }

    // Métodos auxiliares para lançar exceções (a implementar)
    private void LancarExcecaoSeCapacidadeNegativa(int capacidade) {
        if (capacidade < 0) {
            throw new ArgumentException("A capacidade não pode ser negativa.");
        }
    }

    private void LancarExcecaoSeOverflow() {
        if (Contagem >= capacidade) {
            throw new InvalidOperationException("O grafo atingiu sua capacidade máxima.");
        }
    }

    private void LancarExcecaoSeVerticeNaoEstaNoGrafo(Vertice<T> vertice) {
        if (vertice.Grafo != this) {
            throw new ArgumentException("O vértice não pertence a este grafo.");
        }
    }

    private void LancarExcecaoSePesoZero(double peso) {
        if (peso == 0) {
            throw new ArgumentException("O peso da aresta não pode ser zero.");
        }
    }

    private void LancarExcecaoSeArestaExiste(double pesoArestaAtual) {
        if (pesoArestaAtual != 0) {
            throw new InvalidOperationException("Já existe uma aresta entre esses vértices.");
        }
    }
}
public interface IGrafoDirecionadoPonderado<T>{
    Vertice<T> AdicionarVertice(T dados);
    void AdicionarAresta(Vertice<T> verticeInicial, Vertice<T> verticeFinal, double peso);
}    

    public void RemoverVertice(Vertice<T> vertice) {
    LancarExcecaoSeVerticeNaoEstaNoGrafo(vertice);
    int indiceARemover = vertice.Indice;
    vertice.Indice = -1;
    vertice.DefinirGrafoNulo();

    // Atualiza o array de vértices e o índice dos vértices.
    for (int i = indiceARemover; i < Contagem - 1; i++) {
        Vertices[i] = Vertices[i + 1];
        Vertices[i]!.Indice = i;
    }
    Vertices[Contagem - 1] = null;

    // Atualiza a matriz de adjacência para remover a linha e a coluna do vértice removido.
    for (int i = 0; i < Contagem; i++) {
        for (int j = 0; j < Contagem; j++) {
            if (i < indiceARemover && j < indiceARemover) {
                continue;
            } else if (i < indiceARemover && j >= indiceARemover && j < Contagem - 1) {
                matrizAdjacencia[i, j] = matrizAdjacencia[i, j + 1];
            } else if (i >= indiceARemover && i < Contagem - 1 && j < indiceARemover) {
                matrizAdjacencia[i, j] = matrizAdjacencia[i + 1, j];
            } else if (i >= indiceARemover && i < Contagem - 1 && j >= indiceARemover && j < Contagem - 1) {
                matrizAdjacencia[i, j] = matrizAdjacencia[i + 1, j + 1];
            } else if (i == Contagem - 1 || j == Contagem - 1) {
                matrizAdjacencia[i, j] = 0;
            } else {
                throw new InvalidOperationException();
            }
        }
    }
    Contagem--;
}
public void RemoverAresta(Vertice<T> verticeInicial, Vertice<T> verticeFinal) {
    LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeInicial);
    LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeFinal);
    matrizAdjacencia[verticeInicial.Indice, verticeFinal.Indice] = 0;
}

public IEnumerable<Vertice<T>?> ObterVizinhos(Vertice<T> vertice) {
    LancarExcecaoSeVerticeNaoEstaNoGrafo(vertice);

    for (var i = 0; i < Contagem; i++) {
        if (matrizAdjacencia[vertice.Indice, i] != 0) {
            yield return Vertices[i];
        }
    }
}
public bool SaoAdjacentes(Vertice<T> verticeInicial, Vertice<T> verticeFinal) {
    LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeInicial);
    LancarExcecaoSeVerticeNaoEstaNoGrafo(verticeFinal);

    return matrizAdjacencia[verticeInicial.Indice, verticeFinal.Indice] != 0;
}

public double DistanciaAdjacente(Vertice<T> verticeInicial, Vertice<T> verticeFinal) {
    if (SaoAdjacentes(verticeInicial, verticeFinal)) {
        return matrizAdjacencia[verticeInicial.Indice, verticeFinal.Indice];
    }
    return 0;
}

private static void LancarExcecaoSeCapacidadeNegativa(int capacidade) {
    if (capacidade < 0) {
        throw new InvalidOperationException("A capacidade do grafo deve sempre ser um inteiro não negativo.");
    }
}

private static void LancarExcecaoSePesoZero(double peso) {
    if (peso.Equals(0.0d)) {
        throw new InvalidOperationException("O peso da aresta não pode ser zero.");
    }
}

private static void LancarExcecaoSeArestaExiste(double pesoArestaAtual) {
    if (!pesoArestaAtual.Equals(0.0d)) {
        throw new InvalidOperationException($"Aresta já existe: {pesoArestaAtual}");
    }
}

private void LancarExcecaoSeOverflow() {
    if (Contagem == capacidade) {
        throw new InvalidOperationException("Overflow do grafo.");
    }
}

private void LancarExcecaoSeVerticeNaoEstaNoGrafo(Vertice<T> vertice) {
    if (vertice.Grafo != this) {
        throw new InvalidOperationException($"O vértice não pertence ao grafo: {vertice}.");
    }
}