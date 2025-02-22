using System;

namespace Algoritmos.Numericos;

    // Na teoria dos números, um número perfeito é um número inteiro positivo que é igual à soma de seus
    // divisores positivos, excluindo ele mesmo. Por exemplo, o número 6 possui os divisores 1, 2 e 3 (sem contar ele próprio),
    // e 1 + 2 + 3 = 6, portanto, 6 é um número perfeito.
    // Dizemos que um número natural é perfeito se é igual à soma de todos os seus fatores (divisores), 
    // excluindo ele próprio. Por exemplo, 6 e 28 são números perfeitos, 
    // veja: 6 = 1 + 2 + 3 (fatores de 6: 1, 2, 3 e 6), 
    // excluímos o número 6. 28 = 1 + 2 + 4 + 7 + 14 (fatores de 28: 1, 2, 4, 7, 14, 28), excluímos o 28.
    // public static class VerificadorDeNumeroPerfeito
{
    public static bool EhNumeroPerfeito(int numero) {
        if (numero < 0) {
            throw new ArgumentException($"{nameof(numero)} não pode ser negativo");
        }

        var somaDivisores = 0; /* Armazena a soma dos divisores positivos do número */
        for (var i = 1; i < numero; ++i) {
            if (numero % i == 0) {
                somaDivisores += i;
            }
        }
        return somaDivisores == numero;
    }
}
