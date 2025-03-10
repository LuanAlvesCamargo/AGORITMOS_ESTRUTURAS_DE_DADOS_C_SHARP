using System;

namespace EstruturasDeDados.Fila;
// Implementação de uma fila baseada em array. Estilo FIFO (First-In, First-Out).

public class FilaBaseadaEmArray<T> {
    private readonly T[] fila;
    private int indiceFinal;
    private bool estaVazia;
    private bool estaCheia;
    private int indiceInicial;
    public FilaBaseadaEmArray(int capacidade) {
        fila = new T[capacidade];
        Limpar();
    }
    public void Limpar() {
        indiceInicial = 0;
        indiceFinal = 0;
        estaVazia = true;
        estaCheia = false;
    }
    public T Desenfileirar() {
        if (EstaVazia()) {
            throw new InvalidOperationException("Não há itens na fila.");
        }
        var indiceDesenfileiramento = indiceFinal;
        indiceFinal++;
        if (indiceFinal >= fila.Length) {
            indiceFinal = 0;
        }
        estaCheia = false;
        estaVazia = indiceInicial == indiceFinal;
        return fila[indiceDesenfileiramento];
    }
    public bool EstaVazia() => estaVazia;
    public bool EstaCheia() => estaCheia;
    public T Espiar() {
        if (EstaVazia()) {
            throw new InvalidOperationException("Não há itens na fila.");
        }
        return fila[indiceFinal];
    }
    public void Enfileirar(T item) {
        if (EstaCheia()) {
            throw new InvalidOperationException("A fila atingiu sua capacidade máxima.");
        }
        fila[indiceInicial] = item;
        indiceInicial++;
        if (indiceInicial >= fila.Length) {
            indiceInicial = 0;
        }
        estaVazia = false;
        estaCheia = indiceInicial == indiceFinal;
    }
}