namespace Algoritmos.Strings.BuscaPadrao;

public static class BuscaSubstringZblock {
    public static int EncontrarSubstring(string padrao, string texto) {
        var stringConcatenada = $"{padrao}${texto}";
        var comprimentoPadrao = padrao.Length;
        var n = stringConcatenada.Length;
        var vetorZ = new int[n];
        var esquerda = 0;
        var direita = 0;

        for (var i = 1; i < n; i++) {
            if (i > direita) {
                esquerda = i;
                direita = CalcularNovoValorDireita(stringConcatenada, n, esquerda, i);

                vetorZ[i] = direita - esquerda;
                direita--;
            } else {
                var k = i - esquerda;
                if (vetorZ[k] < (direita - i + 1)) {
                    vetorZ[i] = vetorZ[k];
                } else {
                    esquerda = i;
                    direita = CalcularNovoValorDireita(stringConcatenada, n, esquerda, direita);
                    vetorZ[i] = direita - esquerda;
                    direita--;
                }
            }
        }

        var encontrado = 0;
        foreach (var valorZ in vetorZ) {
            if (valorZ == comprimentoPadrao) {
                encontrado++;
            }
        }
        return encontrado;
    }

    private static int CalcularNovoValorDireita(string stringConcatenada, int n, int esquerda, int direita) {
        while (direita < n && stringConcatenada[direita - esquerda].Equals(stringConcatenada[direita])) {
            direita++;
        }
        return direita;
    }
}