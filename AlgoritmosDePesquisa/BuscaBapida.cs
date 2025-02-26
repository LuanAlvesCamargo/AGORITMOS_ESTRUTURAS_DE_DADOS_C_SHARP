using System;
using Utilities.Exceptions;

namespace Algoritmos.Busca;

// O algoritmo de busca rápida é uma técnica eficiente para encontrar um elemento específico em um array ordenado.
// Ele combina os princípios da busca binária e da busca por interpolação para otimizar o processo de busca.
// Primeiro, o algoritmo calcula os índices de busca, utilizando tanto a busca binária quanto a busca por interpolação. 
// A busca binária divide o array ao meio, enquanto a busca por interpolação estima a posição do elemento com base em sua 
// magnitude em relação aos valores mínimo e máximo do array.

// Em seguida, o algoritmo seleciona o segmento do array onde o elemento pode estar localizado, com base nos índices calculados. 
// Se o elemento estiver dentro do intervalo estimado pela interpolação, essa busca é usada. Caso contrário, a busca binária é 
// utilizada para garantir a eficiência.
// O processo é repetido recursivamente no segmento selecionado até que o elemento seja encontrado ou o segmento se torne vazio.

// A complexidade de tempo do algoritmo é de O(log n) no pior caso e O(log log n) no caso médio, 
// tornando-o uma opção eficiente para arrays ordenados.

// Em resumo, o algoritmo de busca rápida combina as vantagens da busca binária e da busca por interpolação para encontrar elementos em arrays ordenados de forma eficiente, adaptando-se à distribuição dos dados para otimizar o processo de busca.
// Algoritmo que combina as vantagens da busca binária e da busca por interpolação.
// Complexidade de tempo:
// - Pior caso: O(log n) (quando o item não é encontrado)
// - Caso médio: O(log log n)
// - Melhor caso: O(1)
// OBS: O algoritmo é recursivo e exige que o array esteja previamente ordenado.

public class BuscaRapida {
       public int EncontrarIndice(Span<int> array, int item) {
        if (array.Length == 0) {
            throw new ItemNotFoundException();
        } 

        if (item < array[0] || item > array[^1]) {
            throw new ItemNotFoundException();
        }

        if (array[0] == array[^1]) {
            return item == array[0] ? 0 : throw new ItemNotFoundException();
        }

        var (esquerda, direita) = CalcularIndices(array, item);
        var (inicio, fim) = SelecionarSegmento(array, esquerda, direita, item);

        return inicio + EncontrarIndice(array.Slice(inicio, fim - inicio + 1), item);
    }

    private (int Esquerda, int Direita) CalcularIndices(Span<int> array, int item) {
        var indiceBinario = array.Length / 2;

        int[] secao = {
            array.Length - 1,
            item - array[0],
            array[^1] - array[0],
        };
        var indiceInterpolado = secao[0] * secao[1] / secao[2];

        // O índice esquerdo é o menor e o índice direito é o maior dos índices calculados
        return indiceInterpolado > indiceBinario
            ? (indiceBinario, indiceInterpolado)
            : (indiceInterpolado, indiceBinario);
    }

    private (int Inicio, int Fim) SelecionarSegmento(Span<int> array, int esquerda, int direita, int item) {
        if (item < array[esquerda]) {
            return (0, esquerda - 1);
        }

        if (item < array[direita]) {
            return (esquerda, direita - 1);
        }

        return (direita, array.Length - 1);
    }
}