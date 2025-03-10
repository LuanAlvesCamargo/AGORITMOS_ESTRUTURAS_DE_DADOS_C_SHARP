using System.Collections.Generic;
using System.Linq;

namespace Algoritmos.Strings;

public static class Permutacao {
    // Retorna todos os anagramas de uma palavra fornecida.
    public static List<string> ObterTodasPermutacoesUnicas(string palavra) {
        if (palavra.Length < 2) {
            return new List<string> {
                palavra,
            };
        }

        var resultado = new HashSet<string>();
        for (var i = 0; i < palavra.Length; i++) {
            var temp = ObterTodasPermutacoesUnicas(palavra.Remove(i, 1));

            resultado.UnionWith(temp.Select(subPerm => palavra[i] + subPerm));
        }
        return resultado.ToList();
    }
}