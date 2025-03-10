using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

public static class SimilaridadeCosseno {
    public static double Calcular(string esquerda, string direita) {
        // Passo 1: Obter os vetores para as duas strings
        // Cada vetor representa a frequência de cada caractere na string.
        var vetores = ObterVetores(esquerda.ToLowerInvariant(), direita.ToLowerInvariant());
        var vetorEsquerda = vetores.VetorEsquerda;
        var vetorDireita = vetores.VetorDireita;
        // Passo 2: Calcular a interseção dos dois vetores
        // A interseção é o conjunto de caracteres que aparecem em ambas as strings.
        var intersecao = ObterIntersecao(vetorEsquerda, vetorDireita);
        // Passo 3: Calcular o produto escalar dos dois vetores
        // O produto escalar é a soma dos produtos dos valores correspondentes dos caracteres na interseção.
        var produtoEscalar = ProdutoEscalar(vetorEsquerda, vetorDireita, intersecao);
        // Passo 4: Calcular a magnitude quadrada de cada vetor
        // A magnitude é a raiz quadrada da soma dos quadrados dos valores no vetor.
        var mEsquerda = 0.0;
        foreach (var valor in vetorEsquerda.Values) {
            mEsquerda += valor * valor;
        }
        var mDireita = 0.0;
        foreach (var valor in vetorDireita.Values) {
            mDireita += valor * valor;
        }
        // Passo 5: Verificar se algum vetor é zero
        // Se algum vetor for zero (ou seja, todos os caracteres são únicos), a Similaridade de Cosseno é 0.
        if (mEsquerda <= 0 || mDireita <= 0) {
            return 0.0;
        }
        // Passo 6: Calcular e retornar a Similaridade de Cosseno
        // A Similaridade de Cosseno é o produto escalar dividido pelo produto das magnitudes.
        return produtoEscalar / (Math.Sqrt(mEsquerda) * Math.Sqrt(mDireita));
    }

    private static (Dictionary<char, int> VetorEsquerda, Dictionary<char, int> VetorDireita) ObterVetores(string esquerda, string direita) {
        var vetorEsquerda = new Dictionary<char, int>();
        var vetorDireita = new Dictionary<char, int>();

        // Calcula a frequência de cada caractere na string esquerda
        foreach (var caractere in esquerda) {
            vetorEsquerda.TryGetValue(caractere, out var frequencia);
            vetorEsquerda[caractere] = ++frequencia;
        }
        // Calcula a frequência de cada caractere na string direita
        foreach (var caractere in direita) {
            vetorDireita.TryGetValue(caractere, out var frequencia);
            vetorDireita[caractere] = ++frequencia;
        }
        return (vetorEsquerda, vetorDireita);
    }

    private static double ProdutoEscalar(Dictionary<char, int> vetorEsquerda, Dictionary<char, int> vetorDireita, HashSet<char> intersecao) {
        // Inicializa o produto escalar como 0
        double produtoEscalar = 0;

        // Itera sobre cada caractere na interseção dos dois vetores
        foreach (var caractere in intersecao) {
            // Calcula o produto dos valores correspondentes dos caracteres nos vetores esquerdo e direito
            produtoEscalar += vetorEsquerda[caractere] * vetorDireita[caractere];
        }
        // Retorna o produto escalar
        return produtoEscalar;
    }

    private static HashSet<char> ObterIntersecao(Dictionary<char, int> vetorEsquerda, Dictionary<char, int> vetorDireita) {
        // Inicializa um HashSet para armazenar a interseção dos dois vetores.
        var intersecao = new HashSet<char>();
        // Itera sobre cada par chave-valor no vetor esquerdo.
        foreach (var kvp in vetorEsquerda) {
            // Se o vetor direito contém a mesma chave, adiciona-a à interseção.
            if (vetorDireita.ContainsKey(kvp.Key)) {
                intersecao.Add(kvp.Key);
            }
        }
        return intersecao;
    }
}