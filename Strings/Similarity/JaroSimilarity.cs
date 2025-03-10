using System;

namespace Algorithms.Strings.Similarity;

public static class SimilaridadeJaro {
    public static double Calcular(string s1, string s2) {
        if (s1 == s2) {
            return 1;
        }

        var stringMaisLonga = s1.Length > s2.Length ? s1 : s2;
        var stringMaisCurta = s1.Length < s2.Length ? s1 : s2;
        // Procurará caracteres correspondentes neste intervalo
        var intervaloCaracteresCorrespondentes = Math.Max((stringMaisLonga.Length / 2) - 1, 0);
        var correspondencias = 0d;
        // Verdadeiro se o índice i-ésimo de s1 foi correspondido em s2
        var indicesS1Correspondidos = new bool[s1.Length];
        // Verdadeiro se o índice i-ésimo de s2 foi correspondido em s1
        var indicesS2Correspondidos = new bool[s2.Length];

        for (var i = 0; i < stringMaisLonga.Length; i++) {
            var indiceInicial = Math.Max(i - intervaloCaracteresCorrespondentes, 0);
            var indiceFinal = Math.Min(i + intervaloCaracteresCorrespondentes, stringMaisCurta.Length - 1);
            for (var j = indiceInicial; j <= indiceFinal; j++) {
                if (s1[i] == s2[j] && !indicesS2Correspondidos[j]) {
                    correspondencias++;
                    indicesS1Correspondidos[i] = true;
                    indicesS2Correspondidos[j] = true;
                    break;
                }
            }
        }

        if (correspondencias == 0) {
            return 0;
        }
        var transposicoes = CalcularTransposicoes(s1, s2, indicesS1Correspondidos, indicesS2Correspondidos);
        return ((correspondencias / s1.Length) + (correspondencias / s2.Length) + ((correspondencias - transposicoes) / correspondencias)) / 3;
    }

    private static int CalcularTransposicoes(string s1, string s2, bool[] indicesS1Correspondidos, bool[] indicesS2Correspondidos) {
        var transposicoes = 0;
        var indiceS2 = 0;
        for (var indiceS1 = 0; indiceS1 < s1.Length; indiceS1++) {
            if (indicesS1Correspondidos[indiceS1]) {
                while (!indicesS2Correspondidos[indiceS2]) {
                    indiceS2++;
                }

                if (s1[indiceS1] != s2[indiceS2]) {
                    transposicoes++;
                }
                indiceS2++;
            }
        }
        transposicoes /= 2;
        return transposicoes;
    }
}