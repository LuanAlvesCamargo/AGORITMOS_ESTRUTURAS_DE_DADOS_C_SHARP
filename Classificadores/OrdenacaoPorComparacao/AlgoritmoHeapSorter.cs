using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

// Heap sort é uma técnica de ordenação baseada em comparação,
// fundamentada na estrutura de dados Heap Binário.
// Tipo do array de entrada.
public class OrdenadorHeap<T> : IOrdenadorComparacao<T> {
    public void Ordenar(T[] array, IComparer<T> comparador) => HeapSort(array, comparador);

    private static void HeapSort(IList<T> dados, IComparer<T> comparador) {
        var tamanhoHeap = dados.Count;
        for (var p = (tamanhoHeap - 1) / 2; p >= 0; p--) {
            ConstruirHeap(dados, tamanhoHeap, p, comparador);
        }

        for (var i = dados.Count - 1; i > 0; i--) {
            var temp = dados[i];
            dados[i] = dados[0];
            dados[0] = temp;
            tamanhoHeap--;
            ConstruirHeap(dados, tamanhoHeap, 0, comparador);
        }
    }

    private static void ConstruirHeap(IList<T> entrada, int tamanhoHeap, int indice, IComparer<T> comparador) {
        var indiceRaiz = indice;

        while (true) {
            var esquerda = (indiceRaiz + 1) * 2 - 1;
            var direita = (indiceRaiz + 1) * 2;
            var maior = esquerda < tamanhoHeap && comparador.Compare(entrada[esquerda], entrada[indiceRaiz]) == 1 ? esquerda : indiceRaiz;

            // Encontra o índice do maior
            if (direita < tamanhoHeap && comparador.Compare(entrada[direita], entrada[maior]) == 1) {
                maior = direita;
            } 
            if (maior == indiceRaiz) {
                return;
            }

            // Processo de re-heap / troca
            var temp = entrada[indiceRaiz];
            entrada[indiceRaiz] = entrada[maior];
            entrada[maior] = temp;
            indiceRaiz = maior;
        }
    }
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}