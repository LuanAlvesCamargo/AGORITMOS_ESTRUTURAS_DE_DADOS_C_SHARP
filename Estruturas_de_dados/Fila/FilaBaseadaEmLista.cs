using System;
using System.Collections.Generic;
using System.Linq;

namespace EstruturasDeDados.Fila;

// Implementação de uma fila baseada em lista. Estilo FIFO (Primeiro a Entrar, Primeiro a Sair).
public class FilaBaseadaEmLista<T> { 
    private readonly LinkedList<T> fila;
    public FilaBaseadaEmLista() => fila = new LinkedList<T>();
    public void Limpar() {
        fila.Clear();
    }
    public T Desenfileirar() {
        if (fila.First is null) {
            throw new InvalidOperationException("Não há itens na fila.");
        }
        var item = fila.First;
        fila.RemoveFirst();
        return item.Value;
    }

    public bool EstaVazia() => !fila.Any();
    public bool EstaCheia() => false;
    public T Espiar() {
        if (fila.First is null) {
            throw new InvalidOperationException("Não há itens na fila.");
        
        return fila.First.Value;
    }
    public void Enfileirar(T item) {
        fila.AddLast(item);
    }
}
}