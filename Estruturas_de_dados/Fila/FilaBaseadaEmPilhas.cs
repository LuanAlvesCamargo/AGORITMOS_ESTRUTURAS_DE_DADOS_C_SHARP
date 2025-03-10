using System;
using System.Collections.Generic;

namespace EstruturasDeDados.Fila;

// Implementação de uma fila baseada em pilhas. Estilo FIFO (First-In, First-Out).
// Enfileirar é O(1) e Desenfileirar é O(1) amortizado. 

public class FilaBaseadaEmPilhas<T> {
    private readonly Stack<T> entrada;
    private readonly Stack<T> saida;
    public FilaBaseadaEmPilhas(){
        entrada = new Stack<T>();
        saida = new Stack<T>();
    }
    public void Limpar() {
        entrada.Clear();
        saida.Clear();
    }
    public T Desenfileirar() {
        if (entrada.Count == 0 && saida.Count == 0) {
            throw new InvalidOperationException("A fila não contém itens.");
        }

        if (saida.Count == 0) {
            while (entrada.Count > 0) {
                var item = entrada.Pop();
                saida.Push(item);
            }
        }
        return saida.Pop();
    }
    public bool EstaVazia() => entrada.Count == 0 && saida.Count == 0;
    public bool EstaCheia() => false;
    public T Espiar() {
        if (entrada.Count == 0 && saida.Count == 0) {
            throw new InvalidOperationException("A fila não contém itens.");
        }

        if (saida.Count == 0) {
            while (entrada.Count > 0) {
                var item = entrada.Pop();
                saida.Push(item);
            }
        }
        return saida.Peek();
    }
    public void Enfileirar(T item) => entrada.Push(item);
}