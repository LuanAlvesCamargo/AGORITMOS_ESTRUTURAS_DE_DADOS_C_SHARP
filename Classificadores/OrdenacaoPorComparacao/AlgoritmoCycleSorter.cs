using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;


    // O Cycle Sort (Ordenação por Ciclos) é um algoritmo de ordenação interna (in-place) e instável.
    // É um algoritmo de comparação que é teoricamente ótimo em termos do número total
    // de escritas no array original.
    // Baseia-se na ideia de que a permutação a ser ordenada pode ser fatorada
    // em ciclos, que podem ser rotacionados individualmente para dar um resultado ordenado.

    // Cycle sort é um algoritmo de ordenação instável no local, uma ordenação por comparação teoricamente 
    // ideal em termos do número total de gravações no array original, diferente de qualquer outro algoritmo de ordenação no local.
public class OrdenadorCiclos<T> : IOrdenadorComparacao<T> {

    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 0; i < array.Length - 1; i++) {
            MoverCiclo(array, i, comparador);
        }
    }

    private static void MoverCiclo(T[] array, int indiceInicial, IComparer<T> comparador) {
        var item = array[indiceInicial];
        var posicao = indiceInicial + ContarElementosMenores(array, indiceInicial + 1, item, comparador);

        if (posicao == indiceInicial) {
            return;
        }

        posicao = PularElementosIguais(array, posicao, item, comparador);

        var temp = array[posicao];
        array[posicao] = item;
        item = temp;

        while (posicao != indiceInicial) {
            posicao = indiceInicial + ContarElementosMenores(array, indiceInicial + 1, item, comparador);
            posicao = PularElementosIguais(array, posicao, item, comparador);

            temp = array[posicao];
            array[posicao] = item;
            item = temp;
        }
    }

    private static int PularElementosIguais(T[] array, int proximoIndice, T item, IComparer<T> comparador) {
        while (comparador.Compare(array[proximoIndice], item) == 0) {
            proximoIndice++;
        }

        return proximoIndice;
    }

    private static int ContarElementosMenores(T[] array, int indiceInicial, T elemento, IComparer<T> comparador) {
        var elementosMenores = 0;
        for (var i = indiceInicial; i < array.Length; i++) {
            if (comparador.Compare(array[i], elemento) < 0) {
                elementosMenores++;
            }
        }

        return elementosMenores;
    }
}
public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}