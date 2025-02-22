using System;

namespace Algoritmos.Numericos;

    // Implementação da função SoftMax.
    // Esta função recebe um vetor de K números reais e normaliza
    // seus valores em uma distribuição de probabilidade,
    // onde cada elemento é proporcional ao exponencial do valor original.
    // Após a aplicação da SoftMax, a soma dos elementos do vetor será sempre 1.
    // softmax é uma função que converte um vetor de números em probabilidades. 
    // Note que o vetor de entrada pode conter qualquer número (de -infinito a +infinito), 
    // enquanto a saída é um vetor de probabilidades, então o mínimo de cada elemento 
    // deve ser zero e a soma de todos os elementos deve ser igual a 1.
public static class FuncaoSoftMax
{
    // Calcula a função SoftMax.
    // A SoftMax é definida como:
    // softmax(x_i) = exp(x_i) / soma(exp(x_j)) para j = 1 até n
    // onde x_i é o i-ésimo elemento do vetor de entrada.
    // O vetor resultante contém probabilidades que somam 1.
    // Vetor de entrada contendo números reais.
    // Vetor de saída com os valores normalizados.
    public static double[] Calcular(double[] entrada) {
        if (entrada.Length == 0) {
            throw new ArgumentException("O vetor de entrada está vazio.");
        }

        var vetorExponencial = new double[entrada.Length];
        var soma = 0.0;
        for (var indice = 0; indice < entrada.Length; indice++) {
            vetorExponencial[indice] = Math.Exp(entrada[indice]);
            soma += vetorExponencial[indice];
        }

        for (var indice = 0; indice < entrada.Length; indice++) {
            vetorExponencial[indice] /= soma;
        }

        return vetorExponencial;
    }
}
