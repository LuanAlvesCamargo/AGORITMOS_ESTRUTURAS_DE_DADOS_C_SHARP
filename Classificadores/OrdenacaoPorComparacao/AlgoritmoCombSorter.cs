using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Comb sort é um algoritmo de ordenação relativamente simples que melhora o bubble sort.
    // O algoritmo Comb sort é um algoritmo de ordenação relativamente simples, 
    // e faz parte da família de algoritmos de ordenação por troca. Foi desenvolvido em 1980 por Wlodzimierz Dobosiewicz.

public class OrdenadorPente<T> : IOrdenadorComparacao<T> {
    public OrdenadorPente(double fatorReducao = 1.3) => FatorReducao = fatorReducao;

    private double FatorReducao { get; }

    // Ordena um array usando o comparador especificado.
    // Características:
    // - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    // - Ordenação instável: a ordem relativa de elementos iguais não é preservada.
    // - Desempenho no pior caso: O(n^2).
    // - Desempenho no melhor caso: O(n log(n)).
    // - Desempenho médio: O(n^2 / 2^p), onde n é o comprimento do array e p é o número de incrementos.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        var espaco = array.Length;
        var ordenado = false;
        while (!ordenado) {
            espaco = (int)Math.Floor(espaco / FatorReducao);
            if (espaco <= 1) {
                espaco = 1;
                ordenado = true;
            }

            for (var i = 0; i < array.Length - espaco; i++) {
                if (comparador.Compare(array[i], array[i + espaco]) > 0) {
                    (array[i], array[i + espaco]) = (array[i + espaco], array[i]);
                    ordenado = false;
                }
            }
        }
    }
}
    // Interface para ordenadores que usam comparações entre elementos.
public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}