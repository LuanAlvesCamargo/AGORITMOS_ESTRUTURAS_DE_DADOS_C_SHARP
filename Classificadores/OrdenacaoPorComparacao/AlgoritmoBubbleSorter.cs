using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Classe que implementa o algoritmo de ordenação Bubble Sort (Ordenação por Bolha).
    // O bubble sort, ou ordenação por flutuação, é um algoritmo de ordenação dos mais simples. 
    // A ideia é percorrer um conjunto de elementos diversas vezes, e a cada passagem fazer flutuar para o topo o maior elemento da sequência.
public class OrdenadorBolha<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o comparador especificado.
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação estável: a ordem relativa de elementos iguais é preservada.
    // - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
    // - Complexidade de espaço: O(1), pois usa espaço constante adicional.
    // Algoritmo:
    // 1.  Percorre o array repetidamente, comparando pares de elementos adjacentes.
    // 2.  Se os elementos estiverem na ordem errada, troca-os.
    // 3.  Repete os passos 1 e 2 até que o array esteja ordenado.
    // 4.  Uma otimização é verificar se houve alguma troca durante uma passagem. Se não houve, o array já está ordenado.
    // O array a ser ordenado.
    // Um comparador para comparar elementos.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 0; i < array.Length - 1; i++) {
            var houveTroca = false;
            for (var j = 0; j < array.Length - i - 1; j++) {
                if (comparador.Compare(array[j], array[j + 1]) > 0) {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
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