using System.Collections.Generic;
using System.Linq;

namespace Algoritmos.Ordenacao.Comparacao;

    // Algoritmo Dividir e Conquistar, que divide o array em duas metades,
    // chama a si mesmo para as duas metades e depois mescla as duas
    // metades ordenadas.
    // Divisão e Conquista em computação é uma técnica de projeto de algoritmos utilizada pela primeira vez por Anatolii 
    // Karatsuba em 1960 no algoritmo de Karatsuba.
public class OrdenadorMerge<T> : IOrdenadorComparacao<T> { 
    // Ordena um array usando o algoritmo Merge Sort (Ordenação por Mesclagem).
    // Originalmente projetado como um algoritmo de ordenação externa.
    // Características:
    // - Ordenação interna (in-place) quando otimizado, neste caso, não é totalmente in-place por usar arrays auxiliares.
    // - Ordenação estável: a ordem relativa de elementos iguais é preservada.
    // - Complexidade de tempo: O(n log(n)), onde n é o comprimento do array.
    // - Complexidade de espaço: O(n), devido aos arrays auxiliares utilizados na mesclagem.

    public void Ordenar(T[] array, IComparer<T> comparador) {
        if (array.Length <= 1) {
            return;
        }

        var (esquerda, direita) = Dividir(array);
        Ordenar(esquerda, comparador);
        Ordenar(direita, comparador);
        Mesclar(array, esquerda, direita, comparador);
    }
    private static void Mesclar(T[] array, T[] esquerda, T[] direita, IComparer<T> comparador) {
        var indicePrincipal = 0;
        var indiceEsquerda = 0;
        var indiceDireita = 0;

        while (indiceEsquerda < esquerda.Length && indiceDireita < direita.Length) {
            var resultadoComparacao = comparador.Compare(esquerda[indiceEsquerda], direita[indiceDireita]);
            array[indicePrincipal++] = resultadoComparacao <= 0 ? esquerda[indiceEsquerda++] : direita[indiceDireita++];
        } while (indiceEsquerda < esquerda.Length) {
            array[indicePrincipal++] = esquerda[indiceEsquerda++];
        } while (indiceDireita < direita.Length) {
            array[indicePrincipal++] = direita[indiceDireita++];
        }
    }
    private static (T[] Esquerda, T[] Direita) Dividir(T[] array) {
        var meio = array.Length / 2;
        return (array.Take(meio).ToArray(), array.Skip(meio).ToArray());
    }
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}