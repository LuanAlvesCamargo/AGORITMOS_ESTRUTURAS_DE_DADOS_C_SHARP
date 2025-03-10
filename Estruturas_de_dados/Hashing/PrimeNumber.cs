using System;
using System.Collections.Generic;
using System.Linq;

namespace EstruturasDeDados.Hashing.TeoriaDosNumeros;

public static class NumeroPrimo {
    public static bool EhPrimo(int numero) {
        // Números menores ou iguais a 1 não são primos
        if (numero <= 1) {
            return false;
        }

        // 2 e 3 são primos
        if (numero <= 3) {
            return true;
        }

        // Números pares maiores que 2 e múltiplos de 3 não são primos
        if (numero % 2 == 0 || numero % 3 == 0) {
            return false;
        }

        // Verifica divisibilidade por números da forma 6k ± 1
        for (int i = 5; i * i <= numero; i += 6) {
            if (numero % i == 0 || numero % (i + 2) == 0) {
                return false;
            }
        }

        // Se não encontrou divisores, o número é primo
        return true;
    }
    public static int ProximoPrimo(int numero, int fator = 1, bool desc = false) {
        // Aplica o fator ao número inicial
        numero = fator * numero;
        int primeiroValor = numero;
        // Loop até encontrar um número primo
        while (!EhPrimo(numero)) {
            // Incrementa ou decrementa o número, dependendo da direção da busca
            numero += desc ? -1 : 1;
        }
        // Se o número encontrado for o mesmo que o número inicial, busca o próximo
        if (numero == primeiroValor) {
            return ProximoPrimo(
                numero + (desc ? -1 : 1),
                fator,
                desc);
        }

        // Retorna o número primo encontrado
        return numero;
    }
}