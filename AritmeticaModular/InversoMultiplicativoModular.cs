using System;
using System.Numerics;

namespace Algoritmos.AritmeticaModular
{

    // Classe para calcular o inverso multiplicativo modular.
    // O inverso existe se e somente se "a" e "n" forem coprimos (MDC(a, n) = 1).
    public static class InversoMultiplicativoModular {
        public static long Calcular(long a, long n) {
            var resultadoEEA = AlgoritmoEuclidianoEstendido.Calcular(a, n);

            // Verifica se "a" e "n" são coprimos, condição necessária para existência do inverso.
            if (resultadoEEA.Mdc != 1) {
                throw new ArithmeticException($"{a} não é invertível em Z/{n}Z.");
            }

            // Ajusta o coeficiente de Bézout de "a" para garantir que ele esteja no intervalo [0, n).
            var inversoDeA = resultadoEEA.CoefficienteBezoutA;
            if (inversoDeA < 0) {
                inversoDeA += n;
            }

            return inversoDeA;
        }
        public static BigInteger Calcular(BigInteger a, BigInteger n) {
            var resultadoEEA = AlgoritmoEuclidianoEstendido.Calcular(a, n);

            // Verifica se "a" e "n" são coprimos, condição necessária para existência do inverso.
            if (resultadoEEA.Mdc != 1) {
                throw new ArithmeticException($"{a} não é invertível em Z/{n}Z.");
            }

            // Ajusta o coeficiente de Bézout de "a" para garantir que ele esteja no intervalo [0, n).
            var inversoDeA = resultadoEEA.CoefficienteBezoutA;
            if (inversoDeA < 0) {
                inversoDeA += n;
            }

            return inversoDeA;
        }
    }
}
