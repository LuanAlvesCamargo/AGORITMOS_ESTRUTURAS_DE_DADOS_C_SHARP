using System;
using System.Linq;

namespace Algoritmos.Ordenacao.Inteiros;

// Counting sort (Ordenação por Contagem) é um algoritmo para ordenar uma coleção de objetos de acordo com chaves
// que são pequenos inteiros; ou seja, é um algoritmo de ordenação de inteiros. Ele opera contando o número de objetos
// que têm cada valor de chave distinto e usando aritmética nessas contagens para determinar as posições de cada
// valor de chave na sequência de saída. Seu tempo de execução é linear no número de itens e na diferença entre os
// valores de chave máximo e mínimo, portanto, é adequado apenas para uso direto em situações em que a variação nas
// chaves não é significativamente maior que o número de itens. No entanto, ele é frequentemente usado como uma
// sub-rotina em outro algoritmo de ordenação, radix sort, que pode lidar com chaves maiores de forma mais eficiente.

public class OrdenadorContagem : IOrdenadorInteiro {
    // Ordena o array de entrada usando o algoritmo de ordenação por contagem.
    // Complexidade de tempo: O(n+k), onde k é o intervalo dos valores de chave não negativos.
    // Complexidade de espaço: O(n+k), onde k é o intervalo dos valores de chave não negativos.

    public void Ordenar(int[] array) {
        if (array.Length == 0) {
            return;
        }

        var maximo = array.Max();
        var minimo = array.Min();
        var contagem = new int[maximo - minimo + 1];
        var saida = new int[array.Length];
        for (var i = 0; i < array.Length; i++) {
            contagem[array[i] - minimo]++;
        }

        for (var i = 1; i < contagem.Length; i++) {
            contagem[i] += contagem[i - 1];
        }

        for (var i = array.Length - 1; i >= 0; i--) {
            saida[contagem[array[i] - minimo] - 1] = array[i];
            contagem[array[i] - minimo]--;
        }

        Array.Copy(saida, array, array.Length);
    }
}

public interface IOrdenadorInteiro {
    // Ordena um array de inteiros.
    void Ordenar(int[] array);
}