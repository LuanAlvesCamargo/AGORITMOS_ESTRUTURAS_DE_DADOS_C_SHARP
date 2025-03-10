using System;
using System.Collections.Generic;

namespace DataStructures.Heap.PairingHeap;

public class ComparadorNoPareamento<T> : IComparer<T> where T : IComparable {
    // Comparador de nós
    private readonly bool ehMax;
    private readonly IComparer<T> comparadorNo;

    public ComparadorNoPareamento(Ordenacao direcaoOrdenacao, IComparer<T> comparador) {
        ehMax = direcaoOrdenacao == Ordenacao.Decrescente;
        comparadorNo = comparador;
    }

    public int Compare(T? x, T? y) {
        return !ehMax
            ? CompararNos(x, y)
            : CompararNos(y, x);
    }

    private int CompararNos(T? um, T? dois) {
        return comparadorNo.Compare(um, dois);
    }
}

public enum Ordenacao {
    Crescente,
    Decrescente
}
