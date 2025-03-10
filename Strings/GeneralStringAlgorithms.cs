using System;

namespace Algoritmos.Strings;

// Implementa algoritmos simples em strings.
public static class AlgoritmosGeraisString { 
    public static Tuple<char, int> EncontrarMaiorSubstringConsecutiva(string entrada) {
        var caractereMaximo = entrada[0];
        var maximo = 1;
        var atual = 1;
        for (var i = 1; i < entrada.Length; i++) {
            if (entrada[i] == entrada[i - 1]) {
                atual++;
                if (atual > maximo) {
                    maximo = atual;
                    caractereMaximo = entrada[i];
                }
            } else {
                atual = 1;
            }
        }
        return new Tuple<char, int>(caractereMaximo, maximo);
    }
}