using System;

namespace Algorithms.Strings.PatternMatching;

public static class BoyerMoore {
    public static int EncontrarPrimeiraOcorrencia(string texto, string padrao) {
        var m = padrao.Length;
        var n = texto.Length;
        int[] regraCaractereRuim = RegraCaractereRuim(padrao, m);
        int[] regraSufixoBom = RegraSufixoBom(padrao, m);
        var i = 0;
        int j;

        while (i <= n - m) {
            j = m - 1;
            while (j >= 0 && padrao[j] == texto[i + j]) {
                j--;
            }
            if (j < 0) {
                return i;
            }
            i += Math.Max(regraSufixoBom[j + 1], j - regraCaractereRuim[texto[i + j]]);
        }
        return -1;
    }

    private static int[] RegraCaractereRuim(string padrao, int m) {
        int[] caractereRuim = new int[256];
        Array.Fill(caractereRuim, -1);
        for (var j = 0; j < m; j++) {
            caractereRuim[padrao[j]] = j;
        }
        return caractereRuim;
    }

    private static int[] RegraSufixoBom(string padrao, int m) {
        int[] f = new int[padrao.Length + 1];
        f[m] = m + 1;
        int[] s = new int[padrao.Length + 1];
        var i = m;
        var j = m + 1;
        while (i > 0) {
            while (j <= m && padrao[i - 1] != padrao[j - 1]) {
                if (s[j] == 0) {
                    s[j] = j - i;
                }
                j = f[j];
            }
            --i;
            --j;
            f[i] = j;
        }

        j = f[0];
        for (i = 0; i <= m; i++) {
            if (s[i] == 0) {
                s[i] = j;
            }
            if (i == j) {
                j = f[j];
            }
        }
        return s;
    }
}