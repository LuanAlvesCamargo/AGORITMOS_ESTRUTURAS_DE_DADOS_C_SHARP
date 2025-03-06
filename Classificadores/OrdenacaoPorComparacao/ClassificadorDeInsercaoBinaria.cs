using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;
    // Implementa o algoritmo de ordenação por inserção binária.
    // Este algoritmo é uma variação do algoritmo de ordenação por inserção, onde a posição
    // correta para inserir cada elemento é determinada usando busca binária.
    // O tipo dos elementos no array a ser ordenado.
public class OrdenadorInsercaoBinaria<T> : IOrdenadorComparacao<T> {
        //  Ordena um array usando um comparador especificado.
        //  Este método implementa a ordenação por inserção binária, que é uma variação
        //  da ordenação por inserção tradicional.
        //  Características:
        //  - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
        //  - Ordenação instável: a ordem relativa de elementos iguais não é preservada.
        //  - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
        //  - Complexidade de espaço: O(1), pois usa espaço constante adicional.
        //  Algoritmo:
        //  1.  Para cada elemento do array, começando do segundo elemento:
        //  2.  Encontre a posição correta para inserir o elemento usando busca binária.
        //  3.  Desloque os elementos maiores que o elemento a ser inserido para a direita.
        //  4.  Insira o elemento na posição correta.
        public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 1; i < array.Length; i++) {
            var alvo = array[i];
            var indiceMovimento = i - 1;
            var localInsercaoAlvo = BuscaBinaria(array, 0, indiceMovimento, alvo, comparador);
            Array.Copy(array, localInsercaoAlvo, array, localInsercaoAlvo + 1, i - localInsercaoAlvo);

            array[localInsercaoAlvo] = alvo;
        }
    }
    // Implementação da busca binária usando uma abordagem iterativa.
    // Este método encontra a posição correta para inserir um elemento em um array ordenado.
    // Um array de valores ordenados em ordem crescente entre os índices 'esquerda' e 'direita'.
    // O índice esquerdo do intervalo de busca (inclusivo).
    // O índice direito do intervalo de busca (inclusivo).
    // O valor a ser encontrado no array.
    // Um comparador para comparar elementos.
    // O índice onde o valor alvo deve ser inserido.
    private static int BuscaBinaria(T[] array, int esquerda, int direita, T alvo, IComparer<T> comparador) {
        while (direita > esquerda) {
            var meio = (esquerda + direita) / 2;
            var resultadoComparacao = comparador.Compare(alvo, array[meio]);

            if (resultadoComparacao == 0) {
                return meio + 1;
            }

            if (resultadoComparacao > 0) {
                esquerda = meio + 1;
            } else {
                direita = meio - 1;
            }
        }

        return comparador.Compare(alvo, array[esquerda]) < 0 ? esquerda : esquerda + 1;
    }
}
    // Interface para ordenadores que usam comparações entre elementos.
    // O tipo dos elementos a serem ordenados.
public interface IOrdenadorComparacao<T> {
    // Ordena um array usando um comparador especificado.
    // O array a ser ordenado.
    // Um comparador para comparar elementos.
    void Ordenar(T[] array, IComparer<T> comparador);
}