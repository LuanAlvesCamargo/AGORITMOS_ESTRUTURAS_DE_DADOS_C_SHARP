using System;

namespace Algorithms.Strings.PatternMatching;

public static class Bitap {
    public static int EncontrarPadraoExato(string texto, string padrao) {
        var comprimentoPadrao = padrao.Length;
        var mascaraPadrao = new int[128];
        int indice;

        if (string.IsNullOrEmpty(padrao)) {
            return 0;
        }
        if (comprimentoPadrao > 31) {
            throw new ArgumentException("O padrão é maior que 31 caracteres.");
        }
        var registroR = ~1;
        for (indice = 0; indice <= 127; ++indice) {
            mascaraPadrao[indice] = ~0;
        }
        for (indice = 0; indice < comprimentoPadrao; ++indice) {
            mascaraPadrao[padrao[indice]] &= ~(1 << indice);
        }
        for (indice = 0; indice < texto.Length; ++indice) {
            registroR |= mascaraPadrao[texto[indice]];
            registroR <<= 1;
            if ((registroR & 1 << comprimentoPadrao) == 0) {
                return indice - comprimentoPadrao + 1;
            }
        }
        return -1;
    }

    public static int EncontrarPadraoFuzzy(string texto, string padrao, int limiar) {
        var mascaraPadrao = new int[128];
        var registroR = new int[(limiar + 1) * sizeof(int)];
        var comprimentoPadrao = padrao.Length;
        if (string.IsNullOrEmpty(texto)) {
            return 0;
        }
        if (string.IsNullOrEmpty(padrao)) {
            return 0;
        }
        if (comprimentoPadrao > 31) {
            return -1;
        }
        for (var i = 0; i <= limiar; ++i) {
            registroR[i] = ~1;
        }
        for (var i = 0; i <= 127; i++) {
            mascaraPadrao[i] = ~0;
        }
        for (var i = 0; i < comprimentoPadrao; ++i) {
            mascaraPadrao[padrao[i]] &= ~(1 << i);
        }
        for (var i = 0; i < texto.Length; ++i) {
            var registroAntigoR = registroR[0];

            registroR[0] |= mascaraPadrao[texto[i]];
            registroR[0] <<= 1;
            for (var j = 1; j <= limiar; ++j) {
                var temp = registroR[j];

                registroR[j] = (registroAntigoR & (registroR[j] | mascaraPadrao[texto[i]])) << 1;
                registroAntigoR = temp;
            }

            if ((registroR[limiar] & 1 << comprimentoPadrao) == 0) {
                return i - comprimentoPadrao + 1;
            }
        }
        return -1;
    }
}