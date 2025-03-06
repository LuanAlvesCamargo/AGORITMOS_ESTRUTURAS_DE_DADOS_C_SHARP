using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

// Classe que implementa o algoritmo de ordenação por inserção (Insertion Sort).
public class OrdenadorInsercao<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o comparador especificado.
    // Características:
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação estável: a ordem relativa de elementos iguais é preservada.
    // - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
    // - Complexidade de espaço: O(1), pois usa espaço constante adicional 
    // Algoritmo:
    // 1.  Percorre o array a partir do segundo elemento.
    // 2.  Para cada elemento, compara-o com os elementos anteriores e insere-o na posição correta.
    // 3.  Os elementos maiores que o elemento a ser inserido são deslocados para a direita.
    // 4.  Repete os passos 2 e 3 até que o array esteja ordenado.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 1; i < array.Length; i++) {
            for (var j = i; j > 0 && comparador.Compare(array[j], array[j - 1]) < 0; j--) {
                var temp = array[j - 1];
                array[j - 1] = array[j];
                array[j] = temp;
            }
        }
    }
}
// Interface para ordenadores que usam comparações entre elementos.
public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}