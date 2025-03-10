using System;
using System.Collections.Generic;

namespace DataStructures.Stack;

public class PilhaBaseadaEmFila<T> {
    private readonly Queue<T> fila;
    public PilhaBaseadaEmFila() => fila = new Queue<T>();
    public void Limpar() => fila.Clear();
    public bool EstaVazia() => fila.Count == 0;
    public void Empilhar(T item) => fila.Enqueue(item);

    public T Desempilhar() {
        if (EstaVazia()) {
            throw new InvalidOperationException("A pilha não contém itens.");
        }

        for (int i = 0; i < fila.Count - 1; i++) {
            fila.Enqueue(fila.Dequeue());
        }
        return fila.Dequeue();
    }

    public T Espiar() {
        if (EstaVazia()) {
            throw new InvalidOperationException("A pilha não contém itens.");
        }

        for (int i = 0; i < fila.Count - 1; i++) {
            fila.Enqueue(fila.Dequeue());
        }

        var item = fila.Peek();
        fila.Enqueue(fila.Dequeue());
        return item;
    }

    public int Comprimento() => fila.Count;
}

