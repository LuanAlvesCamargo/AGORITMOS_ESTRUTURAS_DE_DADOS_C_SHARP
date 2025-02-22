using System;

namespace Algoritmos.Numericos;

// Classe que implementa a busca pelo máximo divisor comum (MDC)
// utilizando o algoritmo de Euclides.
// O algoritmo de Euclides é um método eficiente para encontrar o máximo divisor comum (MDC) 
// entre dois números inteiros. Ele se baseia no princípio de que o MDC de dois números 
// não muda se o menor número for subtraído do maior. O processo é repetido até que um dos números seja zero, 
// e o outro número restante será o MDC.

public class EncontrarMdcEuclidiano : IEncontrarMdc {
    public int CalcularMdc(int numero1, int numero2) {
        // Caso especial: se ambos os números forem zero, retorna o maior valor possível de um inteiro.
        if (numero1 == 0 && numero2 == 0) {
            return int.MaxValue;
        }

        // Se um dos números for zero, retorna o outro número (que é o MDC nesse caso).
        if (numero1 == 0 || numero2 == 0) {
            return numero1 + numero2;
        }

        // Variáveis auxiliares para calcular o MDC.
        var a = numero1;
        var b = numero2;
        var resto = a % b;

        // Aplica o algoritmo de Euclides até que o resto seja zero.
        while (resto != 0) {
            a = b;
            b = resto;
            resto = a % b;
        }

        // O valor final de b é o MDC.
        return b;
    }
}
