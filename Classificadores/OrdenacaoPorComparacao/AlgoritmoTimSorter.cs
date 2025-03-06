using System;
using System.Collections.Generic;
using Algoritmos.Ordenacao.Utils;

namespace Algoritmos.Ordenacao.Comparacao;


// Timsort é um algoritmo de ordenação híbrido estável, derivado do merge sort e do insertion sort,
// projetado para ter bom desempenho em muitos tipos de dados do mundo real.
// Foi originalmente implementado por Tim Peters em 2002 para uso na linguagem de programação Python.

public class OrdenadorTim<T> : IOrdenadorComparacao<T> {
    private readonly int tamanhoMinimoMerge;
    private readonly int galopeInicialMinimo;

    // Pool de objetos TimChunk reutilizáveis para eficiência de memória.
    private readonly TimChunk<T>[] poolChunks = new TimChunk<T>[2];
    private readonly int[] basesExecucoes;
    private readonly int[] comprimentosExecucoes;
    private int galopeMinimo;
    private int tamanhoPilha;
    private IComparer<T> comparador = default!;

    private class TimChunk<Tc> {
        public Tc[] Array { get; set; } = default!;
        public int Indice { get; set; }
        public int Restante { get; set; }
        public int Vitorias { get; set; }
    }

    public OrdenadorTim(ConfiguracoesOrdenadorTim configuracoes, IComparer<T> comparador) {
        galopeInicialMinimo = galopeMinimo;
        basesExecucoes = new int[85];
        comprimentosExecucoes = new int[85];
        tamanhoPilha = 0;
        galopeMinimo = configuracoes.GalopeMinimo;
        tamanhoMinimoMerge = configuracoes.TamanhoMinimoMerge;
        this.comparador = comparador ?? Comparer<T>.Default;
    }

    public void Ordenar(T[] array, IComparer<T> comparador) {
        this.comparador = comparador;
        var inicio = 0;
        var restante = array.Length;

        if (restante < tamanhoMinimoMerge) {
            if (restante < 2) {
                // Arrays de tamanho 0 ou 1 estão sempre ordenados.
                return;
            }

            // Não precisa mesclar, apenas ordenação binária
            OrdenacaoBinaria(array, inicio, restante, inicio);
            return;
        }

        var comprimentoMinimoExecucao = ComprimentoMinimoExecucao(restante, tamanhoMinimoMerge);

        do {
            // Identifica a próxima execução
            var comprimentoExecucao = ContarExecucaoEColocarAscendente(array, inicio);

            // Se a execução for muito curta, estenda para Min(COMPRIMENTO_MINIMO_EXECUCAO, restante)
            if (comprimentoExecucao < comprimentoMinimoExecucao) {
                var forcar = Math.Min(comprimentoMinimoExecucao, restante);
                OrdenacaoBinaria(array, inicio, inicio + forcar, inicio + comprimentoExecucao);
                comprimentoExecucao = forcar;
            }

            basesExecucoes[tamanhoPilha] = inicio;
            comprimentosExecucoes[tamanhoPilha] = comprimentoExecucao;
            tamanhoPilha++;

            MesclarColapso(array);

            inicio += comprimentoExecucao;
            restante -= comprimentoExecucao;
        } while (restante != 0);

        MesclarColapsoForcado(array);
    }

    // Retorna o comprimento mínimo aceitável de execução para um array do comprimento especificado.
    // Execuções naturais mais curtas que isso serão estendidas.
    // O cálculo é:
    // Se o total for menor que comprimentoMinimoExecucao, retorne n (é muito pequeno para se preocupar com coisas sofisticadas).
    // Caso contrário, se o total for uma potência exata de 2, retorne comprimentoMinimoExecucao/2.
    // Caso contrário, retorne um int k, onde <![CDATA[comprimentoMinimoExecucao/2 <= k <= comprimentoMinimoExecucao]]>, tal que total/k
    // esteja próximo, mas estritamente menor que, uma potência exata de 2.
    private static int ComprimentoMinimoExecucao(int total, int tamanhoMinimoMerge) {
        var r = 0;
        while (total >= tamanhoMinimoMerge) {
            r |= total & 1;
            total >>= 1;
        }

        return total + r;
    }

    private static void InverterIntervalo(T[] array, int inicio, int fim) {
        fim--;
        while (inicio < fim) {
            var t = array[inicio];
            array[inicio++] = array[fim];
            array[fim--] = t;
        }
    }

    // Verifica os chunks antes de entrar em um merge para garantir que haja algo para realmente fazer.
    // TimChunk do lado esquerdo.
    // TimChunk do lado direito.
    // O ponto de destino atual para os valores restantes.
    // Se um merge é necessário.
    private static bool PrecisaMesclar(TimChunk<T> esquerda, TimChunk<T> direita, ref int destino) {
        direita.Array[destino++] = direita.Array[direita.Indice++];
        if (--direita.Restante == 0) {
            Array.Copy(esquerda.Array, esquerda.Indice, direita.Array, destino, esquerda.Restante);
            return false;
        }

        if (esquerda.Restante == 1) {
            Array.Copy(direita.Array, direita.Indice, direita.Array, destino, direita.Restante);
            direita.Array[destino + direita.Restante] = esquerda.Array[esquerda.Indice];
            return false;
        }

        return true;
    }
}