using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Externo;

// algoritmo de ordenação externa Merge Sort.


public class OrdenadorMergeExterno<T> : IOrdenadorExterno<T> {

    public void Ordenar(
        IArmazenamentoSequencial<T> memoriaPrincipal,
        IArmazenamentoSequencial<T> memoriaTemporaria,
        IComparer<T> comparador) {
        var fonteOriginal = memoriaPrincipal;
        var fonte = memoriaPrincipal;
        var temp = memoriaTemporaria;
        var comprimentoTotal = memoriaPrincipal.Comprimento;
        for (var comprimentoFaixa = 1L; comprimentoFaixa < comprimentoTotal; comprimentoFaixa *= 2) {
            using var esquerda = fonte.ObterLeitor();
            using var direita = fonte.ObterLeitor();
            using var saida = temp.ObterEscritor();

            for (var i = 0L; i < comprimentoFaixa; i++) {
                direita.Ler();
            }

            Mesclar(esquerda, direita, saida, comprimentoFaixa, Math.Min(comprimentoFaixa, comprimentoTotal - comprimentoFaixa), comparador);
            var passo = 2 * comprimentoFaixa;
            long inicioFaixaDireita;
            for (inicioFaixaDireita = comprimentoFaixa + passo; inicioFaixaDireita < memoriaPrincipal.Comprimento; inicioFaixaDireita += passo) {
                for (var i = 0L; i < comprimentoFaixa; i++) {
                    esquerda.Ler();
                    direita.Ler();
                }

                Mesclar(
                    esquerda,
                    direita,
                    saida,
                    comprimentoFaixa,
                    Math.Min(comprimentoFaixa, comprimentoTotal - inicioFaixaDireita),
                    comparador);
            }

            for (var i = 0L; i < comprimentoTotal + comprimentoFaixa - inicioFaixaDireita; i++) {
                saida.Escrever(direita.Ler());
            }

            (fonte, temp) = (temp, fonte);
        }

        if (fonte == fonteOriginal) {
            return;
        }

        using var ordenado = fonte.ObterLeitor();
        using var destino = fonteOriginal.ObterEscritor();
        for (var i = 0; i < comprimentoTotal; i++) {
            destino.Escrever(ordenado.Ler());
        }
    }
    private static void Mesclar(
        ILeitorArmazenamentoSequencial<T> esquerda,
        ILeitorArmazenamentoSequencial<T> direita,
        IEscritorArmazenamentoSequencial<T> saida,
        long comprimentoEsquerda,
        long comprimentoDireita,
        IComparer<T> comparador) {
        var indiceEsquerda = 0L;
        var indiceDireita = 0L;

        var l = esquerda.Ler();
        var r = direita.Ler();
        while (true) {
            if (comparador.Compare(l, r) < 0) {
                saida.Escrever(l);
                indiceEsquerda++;
                if (indiceEsquerda == comprimentoEsquerda) {
                    break;
                }

                l = esquerda.Ler();
            } else {
                saida.Escrever(r);
                indiceDireita++;
                if (indiceDireita == comprimentoDireita) {
                    break;
                }

                r = direita.Ler();
            }
        } 

        if (indiceEsquerda < comprimentoEsquerda) {
            saida.Escrever(l);
            Copiar(esquerda, saida, comprimentoEsquerda - indiceEsquerda - 1);
        }

        if (indiceDireita < comprimentoDireita) {
            saida.Escrever(r);
            Copiar(direita, saida, comprimentoDireita - indiceDireita - 1);
        }
    }
    private static void Copiar(ILeitorArmazenamentoSequencial<T> fonte, IEscritorArmazenamentoSequencial<T> destino, long contagem) {
        for (var i = 0; i < contagem; i++) {
            destino.Escrever(fonte.Ler());
        }
    }
}

public interface IOrdenadorExterno<T> {
    // Ordena dados em armazenamento sequencial externo.
    void Ordenar(IArmazenamentoSequencial<T> memoriaPrincipal, IArmazenamentoSequencial<T> memoriaTemporaria, IComparer<T> comparador);
}
public interface IArmazenamentoSequencial<T> {
    long Comprimento { get; }
    ILeitorArmazenamentoSequencial<T> ObterLeitor();
    IEscritorArmazenamentoSequencial<T> ObterEscritor();
}
public interface ILeitorArmazenamentoSequencial<T> : IDisposable {
    T Ler();
}

public interface IEscritorArmazenamentoSequencial<T> : IDisposable {
    void Escrever(T elemento);
}