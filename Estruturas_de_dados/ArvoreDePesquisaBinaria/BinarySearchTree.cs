using System;
using System.Collections.Generic;

namespace EstruturasDeDados.ArvoreDeBuscaBinaria;

// Uma árvore ordenada com inserção, remoção e busca eficientes. Uma Árvore de Busca Binária (ABB) é uma árvore que satisfaz as seguintes propriedades:

public class ArvoreDeBuscaBinaria<TChave> {
    private readonly Comparador<TChave> comparador;
    public NoArvoreDeBuscaBinaria<TChave>? Raiz { get; private set; }

    public ArvoreDeBuscaBinaria() {
        Raiz = null;
        Contagem = 0;
        comparador = Comparador<TChave>.Padrao;
    }

    public ArvoreDeBuscaBinaria(Comparador<TChave> comparadorPersonalizado) {
        Raiz = null;
        Contagem = 0;
        comparador = comparadorPersonalizado;
    }

    public int Contagem { get; private set; }

    public void Adicionar(TChave chave) {
        if (Raiz is null) {
            Raiz = new NoArvoreDeBuscaBinaria<TChave>(chave);
        } else {
            Adicionar(Raiz, chave);
        }

        Contagem++;
    }
    public void AdicionarVarias(IEnumerable<TChave> chaves) {
        foreach (var chave in chaves) {
            Adicionar(chave);
        }
    }

    public NoArvoreDeBuscaBinaria<TChave>? Buscar(TChave chave) => Buscar(Raiz, chave);
    public bool Contem(TChave chave) => Buscar(Raiz, chave) is not null;
    public bool Remover(TChave chave) {
        if (Raiz is null) {
            return false;
        }

        var resultado = Remover(Raiz, Raiz, chave);
        if (resultado) {
            Contagem--;
        }

        return resultado;
    }

    public NoArvoreDeBuscaBinaria<TChave>? ObterMinimo() {
        if (Raiz is null) {
            return default;
        }

        return ObterMinimo(Raiz);
    }

    public NoArvoreDeBuscaBinaria<TChave>? ObterMaximo() {
        if (Raiz is null) {
            return default;
        }

        return ObterMaximo(Raiz);
    }

    public ICollection<TChave> ObterChavesEmOrdem() => ObterChavesEmOrdem(Raiz);
    public ICollection<TChave> ObterChavesPreOrdem() => ObterChavesPreOrdem(Raiz);
    public ICollection<TChave> ObterChavesPosOrdem() => ObterChavesPosOrdem(Raiz);
    private void Adicionar(NoArvoreDeBuscaBinaria<TChave> no, TChave chave) {
        var resultadoComparacao = comparador.Comparar(no.Chave, chave);
        if (resultadoComparacao > 0) {
            if (no.Esquerda is not null)
            {
                Adicionar(no.Esquerda, chave);
            } else {
                var novoNo = new NoArvoreDeBuscaBinaria<TChave>(chave);
                no.Esquerda = novoNo;
            }
        } else if (resultadoComparacao < 0) {
            if (no.Direita is not null)
            {
                Adicionar(no.Direita, chave);
            } else {
                var novoNo = new NoArvoreDeBuscaBinaria<TChave>(chave);
                no.Direita = novoNo;
            }
        } else {
            throw new ArgumentException($"A chave \"{chave}\" já existe na árvore!");
        }
    }
}