using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmos.Busca;


// Implementação do algoritmo de Boyer-Moore para encontrar o elemento majoritário em uma coleção.

public static class BoyerMoore<T> where T : IComparable {
    public static T? EncontrarMajoritario(IEnumerable<T> entrada) {
        var candidato = ObterCandidatoMajoritario(entrada, entrada.Count());

        if (VerificarMajoritario(entrada, entrada.Count(), candidato)) {
            return candidato;
        }
        return default(T?);
    }


    private static T ObterCandidatoMajoritario(IEnumerable<T> entrada, int tamanho) {
        int contagem = 1;
        T candidato = entrada.First();

        foreach (var elemento in entrada.Skip(1)) {
            if (candidato.Equals(elemento)) {
                contagem++;
            } else {
                contagem--;
            }

            if (contagem == 0) {
                candidato = elemento;
                contagem = 1;
            }
        }
        return candidato;
    }
    private static bool VerificarMajoritario(IEnumerable<T> entrada, int tamanho, T candidato) {
        return entrada.Count(x => x.Equals(candidato)) > tamanho / 2;
    }
}
