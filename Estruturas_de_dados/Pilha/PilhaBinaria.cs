using System;
using System.Collections.Generic;

namespace DataStructures.Heap;

public class HeapBinaria<T> {
    // Uma implementação genérica de um heap binário.
    // Um ​​heap binário é uma árvore binária completa que satisfaz a propriedade heap;
    // isto é, cada nó na árvore compara maior/menor que ou igual aos seus nós filhos esquerdo e direito
    // Observe que isso é diferente de uma árvore de busca binária, onde cada nó
    // deve ser o maior/menor nó de todos os seus filhos.
    // Embora os heaps binários não sejam muito eficientes, eles são (provavelmente) os heaps mais simples
    private readonly Comparer<T> comparador;
    private readonly List<T> dados;

    public HeapBinaria() {
        dados = new List<T>();
        comparador = Comparer<T>.Default;
    }

    public HeapBinaria(Comparer<T> comparadorPersonalizado) {
        dados = new List<T>();
        comparador = comparadorPersonalizado;
    }

    public int Contagem => dados.Count;

    public void Inserir(T elemento) {
        dados.Add(elemento);
        RestaurarHeapAcima(dados.Count - 1);
    }

    public T Remover() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazia!");
        }
        var elemento = dados[0];
        dados[0] = dados[^1];
        dados.RemoveAt(dados.Count - 1);
        RestaurarHeapAbaixo(0);
        return elemento;
    }

    public T Espiar() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazia!");
        }
        return dados[0];
    }

    public T InserirRemover(T elemento) {
        if (Contagem == 0) {
            return elemento;
        }
        if (comparador.Compare(elemento, dados[0]) < 0) {
            var temp = dados[0];
            dados[0] = elemento;
            RestaurarHeapAbaixo(0);
            return temp;
        }
        return elemento;
    }

    public bool Contem(T elemento) => dados.Contains(elemento);

    public void Remover(T elemento) {
        var indice = dados.IndexOf(elemento);
        if (indice == -1) {
            throw new ArgumentException($"{elemento} não está na heap!");
        }

        Trocar(indice, dados.Count - 1);
        var temp = dados[^1];
        dados.RemoveAt(dados.Count - 1);

        if (indice < dados.Count) {
            if (comparador.Compare(temp, dados[indice]) > 0) {
                RestaurarHeapAbaixo(indice);
            } else {
                RestaurarHeapAcima(indice);
            }
        }
    }

    private void Trocar(int indice1, int indice2) {
        var temp = dados[indice1];
        dados[indice1] = dados[indice2];
        dados[indice2] = temp;
    }

    private void RestaurarHeapAcima(int indiceElemento) {
        var pai = (indiceElemento - 1) / 2;
        if (pai >= 0 && comparador.Compare(dados[indiceElemento], dados[pai]) > 0) {
            Trocar(indiceElemento, pai);
            RestaurarHeapAcima(pai);
        }
    }

    private void RestaurarHeapAbaixo(int indiceElemento) {
        var esquerda = 2 * indiceElemento + 1;
        var direita = 2 * indiceElemento + 2;
        var esquerdaMaiorQueElemento = esquerda < Contagem && comparador.Compare(dados[indiceElemento], dados[esquerda]) < 0;
        var direitaMaiorQueElemento = direita < Contagem && comparador.Compare(dados[indiceElemento], dados[direita]) < 0;
        var esquerdaMaiorQueDireita = esquerda < Contagem && direita < Contagem && comparador.Compare(dados[esquerda], dados[direita]) > 0;

        if (esquerdaMaiorQueElemento && esquerdaMaiorQueDireita) {
            Trocar(indiceElemento, esquerda);
            RestaurarHeapAbaixo(esquerda);
        } else if (direitaMaiorQueElemento && !esquerdaMaiorQueDireita) {
            Trocar(indiceElemento, direita);
            RestaurarHeapAbaixo(direita);
        }
    }
}

