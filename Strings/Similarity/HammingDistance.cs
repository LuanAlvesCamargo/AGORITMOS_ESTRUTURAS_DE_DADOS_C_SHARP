using System;

namespace Algoritmos.Strings.Similaridade;

public static class DistanciaDeHamming {
    public static int Calcular(string s1, string s2) {
        if (s1.Length != s2.Length) {
            throw new ArgumentException("As strings devem ter o mesmo comprimento.");
        }

        var distancia = 0;
        for (var i = 0; i < s1.Length; i++) {
            distancia += s1[i] != s2[i] ? 1 : 0;
        }
        return distancia;
    }
}