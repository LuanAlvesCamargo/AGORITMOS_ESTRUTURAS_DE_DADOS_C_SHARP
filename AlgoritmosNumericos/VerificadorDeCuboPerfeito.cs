using System;

namespace Algoritmos.Numericos
{
    // Um cubo perfeito é um número que resulta do cubo de outro número inteiro.
    public static class VerificadorCuboPerfeito {
    // Verifica se um número é um cubo perfeito.
    // Número a ser verificado.
    // Verdadeiro se for um cubo perfeito; Falso caso contrário.
        public static bool EhCuboPerfeito(int numero) {
            if (numero < 0) {
                numero = -numero;
            }
            var raizCubica = Math.Round(Math.Pow(numero, 1.0 / 3.0));
            return Math.Abs(raizCubica * raizCubica * raizCubica - numero) < 1e-6;
        }

        // Verifica se um número é um cubo perfeito utilizando busca binária.
        // Número a ser verificado.
        // Verdadeiro se for um cubo perfeito; Falso caso contrário.
        public static bool EhCuboPerfeitoBuscaBinaria(int numero) {
            if (numero < 0) {
                numero = -numero;
            }

            int esquerda = 0;
            int direita = numero;
            
            while (esquerda <= direita) {
                int meio = esquerda + (direita - esquerda) / 2;
                int cuboMeio = meio * meio * meio;
                
                if (cuboMeio == numero) {
                    return true;
                } else if (cuboMeio < numero) {
                    esquerda = meio + 1;
                } else {
                    direita = meio - 1;
                }
            }
            return false;
        }
    }
}
