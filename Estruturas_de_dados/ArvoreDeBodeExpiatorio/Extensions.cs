using System;
using System.Collections.Generic;

namespace EstruturasDeDados.ArvoreBodeExpiatorio;

public static class Extensoes {
    // Transforma uma árvore bode expiatório em uma lista de nós.
    // Árvore bode expiatório fornecida como nó raiz. Uma lista vazia. Tipo da chave do nó da árvore bode expiatório.
    public static void AchatamentoArvore<TChave>(No<TChave> raiz, List<No<TChave>> lista) where TChave : IComparable {
        if (raiz.Esquerda != null) {
            AchatamentoArvore(raiz.Esquerda, lista);
        }

        lista.Add(raiz);

        if (raiz.Direita != null) {
            AchatamentoArvore(raiz.Direita, lista);
        }
    }

    public static No<TChave> ReconstruirDaLista<TChave>(IList<No<TChave>> lista, int inicio, int fim) 
        where TChave : IComparable {
        if (inicio > fim) {
            throw new ArgumentException("O valor do parâmetro é inválido.", nameof(inicio));
        }

        var pivo = Convert.ToInt32(Math.Ceiling(inicio + (fim - inicio) / 2.0));

        return new No<TChave>(lista[pivo].Chave) {
            Esquerda = inicio > (pivo - 1) ? null : ReconstruirDaLista(lista, inicio, pivo - 1),
            Direita = (pivo + 1) > fim ? null : ReconstruirDaLista(lista, pivo + 1, fim),
        };
    }
}