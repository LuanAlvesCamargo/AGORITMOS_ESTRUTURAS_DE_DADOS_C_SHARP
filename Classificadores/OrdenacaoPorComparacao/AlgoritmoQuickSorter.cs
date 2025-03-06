using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Ordena arrays usando o algoritmo Quicksort.
    // O algoritmo quicksort é um método de ordenação muito rápido e eficiente, inventado por C.A.R. Hoare em 1960, 
    // quando visitou a Universidade de Moscovo como estudante. Naquela época,
    // Hoare trabalhou em um projeto de tradução de máquina para o National Physical Laboratory.
public abstract class OrdenadorQuicksort<T> : IOrdenadorComparacao<T> {
    // Ordena o array usando o esquema de partição de Hoare.
    // Características:
    // - Ordenação interna (in-place).
    // - Complexidade de tempo média: O(n log(n)).
    // - Complexidade de tempo pior caso: O(n^2).
    // - Complexidade de espaço: O(log(n)).
    // Onde n é o comprimento do array.
    public void Ordenar(T[] array, IComparer<T> comparador) => Ordenar(array, comparador, 0, array.Length - 1);

    protected abstract T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita);
    // Ordena recursivamente o sub-array.
    private void Ordenar(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        if (esquerda >= direita) {
            return;
        }

        var p = Particionar(array, comparador, esquerda, direita);
        Ordenar(array, comparador, esquerda, p);
        Ordenar(array, comparador, p + 1, direita);
    }

    // Particiona o sub-array usando o esquema de Hoare.
    private int Particionar(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        var pivo = SelecionarPivo(array, comparador, esquerda, direita);
        var nEsquerda = esquerda;
        var nDireita = direita;
        while (true) {
            while (comparador.Compare(array[nEsquerda], pivo) < 0) {
                nEsquerda++;
            } 

            while (comparador.Compare(array[nDireita], pivo) > 0) {
                nDireita--;
            }

            if (nEsquerda >= nDireita) {
                return nDireita;
            }

            var temp = array[nEsquerda];
            array[nEsquerda] = array[nDireita];
            array[nDireita] = temp;

            nEsquerda++;
            nDireita--;
        }
    }
}
public interface IOrdenadorComparacao<T> {
    // Ordena um array usando um comparador especificado.
    void Ordenar(T[] array, IComparer<T> comparador);
}