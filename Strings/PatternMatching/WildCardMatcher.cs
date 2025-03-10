using System;

namespace Algorithms.Strings.PatternMatching;

public static class CorrespondedorCuringa {
    public static bool CorresponderPadrao(string entrada, string padrao) {
        if (padrao.Length > 0 && padrao[0] == '*') {
            throw new ArgumentException("Padrão não pode começar com *");
        }
        var comprimentoEntrada = entrada.Length + 1;
        var comprimentoPadrao = padrao.Length + 1;
        var dp = new bool[comprimentoEntrada, comprimentoPadrao];

        dp[0, 0] = true;

        for (var j = 1; j < comprimentoPadrao; j++) {
            if (padrao[j - 1] == '*') {
                dp[0, j] = dp[0, j - 2];
            }
        }

        for (var i = 1; i < comprimentoEntrada; i++) {
            for (var j = 1; j < comprimentoPadrao; j++) {
                CorresponderComprimentosRestantes(entrada, padrao, dp, i, j);
            }
        }
        return dp[comprimentoEntrada - 1, comprimentoPadrao - 1];
    }

    private static void CorresponderComprimentosRestantes(string entrada, string padrao, bool[,] dp, int i, int j) {
        if (entrada[i - 1] == padrao[j - 1] || padrao[j - 1] == '.') {
            dp[i, j] = dp[i - 1, j - 1];
        } else if (padrao[j - 1] == '*') {
            CorresponderZeroOuMais(entrada, padrao, dp, i, j);
        }
    }

    private static void CorresponderZeroOuMais(string entrada, string padrao, bool[,] dp, int i, int j) {
        if (dp[i, j - 2]) {
            dp[i, j] = true;
        } else if (entrada[i - 1] == padrao[j - 2] || padrao[j - 2] == '.') {
            dp[i, j] = dp[i - 1, j];
        }
    }
}