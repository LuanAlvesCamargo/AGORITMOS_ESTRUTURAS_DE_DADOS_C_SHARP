using System;

namespace Algoritmos.Numericos {
    // número de Armstrong, é um número inteiro que é igual à soma de seus próprios dígitos elevados à 
    // potência do número de dígitos. Em outras palavras, um número narcisista é um número que "se ama", 
    // pois ele se iguala à soma de seus dígitos elevados ao seu próprio número de dígitos.
    // Por exemplo, 370 é um número narcisista porque 3³ + 7³ + 0³ = 370.

    public static class VerificadorNumeroNarcisista {
        public static bool EhNarcisista(int numero) {
            var soma = 0;
            var temp = numero;
            var quantidadeDigitos = 0;
            
            // Conta o número de dígitos no número
            while (temp != 0) {
                quantidadeDigitos++;
                temp /= 10;
            }

            temp = numero;
            
            // Calcula a soma dos dígitos elevados à potência do número total de dígitos
            while (numero > 0) {
                var digito = numero % 10;
                var potencia = (int)Math.Pow(digito, quantidadeDigitos);
                
                soma += potencia;
                numero /= 10;
            }
            
            // Compara a soma calculada com o número original
            return soma == temp;
        }
    }
}
