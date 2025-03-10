using System;

namespace Algorithms.Strings.Similarity {
    public static class AlinhamentoDeStringOtimo {
        public static double Calcular(string primeiraString, string segundaString) {
            ArgumentNullException.ThrowIfNull(nameof(primeiraString));
            ArgumentNullException.ThrowIfNull(nameof(segundaString));
            if (primeiraString == segundaString) {
                return 0.0;
            }
            if (primeiraString.Length == 0) {
                return segundaString.Length;
            }
            if (segundaString.Length == 0) {
                return primeiraString.Length;
            }
            var matrizDistancia = GerarMatrizDistancia(primeiraString.Length, segundaString.Length);
            matrizDistancia = CalcularDistancia(primeiraString, segundaString, matrizDistancia);
            return matrizDistancia[primeiraString.Length, segundaString.Length];
        }

        private static int[,] GerarMatrizDistancia(int primeiroComprimento, int segundoComprimento) {
            var matrizDistancia = new int[primeiroComprimento + 2, segundoComprimento + 2];
            for (var i = 0; i <= primeiroComprimento; i++) {
                matrizDistancia[i, 0] = i;
            }
            for (var j = 0; j <= segundoComprimento; j++) {
                matrizDistancia[0, j] = j;
            }
            return matrizDistancia;
        }

        private static int[,] CalcularDistancia(string primeiraString, string segundaString, int[,] matrizDistancia) {
            for (var i = 1; i <= primeiraString.Length; i++) {
                for (var j = 1; j <= segundaString.Length; j++) {
                    var custo = 1;

                    if (primeiraString[i - 1] == segundaString[j - 1]) {
                        custo = 0;
                    }
                    matrizDistancia[i, j] = Minimo(
                        matrizDistancia[i - 1, j - 1] + custo, // substituição
                        matrizDistancia[i, j - 1] + 1, // inserção
                        matrizDistancia[i - 1, j] + 1); // deleção
                    if (i > 1 && j > 1
                        && primeiraString[i - 1] == segundaString[j - 2]
                        && primeiraString[i - 2] == segundaString[j - 1]) {
                        matrizDistancia[i, j] = Math.Min(
                            matrizDistancia[i, j],
                            matrizDistancia[i - 2, j - 2] + custo); // transposição
                    }
                }
            }

            return matrizDistancia;
        }

        private static int Minimo(int a, int b, int c) {
            return Math.Min(a, Math.Min(b, c));
        }
    }
}