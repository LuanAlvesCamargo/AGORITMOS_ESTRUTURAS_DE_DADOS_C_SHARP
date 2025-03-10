using System;
using System.Collections.Generic;

namespace DataStructures.Stack;

public class PilhaBaseadaEmLista<T> {
    private readonly LinkedList<T> pilha;
    public PilhaBaseadaEmLista() => pilha = new LinkedList<T>();
    public PilhaBaseadaEmLista(T item)
        : this() => Empilhar(item);
    public PilhaBaseadaEmLista(IEnumerable<T> items)
        : this() {
        foreach (var item in items) {
            Empilhar(item);
        }
    }

    public int Contagem => pilha.Count;
    public void Limpar() => pilha.Clear();
    public bool Contem(T item) => pilha.Contains(item);

    public T Espiar() {
        if (pilha.First is null) {
            throw new InvalidOperationException("Pilha está vazia");
        }
        return pilha.First.Value;
    }

    public T Desempilhar() {
        if (pilha.First is null) {
            throw new InvalidOperationException("Pilha está vazia");
        }
        var item = pilha.First.Value;
        pilha.RemoveFirst();
        return item;
    }

    public void Empilhar(T item) => pilha.AddFirst(item);
}
