using System;
using System.Collections.Generic;

namespace Algoritmos.Busca;
    // A busca binária recursiva é uma implementação do algoritmo de busca binária que usa a chamada de 
    // si mesma para encontrar um item em uma lista ordenada. 
    // Como funciona 
    // Compara o item que está sendo buscado com o item do meio da lista
    // Se o item for igual, a busca termina
    // Se o item for maior, a busca binária é realizada na metade superior da lista
    // Se o item for menor, a busca binária é realizada na metade inferior da lista
    // A busca termina quando a lista tiver zero elementos

    // Implementação da busca binária recursiva.
    // Tipo de dado a ser buscado (deve ser comparável).
    // Encontra o índice de um item em uma coleção ordenada.
    // Complexidade de tempo: O(log(n)),
    // Complexidade de espaço: O(1),
    // onde n é o tamanho da coleção.

    // Coleção ordenada onde será realizada a busca.
    // Item a ser buscado.
    // Lançado se a coleção fornecida for nula.
    // Índice do item encontrado ou -1 se não for encontrado.
public class BuscaBinariaRecursiva<T> where T : IComparable<T> {

    public int EncontrarIndice(IList<T>? colecao, T item) { 
        if (colecao is null){
            throw new ArgumentNullException(nameof(colecao)); 
        }

        int indiceEsquerda = 0;
        int indiceDireita = colecao.Count - 1;
        return EncontrarIndice(colecao, item, indiceEsquerda, indiceDireita);
    }

    private int EncontrarIndice(IList<T> colecao, T item, int indiceEsquerda, int indiceDireita) {
        // Caso base: se o limite esquerdo ultrapassou o direito, o item não está na coleção.
        if (indiceEsquerda > indiceDireita) {
            return -1;
        }

        // Calcula o índice do meio para dividir a busca.
        int indiceMeio = indiceEsquerda + (indiceDireita - indiceEsquerda) / 2;
        int comparacao = item.CompareTo(colecao[indiceMeio]);

        // Verifica se o item no meio é o que estamos buscando.
        return comparacao switch {
            0 => indiceMeio, // Item encontrado na posição do meio.
            > 0 => EncontrarIndice(colecao, item, indiceMeio + 1, indiceDireita), // Buscar na metade direita.
            < 0 => EncontrarIndice(colecao, item, indiceEsquerda, indiceMeio - 1), // Buscar na metade esquerda.
            _ => -1,
        };
    }
}
