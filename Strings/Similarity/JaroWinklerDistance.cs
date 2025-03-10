using System.Linq;

namespace Algoritmos.Strings.Similaridade;

public static class DistanciaJaroWinkler {
    public static double Calcular(string s1, string s2, double fatorEscala = 0.1) {
        var similaridadeJaro = SimilaridadeJaro.Calcular(s1, s2);
        var comprimentoPrefixoComum = s1.Zip(s2).Take(4).TakeWhile(x => x.First == x.Second).Count();
        var similaridadeJaroWinkler = similaridadeJaro + comprimentoPrefixoComum * fatorEscala * (1 - similaridadeJaro);

        return 1 - similaridadeJaroWinkler;
    }
}