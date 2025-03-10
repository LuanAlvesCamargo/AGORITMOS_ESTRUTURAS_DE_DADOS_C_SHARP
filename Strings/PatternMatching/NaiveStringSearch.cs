using System.Collections.Generic;

namespace Algorithms.Strings.PatternMatching;

public static class BuscaStringIngenua {

    public static int[] BuscaIngenua(string conteudo, string padrao) {
        var m = padrao.Length;
        var n = conteudo.Length;
        List<int> indices = new();
        for (var e = 0; e <= n - m; e++) {
            int j;
            for (j = 0; j < m; j++) {
                if (conteudo[e + j] != padrao[j]) {
                    break;
                }
            }

            if (j == m) {
                indices.Add(e);
            }
        }
        return indices.ToArray();
    }
}