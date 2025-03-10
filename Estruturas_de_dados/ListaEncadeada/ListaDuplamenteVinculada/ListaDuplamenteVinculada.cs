using System;
using System.Collections.Generic;
using Utilities.Exceptions;

namespace DataStructures.LinkedList.DoublyLinkedList;

public class ListaDuplamenteLigada<T> {
    public ListaDuplamenteLigada(T dados) {
        Cabeca = new NoDuplamenteLigado<T>(dados);
        Cauda = Cabeca;
        Contagem = 1;
    }

    public ListaDuplamenteLigada(IEnumerable<T> dados) {
        foreach (var d in dados) {
            Adicionar(d);
        }
    }

    public int Contagem { get; private set; }
    private NoDuplamenteLigado<T>? Cabeca { get; set; }
    private NoDuplamenteLigado<T>? Cauda { get; set; }
    public NoDuplamenteLigado<T> AdicionarCabeca(T dados) {
        var no = new NoDuplamenteLigado<T>(dados);
        if (Cabeca is null) {
            Cabeca = no;
            Cauda = no;
            Contagem = 1;
            return no;
        }
        Cabeca.Anterior = no;
        no.Proximo = Cabeca;
        Cabeca = no;
        Contagem++;
        return no;
    }

    public NoDuplamenteLigado<T> Adicionar(T dados) {
        if (Cabeca is null) {
            return AdicionarCabeca(dados);
        }
        var no = new NoDuplamenteLigado<T>(dados);
        Cauda!.Proximo = no;
        no.Anterior = Cauda;
        Cauda = no;
        Contagem++;
        return no;
    }

    public NoDuplamenteLigado<T> AdicionarApos(T dados, NoDuplamenteLigado<T> noExistente) {
        if (noExistente == Cauda) {
            return Adicionar(dados);
        }
        var no = new NoDuplamenteLigado<T>(dados);
        no.Proximo = noExistente.Proximo;
        no.Anterior = noExistente;
        noExistente.Proximo = no;
        if (no.Proximo is not null) {
            no.Proximo.Anterior = no;
        }
        Contagem++;
        return no;
    }

    public IEnumerable<T> ObterDados() {
        var atual = Cabeca;
        while (atual is not null) {
            yield return atual.Dados;
            atual = atual.Proximo;
        }
    }

    public IEnumerable<T> ObterDadosInvertidos() {
        var atual = Cauda;
        while (atual is not null) {
            yield return atual.Dados;
            atual = atual.Anterior;
        }
    }

    public void Inverter() {
        var atual = Cabeca;
        NoDuplamenteLigado<T>? temp = null;
        while (atual is not null) {
            temp = atual.Anterior;
            atual.Anterior = atual.Proximo;
            atual.Proximo = temp;
            atual = atual.Anterior;
        }
        Cauda = Cabeca;
        if (temp is not null) {
            Cabeca = temp.Anterior;
        }
    }

    public NoDuplamenteLigado<T> Encontrar(T dados) {
        var atual = Cabeca;
        while (atual is not null) {
            if (atual.Dados is null && dados is null || atual.Dados is not null && atual.Dados.Equals(dados)) {
                return atual;
            }
            atual = atual.Proximo;
        }
        throw new ItemNotFoundException();
    }
    
    public NoDuplamenteLigado<T> ObterEm(int posicao) {
        if (posicao < 0 || posicao >= Contagem) {
            throw new ArgumentOutOfRangeException($"Contagem máxima é {Contagem}");
        }
        var atual = Cabeca;
        for (var i = 0; i < posicao; i++) {
            atual = atual!.Proximo;
        }
        return atual ?? throw new ArgumentOutOfRangeException($"{nameof(posicao)} deve ser um índice na lista");
    }

    public void RemoverCabeca() {
        if (Cabeca is null) {
            throw new InvalidOperationException();
        }
        Cabeca = Cabeca.Proximo;
        if (Cabeca is null) {
            Cauda = null;
            Contagem = 0;
            return;
        }
        Cabeca.Anterior = null;
        Contagem--;
    }

    public void Remover() {
        if (Cauda is null) {
            throw new InvalidOperationException("Não é possível remover de uma lista vazia");
        }
        Cauda = Cauda.Anterior;
        if (Cauda is null) {
            Cabeca = null;
            Contagem = 0;
            return;
        }
        Cauda.Proximo = null;
        Contagem--;
    }

    public void RemoverNo(NoDuplamenteLigado<T> no) {
        if (no == Cabeca) {
            RemoverCabeca();
            return;
        }

        if (no == Cauda) {
            Remover();
            return;
        }

        if (no.Anterior is null || no.Proximo is null) {
            throw new ArgumentException(
                $"{nameof(no)} não pode ter Anterior ou Próximo nulo se for um nó interno");
        }
        no.Anterior.Proximo = no.Proximo;
        no.Proximo.Anterior = no.Anterior;
        Contagem--;
    }

    public void Remover(T dados) {
        var no = Encontrar(dados);
        RemoverNo(no);
    }

    public int IndiceDe(T dados) {
        var atual = Cabeca;
        var indice = 0;
        while (atual is not null) {
            if (atual.Dados is null && dados is null || atual.Dados is not null && atual.Dados.Equals(dados)) {
                return indice;
            }
            indice++;
            atual = atual.Proximo;
        }
        return -1;
    }
    public bool Contem(T dados) => IndiceDe(dados) != -1;
}

public class NoDuplamenteLigado<T> {
    public NoDuplamenteLigado(T dados) {
        Dados = dados;
    }
    public T Dados { get; set; }
    public NoDuplamenteLigado<T>? Proximo { get; set; }
    public NoDuplamenteLigado<T>? Anterior { get; set; }
}