using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Classe que implementa o algoritmo de ordenação por seleção (Selection Sort).
public class OrdenadorSelecao<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o comparador especificado.
    // Características:
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação instável: a ordem relativa de elementos iguais pode não ser preservada.
    // - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
    // - Complexidade de espaço: O(1), pois usa espaço constante adicional.
    // Algoritmo:
    // 1.  Percorre o array, encontrando o menor elemento não ordenado.
    // 2.  Troca o menor elemento encontrado com o primeiro elemento não ordenado.
    // 3.  Repete os passos 1 e 2 até que o array esteja ordenado.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 0; i < array.Length - 1; i++) {
            var indiceMinimo = i;
            for (var j = i + 1; j < array.Length; j++) {
                if (comparador.Compare(array[indiceMinimo], array[j]) > 0) {
                    indiceMinimo = j;
                }
            }

            var temp = array[i];
            array[i] = array[indiceMinimo];
            array[indiceMinimo] = temp;
        }
    }
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}