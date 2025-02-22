using System;

namespace Algoritmos.Numericos;

public class CalculadorMdcBinario : ICalculadorMdc {
    public int CalcularMdc(int a, int b) {
        // Caso especial: MDC(0, 0) = 0
        if (a == 0 && b == 0) {
            return 0;
        }

        // Se um dos números for 0, o MDC é o outro número
        if (a == 0 || b == 0) {
            return a + b;
        }

        // O MDC de números negativos é o mesmo de seus valores absolutos
        a = Math.Sign(a) * a;
        b = Math.Sign(b) * b;

        // Determina o maior fator de 2 comum a ambos os números
        var fatorDeDois = 0;
        while (((a | b) & 1) == 0) {
            a >>= 1;
            b >>= 1;
            fatorDeDois++;
        }

        // Garante que 'a' seja ímpar
        while ((a & 1) == 0) {
            a >>= 1;
        }

        // Algoritmo principal
        do {
            // Remove os fatores de 2 de 'b'
            while ((b & 1) == 0) {
                b >>= 1;
            }

            // Garante que 'a' seja sempre o menor número
            if (a > b) {
                var temp = b;
                b = a;
                a = temp;
            }

            // Subtrai 'a' de 'b', garantindo que a diferença seja par
            b -= a;
        }
        while (b != 0);

        // Restaura os fatores de 2 removidos anteriormente
        return a << fatorDeDois;
    }
}