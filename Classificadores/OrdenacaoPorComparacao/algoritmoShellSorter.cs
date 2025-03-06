using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

// Classe que implementa o algoritmo de ordenação Shell Sort.

public class OrdenadorShell<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o comparador especificado.
    // O algoritmo Shell Sort é baseado no algoritmo Bubble Sort, mas com melhorias.
    // Características:
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação instável: a ordem relativa de elementos iguais não é preservada.
    // - Complexidade de tempo no pior caso: O(n^2), onde n é o comprimento do array.
    // - Complexidade de espaço: O(1), pois usa espaço constante adicional.
    // Algoritmo:
    // 1.  Divide o array em sub-arrays com um intervalo (step) decrescente.
    // 2.  Ordena cada sub-array usando o Bubble Sort com o intervalo especificado.
    // 3.  Reduz o intervalo e repete os passos 1 e 2 até que o intervalo seja 1.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var intervalo = array.Length / 2; intervalo > 0; intervalo /= 2) {
            for (var i = 0; i < intervalo; i++) {
                OrdenarBolhaComIntervalo(array, comparador, i, intervalo);
            }
        }
    }

    private static void OrdenarBolhaComIntervalo(T[] array, IComparer<T> comparador, int inicio, int intervalo) {
        for (var j = inicio; j < array.Length - intervalo; j += intervalo) {
            var houveTroca = false;
            for (var k = inicio; k < array.Length - j - intervalo; k += intervalo) {
                if (comparador.Compare(array[k], array[k + intervalo]) > 0) {
                    var temp = array[k];
                    array[k] = array[k + intervalo];
                    array[k + intervalo] = temp;
                    houveTroca = true;
                }
            }

            if (!houveTroca) {
                break;
            }
        }
    }
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}