using System;

namespace Algoritmos.Numericos;

// A Decomposição LU fatora a matriz de entrada como o produto de uma matriz triangular inferior
// e uma matriz triangular superior.
// Em álgebra linear, a decomposição LU é uma forma de fatoração de uma matriz não singular
// como o produto de uma matriz triangular inferior e uma matriz triangular superior. 
// Às vezes se deve pré-multiplicar a matriz a ser decomposta por uma matriz de permutação.

public static class DecomposicaoLU {
    public static (double[,] L, double[,] U) Decompor(double[,] matriz) {
        if (matriz.GetLength(0) != matriz.GetLength(1)) {
            throw new ArgumentException("A matriz de entrada não é quadrada.");
        }

            var tamanho = matriz.GetLength(0);
            var inferior = new double[tamanho, tamanho];
            var superior = new double[tamanho, tamanho];

        for (var i = 0; i < tamanho; i++) {
            for (var k = i; k < tamanho; k++) {
                double soma = 0;

                for (var j = 0; j < i; j++) {
                    soma += inferior[i, j] * superior[j, k];
                }

                superior[i, k] = matriz[i, k] - soma;
            }

            for (var k = i; k < tamanho; k++) {
                if (i == k) {
                    inferior[i, i] = 1; // Elementos da diagonal principal da matriz inferior são 1
                }else{
                    double soma = 0;

                    for (var j = 0; j < i; j++) {
                        soma += inferior[k, j] * superior[j, i];
                    }

                    inferior[k, i] = (matriz[k, i] - soma) / superior[i, i];
                }
            }
        }

        return (L: inferior, U: superior);
    }

    public static double[] ResolverSistema(double[,] matriz, double[] termosIndependentes) {
        if (matriz.GetLength(0) != matriz.GetLength(1)) {
            throw new ArgumentException("A matriz dos coeficientes não é quadrada.");
        }
            var tamanho = matriz.GetLength(0);
            var vetorIntermediario = new double[tamanho, 1]; // U * vetorIntermediario = termosIndependentes
            var solucao = new double[tamanho]; // L * solucao = vetorIntermediario
            (double[,] l, double[,] u) = Decompor(matriz);

        // Resolvendo L * vetorIntermediario = termosIndependentes
        for (var i = 0; i < tamanho; i++) {
            double soma = 0;

                for (var j = 0; j < i; j++) {
                    soma += vetorIntermediario[j, 0] * l[i, j];
                }
            vetorIntermediario[i, 0] = (termosIndependentes[i] - soma) / l[i, i];
        }

        // Resolvendo U * solucao = vetorIntermediario
        for (var i = tamanho - 1; i >= 0; i--) {
            double soma = 0;

            for (var j = i; j < tamanho; j++) {
                soma += solucao[j] * u[i, j];
            }
        solucao[i] = (vetorIntermediario[i, 0] - soma) / u[i, i];
        }
        return solucao;
    }
}
