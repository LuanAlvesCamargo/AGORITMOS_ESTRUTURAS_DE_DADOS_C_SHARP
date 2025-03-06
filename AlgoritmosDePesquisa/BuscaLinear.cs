
using System;
using Utilities.Exceptions;

namespace Algoritmos.Busca {
    // Na Ciência da Computação, costuma-se usar o termo busca linear para expressar um 
    // tipo de pesquisa em vetores ou listas de modo sequencial, i. e., elemento por elemento, de modo que a 
    // função do tempo em relação ao número de elementos é linear, ou seja, cresce proporcionalmente.
    // Classe que implementa o algoritmo de busca linear.
    // Tipo dos elementos do vetor.
    // Busca o primeiro elemento no vetor que satisfaz a condição especificada.
    // Complexidade de tempo: O(n)
    // Complexidade de espaço: O(1)
    // Vetor onde será realizada a busca.
    // Função que define o critério de busca.
    // O primeiro elemento que satisfaz o critério.
    // Lançado caso nenhum elemento atenda ao critério.</exception
    // Busca o índice do primeiro elemento no vetor que satisfaz a condição especificada.
    // Complexidade de tempo: O(n)
    // Complexidade de espaço: O(1)
    // Vetor onde será realizada a busca.
    // Função que define o critério de busca.
    // Índice do primeiro elemento que satisfaz o critério ou -1 caso nenhum seja encontrado.
    public class BuscadorLinear<T> {

        public T Encontrar(T[] dados, Func<T, bool> criterio) {
            for (var i = 0; i < dados.Length; i++) {
                if (criterio(dados[i])) {
                    return dados[i];
                }
            }
            throw new ItemNotFoundException();
        }


        public int EncontrarIndice(T[] dados, Func<T, bool> criterio) {
            for (var i = 0; i < dados.Length; i++) {
                if (criterio(dados[i])) {
                    return i;
                }
            }
            return -1;
        }
    }
}
