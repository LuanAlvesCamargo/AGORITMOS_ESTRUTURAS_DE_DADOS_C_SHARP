using System;

namespace Algoritmos.Busca;

// A classe "BuscaBinaria" verifica a existência de um elemento em um array ordenado,
// o com o elemento central e ajustando os limites de busca.
// Complexidade de tempo: O(log(n)).
// Complexidade de espaço: O(1).
// Nota: O array deve estar previamente ordenado.

public class BuscaBinaria<T> where T : IComparable<T> {
    public int EncontrarIndice(T[] dadosOrdenados, T elemento) {
        var indiceEsquerdo = 0;
        var indiceDireito = dadosOrdenados.Length - 1;

        while (indiceEsquerdo <= indiceDireito) {
            var indiceMeio = indiceEsquerdo + (indiceDireito - indiceEsquerdo) / 2;

            if (elemento.CompareTo(dadosOrdenados[indiceMeio]) > 0) {
                indiceEsquerdo = indiceMeio + 1;
                continue;
            }

            if (elemento.CompareTo(dadosOrdenados[indiceMeio]) < 0) {
                indiceDireito = indiceMeio - 1;
                continue;
            }
            return indiceMeio;
        }
        return -1;
    }
}