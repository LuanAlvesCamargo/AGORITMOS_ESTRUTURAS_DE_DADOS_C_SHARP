using System.Numerics;

namespace Algoritmos.AritmeticaModular;

    // Algoritmo de Euclides Estendido: 
    // Esse algoritmo é utilizado para encontrar o maior divisor comum (MDC) entre dois números e também os coeficientes de Bézout,
    // que são inteiros x e y tais que a*x + b*y = MDC(a, b).
    // O Algoritmo de Euclides estendido é uma extensão do algoritmo de Euclides, que, além de calcular o máximo divisor 
    // comum entre fornece os coeficientes tais que O algoritmo é utilizado, em especial, para o cálculo de inverso modular.
    // Calcula o maior divisor comum (MDC) entre dois números inteiros e determina os coeficientes de Bézout.
public static class AlgoritmoEuclidesEstendido {

    public static ResultadoEuclidesEstendido<long> Calcular(long a, long b) {
        long quociente;
        long temp;
        var s = 0L;
        var coeficienteBezoutA = 1L;
        var r = b;
        var mdc = a;
        var coeficienteBezoutB = 0L;

        while (r != 0) { 
            quociente = mdc / r; // Divisão inteira

            temp = mdc;
            mdc = r;
            r = temp - quociente * r;

            temp = coeficienteBezoutA;
            coeficienteBezoutA = s;
            s = temp - quociente * s;
        }

        if (b != 0) {
            coeficienteBezoutB = (mdc - coeficienteBezoutA * a) / b; // Divisão inteira
        }

        return new ResultadoEuclidesEstendido<long>(coeficienteBezoutA, coeficienteBezoutB, mdc);
    }

    public static ResultadoEuclidesEstendido<BigInteger> Calcular(BigInteger a, BigInteger b) {
        BigInteger quociente;
        BigInteger temp;
        var s = BigInteger.Zero;
        var coeficienteBezoutA = BigInteger.One;
        var r = b;
        var mdc = a;
        var coeficienteBezoutB = BigInteger.Zero;

        while (r != 0) {
            quociente = mdc / r; // Divisão inteira

            temp = mdc;
            mdc = r;
            r = temp - quociente * r;

            temp = coeficienteBezoutA;
            coeficienteBezoutA = s;
            s = temp - quociente * s;
        }

        if (b != 0) {
            coeficienteBezoutB = (mdc - coeficienteBezoutA * a) / b; // Divisão inteira
        }

        return new ResultadoEuclidesEstendido<BigInteger>(coeficienteBezoutA, coeficienteBezoutB, mdc);
    }

    public record ResultadoEuclidesEstendido<T>(T CoeficienteBezoutA, T CoeficienteBezoutB, T Mdc);
}
