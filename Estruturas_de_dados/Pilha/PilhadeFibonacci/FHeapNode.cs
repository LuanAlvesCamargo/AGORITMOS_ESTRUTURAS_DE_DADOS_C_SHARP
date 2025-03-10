using System;
namespace EstruturasDeDados.Heap.FibonacciHeap;

public class NoHeapFibonacci<T> where T : IComparable {
    public NoHeapFibonacci(T chave) {
        Chave = chave;
        Esquerda = Direita = this;
        Pai = Filho = null;
    }

    public T Chave { get; set; }
    public NoHeapFibonacci<T>? Pai { get; set; }
    public NoHeapFibonacci<T> Esquerda { get; set; }
    public NoHeapFibonacci<T> Direita { get; set; }
    public NoHeapFibonacci<T>? Filho { get; set; }
    public bool Marcado { get; set; }
    public int Grau { get; set; }

    public void DefinirIrmaos(NoHeapFibonacci<T> esquerda, NoHeapFibonacci<T> direita) {
        Esquerda = esquerda;
        Direita = direita;
    }

    public void AdicionarDireita(NoHeapFibonacci<T> no) {
        Direita.Esquerda = no;
        no.Direita = Direita;
        no.Esquerda = this;
        Direita = no;
    }

    public void AdicionarFilho(NoHeapFibonacci<T> no) {
        Grau++;

        if (Filho == null) {
            Filho = no;
            Filho.Pai = this;
            Filho.Esquerda = Filho.Direita = Filho;
            return;
        }
        Filho.AdicionarDireita(no);
    }

    public void Remover() {
        Esquerda.Direita = Direita;
        Direita.Esquerda = Esquerda;
    }

    public void ConcatenarDireita(NoHeapFibonacci<T> outraLista) {
        Direita.Esquerda = outraLista.Esquerda;
        outraLista.Esquerda.Direita = Direita;

        Direita = outraLista;
        outraLista.Esquerda = this;
    }
}