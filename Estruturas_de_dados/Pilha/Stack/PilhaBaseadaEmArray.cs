using System;
namespace DataStructures.Stack;

public class PilhaBaseadaEmArray<T> {
    private const int CapacidadePadrao = 10;
    private const string MensagemErroPilhaVazia = "Pilha está vazia";
    private T[] pilha;
    private int topo;

    public PilhaBaseadaEmArray() {
        pilha = new T[CapacidadePadrao];
        topo = -1;
    }

    public PilhaBaseadaEmArray(T item)
        : this() => Empilhar(item);

    public PilhaBaseadaEmArray(T[] itens) {
        pilha = itens;
        topo = itens.Length - 1;
    }
    public int Topo => topo;

    public int Capacidade {
        get => pilha.Length;
        set => Array.Resize(ref pilha, value);
    }

    public void Limpar() {
        topo = -1;
        Capacidade = CapacidadePadrao;
    }

    public bool Contem(T item) => Array.IndexOf(pilha, item, 0, topo + 1) > -1;

    public T Espiar() {
        if (topo == -1) {
            throw new InvalidOperationException(MensagemErroPilhaVazia);
        }
        return pilha[topo];
    }

    public T Desempilhar() {
        if (topo == -1) {
            throw new InvalidOperationException(MensagemErroPilhaVazia);
        }
        return pilha[topo--];
    }

    public void Empilhar(T item) {
        if (topo == Capacidade - 1) {
            Capacidade *= 2;
        }
        pilha[++topo] = item;
    }
}