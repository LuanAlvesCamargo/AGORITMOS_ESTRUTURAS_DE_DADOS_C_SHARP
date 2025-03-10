using System;
using System.Collections.Generic;

namespace EstruturasDeDados.ArvoreAVL;


// Uma árvore binária auto-balanceável simples.
// Uma árvore AVL é uma árvore de busca binária (BST) auto-balanceável nomeada
// em homenagem aos seus inventores: Adelson, Velsky e Landis. É a primeira BST
// auto-balanceável inventada. A propriedade principal de uma árvore AVL é que
// a altura das subárvores filhas para qualquer nó difere apenas por um.
// Devido à natureza balanceada da árvore, suas complexidades de tempo para
// inserção, exclusão e busca têm uma complexidade de tempo de pior caso de
// O(log n). Isso é uma melhoria em relação ao pior caso O(n) para uma BST regular.

public class ArvoreAVL<TChave> {
    // Obtém o número de nós na árvore.
    public int Contagem { get; private set; }

    // Comparador a ser usado ao comparar valores de chave.
    private readonly Comparer<TChave> comparador;

    // Referência ao nó raiz.
    private NoArvoreAVL<TChave>? raiz;

    public ArvoreAVL() {
        comparador = Comparer<TChave>.Default;
    }

    public ArvoreAVL(Comparer<TChave> comparadorPersonalizado) {
        comparador = comparadorPersonalizado;
    }

    // Adiciona um único nó à árvore.
    public void Adicionar(TChave chave) {
        if (raiz is null)
        {
            raiz = new NoArvoreAVL<TChave>(chave);
        } else {
            raiz = Adicionar(raiz, chave);
        }

        Contagem++;
    }

    // Adiciona vários nós à árvore.
    public void AdicionarVarios(IEnumerable<TChave> chaves) {
        foreach (var chave in chaves) {
            Adicionar(chave);
        }
    }

    // Remove um nó da árvore.
    public void Remover(TChave chave) {
        raiz = Remover(raiz, chave);
        Contagem--;
    }

    // Verifica se o nó fornecido está na árvore.
    public bool Contem(TChave chave) {
        var no = raiz;
        while (no is not null) {
            var resultadoComparacao = comparador.Compare(chave, no.Chave);
            if (resultadoComparacao < 0)
            {
                no = no.Esquerda;
            } else if (resultadoComparacao > 0) {
                no = no.Direita;
            } else {
                return true;
            }
        }
        return false;
    }

    // Obtém o valor mínimo na árvore.
    public TChave ObterMinimo() {
        if (raiz is null) {
            throw new InvalidOperationException("Árvore AVL está vazia.");
        }
        return ObterMinimo(raiz).Chave;
    }

    // Obtém o valor máximo na árvore.
    public TChave ObterMaximo() {
        if (raiz is null) {
            throw new InvalidOperationException("Árvore AVL está vazia.");
        }
        return ObterMaximo(raiz).Chave;
    }

    // Obtém as chaves em ordem do menor para o maior, conforme definido pelo comparador.
    public IEnumerable<TChave> ObterChavesEmOrdem() {
        List<TChave> resultado = new();
        PercorrerEmOrdem(raiz);
        return resultado;

        void PercorrerEmOrdem(NoArvoreAVL<TChave>? no) {
            if (no is null) {
                return;
            }
            PercorrerEmOrdem(no.Esquerda);
            resultado.Add(no.Chave);
            PercorrerEmOrdem(no.Direita);
        }
    }

    // Obtém as chaves em pré-ordem.
    public IEnumerable<TChave> ObterChavesPreOrdem() {
        var resultado = new List<TChave>();
        PercorrerPreOrdem(raiz);
        return resultado;

        void PercorrerPreOrdem(NoArvoreAVL<TChave>? no) {
            if (no is null) {
                return;
            }
            resultado.Add(no.Chave);
            PercorrerPreOrdem(no.Esquerda);
            PercorrerPreOrdem(no.Direita);
        }
    }

    // Obtém as chaves em pós-ordem. 
    public IEnumerable<TChave> ObterChavesPosOrdem() {
        var resultado = new List<TChave>();
        PercorrerPosOrdem(raiz);
        return resultado;

        void PercorrerPosOrdem(NoArvoreAVL<TChave>? no) {
            if (no is null) {
                return;
            }

            PercorrerPosOrdem(no.Esquerda);
            PercorrerPosOrdem(no.Direita);
            resultado.Add(no.Chave);
        }
    }
    // Função auxiliar para rebalancear a árvore para que todos os nós tenham um
    // fator de balanceamento no intervalo [-1, 1].

    private static NoArvoreAVL<TChave> Rebalancear(NoArvoreAVL<TChave> no) {
        if (no.FatorDeBalanceamento > 1) {
            if (no.Direita!.FatorDeBalanceamento == -1) {
                no.Direita = RotacionarDireita(no.Direita);
            }

            return RotacionarEsquerda(no);
        }

        if (no.FatorDeBalanceamento < -1) {
            if (no.Esquerda!.FatorDeBalanceamento == 1) {
                no.Esquerda = RotacionarEsquerda(no.Esquerda);
            }

            return RotacionarDireita(no);
        }
    }
}