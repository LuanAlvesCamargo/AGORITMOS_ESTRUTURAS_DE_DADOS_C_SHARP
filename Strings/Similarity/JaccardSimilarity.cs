using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

public class SimilaridadeJaccard {
    public double Calcular(string esquerda, string direita) {
        // Valida as strings de entrada antes de prosseguir.
        ValidarEntrada(esquerda, direita);

        // Obtém os comprimentos das strings de entrada.
        var comprimentoEsquerda = esquerda.Length;
        var comprimentoDireita = direita.Length;

        // Se ambas as strings estiverem vazias, retorna 1.0 como o coeficiente de similaridade.
        if (comprimentoEsquerda == 0 && comprimentoDireita == 0) {
            return 1.0d;
        }

        // Se qualquer uma das strings estiver vazia, retorna 0.0 como o coeficiente de similaridade.
        if (comprimentoEsquerda == 0 || comprimentoDireita == 0) {
            return 0.0d;
        }

        // Obtém os caracteres únicos em cada string.
        var conjuntoEsquerda = new HashSet<char>(esquerda);
        var conjuntoDireita = new HashSet<char>(direita);

        // Obtém a união dos dois conjuntos.
        var conjuntoUniao = new HashSet<char>(conjuntoEsquerda);
        foreach (var c in conjuntoDireita) {
            conjuntoUniao.Add(c);
        }

        // Calcula o tamanho da interseção dos dois conjuntos.
        var tamanhoIntersecao = conjuntoEsquerda.Count + conjuntoDireita.Count - conjuntoUniao.Count;

        // Retorna o coeficiente de similaridade de Jaccard como a razão da interseção para a união.
        return 1.0d * tamanhoIntersecao / conjuntoUniao.Count;
    }
    private void ValidarEntrada(string esquerda, string direita) {
        if (esquerda == null || direita == null) {
            var nomeParametro = esquerda == null ? nameof(esquerda) : nameof(direita);
            throw new ArgumentNullException(nomeParametro, "Entrada não pode ser nula");
        }
    }
}