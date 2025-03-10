using System;
using System.Collections.Generic;

namespace Algorithms.Strings.PatternMatching;

public static class RabinKarp {

    public static List<int> EncontrarTodasOcorrencias(string texto, string padrao) {
        // Número primo
        const ulong p = 65537;

        // Coeficiente módulo
        const ulong m = (ulong)1e9 + 7;

        // p_pow[i] = P^i mod M
        ulong[] pPow = new ulong[Math.Max(padrao.Length, texto.Length)];
        pPow[0] = 1;
        for (var i = 1; i < pPow.Length; i++) {
            pPow[i] = pPow[i - 1] * p % m;
        }

        // hash_t[i] é a soma dos valores de hash anteriores das letras (t[0], t[1], ..., t[i-1]) e o valor de hash de t[i] em si (mod M).
        // O valor de hash de uma letra t[i] é igual ao produto de t[i] e p_pow[i] (mod M).
        ulong[] hashT = new ulong[texto.Length + 1];
        for (var i = 0; i < texto.Length; i++) {
            hashT[i + 1] = (hashT[i] + texto[i] * pPow[i]) % m;
        }

        // hash_s é igual à soma dos valores de hash do padrão (mod M).
        ulong hashS = 0;
        for (var i = 0; i < padrao.Length; i++) {
            hashS = (hashS + padrao[i] * pPow[i]) % m;
        }

        // No próximo passo, você itera sobre o texto com o padrão.
        List<int> ocorrencias = new();
        for (var i = 0; i + padrao.Length - 1 < texto.Length; i++) {
            // Em cada passo, você calcula o valor de hash da substring a ser testada.
            // Ao armazenar os valores de hash das letras como prefixos, você pode fazer isso em tempo constante.
            var hashAtual = (hashT[i + padrao.Length] + m - hashT[i]) % m;

            // Agora você pode comparar o valor de hash da substring com o produto do valor de hash do padrão e p_pow[i].
            if (hashAtual == hashS * pPow[i] % m) {
                // Se os valores de hash forem idênticos, faça uma verificação dupla caso ocorra uma colisão de hash.
                var j = 0;
                while (j < padrao.Length && texto[i + j] == padrao[j]) {
                    ++j;
                }

                if (j == padrao.Length) {
                    // Se os valores de hash forem idênticos e a verificação dupla passar, uma substring foi encontrada que corresponde ao padrão.
                    // Nesse caso, você adiciona o índice i à lista de ocorrências.
                    ocorrencias.Add(i);
                }
            }
        }

        return ocorrencias;
    }
}