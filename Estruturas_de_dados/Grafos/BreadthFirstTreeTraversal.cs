using System;
using System.Collections.Generic;
using DataStructures.BinarySearchTree;

namespace Algoritmos.Grafos;

    // A travessia em largura (Breadth-First) percorre uma árvore binária 
    // iterando por cada nível primeiro.
    // Complexidade de tempo: O(n).
    // Complexidade de espaço: O(w), onde w é a largura máxima da árvore binária.
public static class TravessiaLarguraArvore<TChave> {
    // Travessia em Nível retorna um array de chaves na ordem
    // de cada nível de uma árvore binária. Utiliza uma fila para iterar
    // por cada nó, seguindo a travessia em largura.

    public static TChave[] TravessiaEmNivel(ArvoreBuscaBinaria<TChave> arvore) {
        NoArvoreBuscaBinaria<TChave>? raiz = arvore.Raiz;
        TChave[] ordemNivel = new TChave[arvore.Contagem];
        if (raiz is null) {
            return Array.Empty<TChave>();
        }

        Queue<NoArvoreBuscaBinaria<TChave>> travessiaLargura = new Queue<NoArvoreBuscaBinaria<TChave>>();
        travessiaLargura.Enqueue(raiz);
        for (int i = 0; i < ordemNivel.Length; i++) {
            NoArvoreBuscaBinaria<TChave> atual = travessiaLargura.Dequeue();
            ordemNivel[i] = atual.Chave;
            if (atual.Esquerda is not null) {
                travessiaLargura.Enqueue(atual.Esquerda);
            }

            if (atual.Direita is not null){
                travessiaLargura.Enqueue(atual.Direita);
            }
        }
        return ordemNivel;
    }

    // Nó Mais Profundo retorna o nó mais profundo em uma árvore binária. Se mais
    // de um nó estiver no nível mais profundo, ele é definido como o
    // nó mais à direita da árvore binária. Nó mais profundo utiliza a travessia
    // em largura para alcançar o final.

    public static TChave? NoMaisProfundo(ArvoreBuscaBinaria<TChave> arvore) {
        NoArvoreBuscaBinaria<TChave>? raiz = arvore.Raiz;
        if (raiz is null) {
            return default(TChave);
        }

        Queue<NoArvoreBuscaBinaria<TChave>> travessiaLargura = new Queue<NoArvoreBuscaBinaria<TChave>>();
        travessiaLargura.Enqueue(raiz);
        TChave maisProfundo = raiz.Chave;
        while (travessiaLargura.Count > 0) {
            NoArvoreBuscaBinaria<TChave> atual = travessiaLargura.Dequeue();
            if (atual.Esquerda is not null) {
                travessiaLargura.Enqueue(atual.Esquerda);
            }

            if (atual.Direita is not null) {
                travessiaLargura.Enqueue(atual.Direita);
            }
            maisProfundo = atual.Chave;
        }
        return maisProfundo;
    }
}