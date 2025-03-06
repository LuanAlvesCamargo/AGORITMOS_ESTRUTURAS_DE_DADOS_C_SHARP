using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Ordena arrays usando o algoritmo Quicksort, selecionando o ponto médio como pivô.
    // O algoritmo quicksort é um método de ordenação muito rápido e eficiente, inventado por C.A.R. Hoare em 1960[1], 
    // quando visitou a Universidade de Moscovo como estudante. Naquela época, Hoare trabalhou em um projeto de tradução 
    // de máquina para o National Physical Laboratory. Ele criou o quicksort ao tentar traduzir um dicionário de inglês para russo, 
    // ordenando as palavras, tendo como objetivo reduzir o problema original em subproblemas que possam ser resolvidos mais fácil e rápido. 
    // Foi publicado em 1962 após uma série de refinamentos.
    // O quicksort adota a estratégia de divisão e conquista. A estratégia consiste em rearranjar as chaves de modo que as chaves "menores" 
    // precedam as chaves "maiores". Em seguida o quicksort ordena as duas sublistas de chaves menores e maiores recursivamente até que 
    // a lista completa se encontre ordenada. [3]Os passos são:

    // Escolha um elemento da lista, denominado pivô;
    // Particiona: rearranje a lista de forma que todos os elementos anteriores ao pivô sejam menores que ele, e todos os elementos posteriores 
    // ao pivô sejam maiores que ele. Ao fim do processo o pivô estará em sua posição final e haverá duas sub listas não ordenadas. 
    // Essa operação é denominada partição;
    // Recursivamente ordene a sub lista dos elementos menores e a sub lista dos elementos maiores;
    // O caso base da recursão são as listas de tamanho zero ou um, que estão sempre ordenadas. O processo é finito, 
    // pois a cada iteração pelo menos um elemento é posto em sua posição final e não será mais manipulado na iteração seguinte.

    // A escolha do pivô e os passos do Particiona podem ser feitos de diferentes formas e a escolha de uma implementação 
    // específica afeta fortemente a performance do algoritmo.
public sealed class OrdenadorQuicksortPontoMedio<T> : OrdenadorQuicksort<T> {
    protected override T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita) =>
        array[esquerda + (direita - esquerda) / 2];
}
public abstract class OrdenadorQuicksort<T> : IOrdenadorComparacao<T> {
    public void Ordenar(T[] array, IComparer<T> comparador) {
        OrdenarQuicksortRecursivo(array, comparador, 0, array.Length - 1);
    }
    private void OrdenarQuicksortRecursivo(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        if (esquerda < direita) {
            int indicePivo = Particionar(array, comparador, esquerda, direita);
            OrdenarQuicksortRecursivo(array, comparador, esquerda, indicePivo - 1);
            OrdenarQuicksortRecursivo(array, comparador, indicePivo + 1, direita);
        }
    }
    private int Particionar(T[] array, IComparer<T> comparador, int esquerda, int direita)
    {
        T pivo = SelecionarPivo(array, comparador, esquerda, direita);
        int i = esquerda - 1;

        for (int j = esquerda; j < direita; j++) {
            if (comparador.Compare(array[j], pivo) <= 0) {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[direita]) = (array[direita], array[i + 1]);
        return i + 1;
    }
    protected abstract T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita);
}
public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}