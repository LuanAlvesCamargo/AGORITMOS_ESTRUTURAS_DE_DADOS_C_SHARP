using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;


    // Classe que implementa o algoritmo de ordenação Pancake Sort (Ordenação da Panqueca).
public class OrdenadorPanqueca<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o comparador especificado.
    // Características:
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação instável: a ordem relativa de elementos iguais não é preservada.
    // - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
    // - Complexidade de espaço: O(1), pois usa espaço constante adicional.
    // Algoritmo:
    // 1.  Percorre o array, reduzindo o tamanho do sub-array a ser ordenado a cada iteração.
    // 2.  Encontra o índice do maior elemento no sub-array atual.
    // 3.  Se o maior elemento não estiver no final do sub-array, realiza duas inversões:
    //     a.  Inverte o sub-array até o maior elemento, movendo-o para o início.
    //     b.  Inverte o sub-array completo, movendo o maior elemento para o final.
    // 4.  Repete os passos 1-3 até que o array esteja ordenado.

    public void Ordenar(T[] array, IComparer<T> comparador) {
        var n = array.Length;
        // Começa do array completo e reduz o tamanho atual em um a cada iteração
        for (var tamanhoAtual = n; tamanhoAtual > 1; --tamanhoAtual) {
            // Encontra o índice do maior elemento em array[0..tamanhoAtual-1]
            var indiceMaximo = EncontrarMaximo(array, tamanhoAtual, comparador);
            // Move o maior elemento para o final do array atual se ele já não estiver lá
            if (indiceMaximo != tamanhoAtual - 1) {
                // Para mover para o final, primeiro move o maior número para o início
                Inverter(array, indiceMaximo);
                // Agora move o maior número para o final invertendo o array atual
                Inverter(array, tamanhoAtual - 1);
            }
        }
    }
    // Inverte array[0..i]
    private void Inverter(T[] array, int i) {
        T temp;
        var inicio = 0;
        while (inicio < i) {
            temp = array[inicio];
            array[inicio] = array[i];
            array[i] = temp;
            inicio++;
            i--;
        }
    }
    // Retorna o índice do maior elemento em array[0..n-1]
    private int EncontrarMaximo(T[] array, int n, IComparer<T> comparador) {
        var indiceMaximo = 0;
        for (var i = 0; i < n; i++) {
            if (comparador.Compare(array[i], array[indiceMaximo]) == 1) {
                indiceMaximo = i;
            }
        }

        return indiceMaximo;
    }
}
// Interface para ordenadores que usam comparações entre elementos.
public interface IOrdenadorComparacao<T> {
    // Ordena um array usando um comparador especificado.
    void Ordenar(T[] array, IComparer<T> comparador);
}