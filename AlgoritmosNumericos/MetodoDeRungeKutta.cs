using System;
using System.Collections.Generic;

namespace Algoritmos.Numericos;

    // Na análise numérica, os métodos de Runge-Kutta formam uma família de métodos iterativos, 
    // implícitos e explícitos, utilizados para a discretização temporal e aproximação de soluções 
    // de equações diferenciais não lineares. 
    // O membro mais conhecido dessa família é o "RK4", também chamado de 
    // "método clássico de Runge-Kutta" ou simplesmente "método de Runge-Kutta".

    // Em análise numérica, os métodos de Runge–Kutta formam uma família importante de metódos 
    // iterativos implícitos e explícitos para a resolução numérica de soluções de equações diferenciais ordinárias. 
    // Estas técnicas foram desenvolvidas por volta de 1900 pelos matemáticos C. Runge e M.W. Kutta.
public static class MetodoDeRungeKutta {
    public static List<double[]> MetodoClassicoRungeKutta(
        double xInicial,
        double xFinal,
        double tamanhoPasso,
        double yInicial,
        Func<double, double, double> funcao)
    {
        if (xInicial >= xFinal) {
            throw new ArgumentOutOfRangeException(
                nameof(xFinal),
                $"{nameof(xFinal)} deve ser maior que {nameof(xInicial)}");
        }

        if (tamanhoPasso <= 0) {
            throw new ArgumentOutOfRangeException(
                nameof(tamanhoPasso),
                $"{nameof(tamanhoPasso)} deve ser maior que zero");
        }

        List<double[]> pontos = new();
        double[] primeiroPonto = { xInicial, yInicial };
        pontos.Add(primeiroPonto);

        var yAtual = yInicial;
        var xAtual = xInicial;

        while (xAtual < xFinal) {
            var k1 = funcao(xAtual, yAtual);
            var k2 = funcao(xAtual + 0.5 * tamanhoPasso, yAtual + 0.5 * tamanhoPasso * k1);
            var k3 = funcao(xAtual + 0.5 * tamanhoPasso, yAtual + 0.5 * tamanhoPasso * k2);
            var k4 = funcao(xAtual + tamanhoPasso, yAtual + tamanhoPasso * k3);

            yAtual += (1.0 / 6.0) * tamanhoPasso * (k1 + 2 * k2 + 2 * k3 + k4);
            xAtual += tamanhoPasso;

            double[] novoPonto = { xAtual, yAtual };
            pontos.Add(novoPonto);
        }
        return pontos;
    }
}
