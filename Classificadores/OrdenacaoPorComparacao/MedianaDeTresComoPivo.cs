using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

// Ordena arrays usando quicksort (selecionando a mediana de três como pivô).
// O algoritmo quicksort é um método de ordenação muito rápido e eficiente, inventado por C.A.R. 
// Hoare em 1960, quando visitou a Universidade de Moscovo como estudante. 
// Naquela época, Hoare trabalhou em um projeto de tradução de máquina para o National Physical Laboratory.
public sealed class OrdenadorRapidoMedianaDeTres<T> : OrdenadorRapido<T> {
    protected override T SelecionarPivo(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        var pontoEsquerda = array[esquerda];
        var pontoMeio = array[esquerda + (direita - esquerda) / 2];
        var pontoDireita = array[direita];
        return EncontrarMediana(comparador, pontoEsquerda, pontoMeio, pontoDireita);
    }

    private static T EncontrarMediana(IComparer<T> comparador, T a, T b, T c) {
        if (comparador.Compare(a, b) <= 0) {
            // a <= b <= c
            if (comparador.Compare(b, c) <= 0) {
                return b;
            }
            // a <= c < b
            if (comparador.Compare(a, c) <= 0) {
                return c;
            }
            // c < a <= b
            return a;
        }

        // a > b >= c
        if (comparador.Compare(b, c) >= 0) {
            return b;
        }
        // a >= c > b
        if (comparador.Compare(a, c) >= 0) {
            return c;
        }
        // c > a > b
        return a;
    }
}

// Assumindo que QuickSorter (OrdenadorRapido) é uma classe base existente
// e IComparer (IComparador) é a interface de comparação padrão.
// Exemplo de como a classe OrdenadorRapido poderia ser (simplificado para demonstração):
public abstract class OrdenadorRapido<T> : IOrdenadorComparacao<T> {
    public void Ordenar(T[] array, IComparer<T> comparador) {
        OrdenarRecursivo(array, comparador, 0, array.Length - 1);
    }

    private void OrdenarRecursivo(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        if (esquerda < direita) {
            int indicePivo = Particionar(array, comparador, esquerda, direita);
            OrdenarRecursivo(array, comparador, esquerda, indicePivo - 1);
            OrdenarRecursivo(array, comparador, indicePivo + 1, direita);
        }
    }

    private int Particionar(T[] array, IComparer<T> comparador, int esquerda, int direita) {
        T pivo = SelecionarPivo(array, comparador, esquerda, direita);
        int i = esquerda - 1;
        for (int j = esquerda; j <= direita - 1; j++) {
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