using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

//  Ordena arrays usando o algoritmo Quicksort, selecionando um ponto aleatório como pivô.
public sealed class OrdenadorQuicksortPivoAleatorio<T> : OrdenadorQuicksort<T> {
    private readonly Random aleatorio = new();
    protected override T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita) =>
        array[aleatorio.Next(esquerda, direita + 1)];
}

public abstract class OrdenadorQuicksort<T> : IOrdenadorComparacao<T> {
    public void Ordenar(T[] array, IComparer<T> comparador) {
        OrdenarQuicksortRecursivo(array, comparador, 0, array.Length - 1);
    }
    private void OrdenarQuicksortRecursivo(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        if (esquerda < direita) {
            int indicePivo = Particionar(array, comparador, esquerda, direita);
            OrdenarQuicksortRecursivo(array, comparador, esquerda, indicePivo - 1);
            OrdenarQuicksortRecursivo(array, comparador, indicePivo + 1, direita);
        }
    }
    private int Particionar(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        T pivo = SelecionarPivo(array, comparador, esquerda, direita);
        int i = esquerda - 1;

        for (int j = esquerda; j < direita; j++) {
            if (comparador.Compare(array[j], pivo) <= 0) {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[direita]) = (array[direita], array[i + 1]);
        return i + 1;
    }
    protected abstract T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita);
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}