using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Heap.FibonacciHeap;

public class HeapFibonacci<T> where T : IComparable {
    public int Contagem { get; set; }
    private NoHeapFibonacci<T>? ItemMinimo { get; set; }
    public NoHeapFibonacci<T> Inserir(T x) {
        Contagem++;
        var novoItem = new NoHeapFibonacci<T>(x);
        if (ItemMinimo == null) {
            ItemMinimo = novoItem;
        } else { 
            ItemMinimo.AdicionarDireita(novoItem);
            if (novoItem.Chave.CompareTo(ItemMinimo.Chave) < 0) {
                ItemMinimo = novoItem;
            }
        }
        return novoItem;
    }

    public void Unir(HeapFibonacci<T> outro) {
        if (outro.ItemMinimo == null) {
            return;
        }
        if (ItemMinimo == null) {
            ItemMinimo = outro.ItemMinimo;
            Contagem = outro.Contagem;
            outro.ItemMinimo = null;
            outro.Contagem = 0;
            return;
        }
        Contagem += outro.Contagem;
        ItemMinimo.ConcatenarDireita(outro.ItemMinimo);
        if (outro.ItemMinimo.Chave.CompareTo(ItemMinimo.Chave) < 0) {
            ItemMinimo = outro.ItemMinimo;
        }
        outro.ItemMinimo = null;
        outro.Contagem = 0;
    }

    public T RemoverMinimo() {
        NoHeapFibonacci<T>? z = null;
        if (ItemMinimo == null) {
            throw new InvalidOperationException("Heap está vazio!");
        }
        z = ItemMinimo;
        if (z.Filho != null) {
            foreach (var x in IteradorIrmaos(z.Filho)) {
                x.Pai = null;
            }
            z.ConcatenarDireita(z.Filho);
        }

        if (Contagem == 1) {
            ItemMinimo = null;
            Contagem = 0;
            return z.Chave;
        }

        ItemMinimo = ItemMinimo.Direita;
        z.Remover();
        Consolidar();
        Contagem -= 1;
        return z.Chave;
    }

    public T ObterMinimo() {
        if (ItemMinimo == null) {
            throw new InvalidOperationException("O heap está vazio");
        }
        return ItemMinimo.Chave;
    }

    private void Consolidar() {
        if (ItemMinimo == null) return;
        var a = new List<NoHeapFibonacci<T>>();
        var start = ItemMinimo;
        var current = ItemMinimo;
        do { 
            a.Add(current);
            current = current.Direita;
        } while (current != start);
        ItemMinimo = null;
        foreach(var x in a) {
            var currentX = x;
            while(true) {
                while(a.Count <= currentX.Grau) {
                    a.Add(null);
                }
                if(a[currentX.Grau] == null) {
                    a[currentX.Grau] = currentX;
                    break;
                }
                var y = a[currentX.Grau];
                a[currentX.Grau] = null;
                if(currentX.Chave.CompareTo(y.Chave) > 0) {
                    var temp = currentX;
                    currentX = y;
                    y = temp;
                }
                y.Remover();
                currentX.AdicionarFilho(y);
            } if(ItemMinimo == null) {
                ItemMinimo = currentX;
            } else {
                ItemMinimo.AdicionarDireita(currentX);
                if(currentX.Chave.CompareTo(ItemMinimo.Chave) < 0) {
                    ItemMinimo = currentX;
                }
            }
        }
    }

    private IEnumerable<NoHeapFibonacci<T>> IteradorIrmaos(NoHeapFibonacci<T> no) {
        var atual = no;
        do {
            yield return atual;
            atual = atual.Direita;
        } while (atual != no);
    }
}

public class NoHeapFibonacci<T> where T : IComparable {
    public NoHeapFibonacci(T chave) {
        Chave = chave;
    }

    public T Chave { get; set; }
    public NoHeapFibonacci<T>? Pai { get; set; }
    public NoHeapFibonacci<T>? Filho { get; set; }
    public NoHeapFibonacci<T> Direita { get; set; }
    public NoHeapFibonacci<T> Esquerda { get; set; }
    public int Grau { get; set; }
    public bool Marcado { get; set; }

    public void AdicionarDireita(NoHeapFibonacci<T> no) {
        if (Direita == null) {
            Direita = no;
            Esquerda = no;
            no.Direita = this;
            no.Esquerda = this;
        } else {
            no.Direita = Direita;
            no.Esquerda = this;
            Direita.Esquerda = no;
            Direita = no;
        }
    }

    public void ConcatenarDireita(NoHeapFibonacci<T> no) {
        if (no == null) return;
        if(Direita == null) {
            Direita = no;
            Esquerda = no.Esquerda;
            no.Esquerda.Direita = this;
            no.Esquerda = this;
            return;
        }
        var temp = Direita;
        Direita = no;
        Esquerda.Direita = no.Esquerda;
        no.Esquerda.Direita = this;
        Esquerda = no.Esquerda;
        temp.Esquerda = no;
    }

    public void Remover() {
        Esquerda.Direita = Direita;
        Direita.Esquerda = Esquerda;
    }

    public void AdicionarFilho(NoHeapFibonacci<T> no) {
        no.Pai = this;
        if(Filho == null) {
            Filho = no;
            no.Direita = no;
            no.Esquerda = no;
        } else {
            Filho.AdicionarDireita(no);
        }
        Grau++;
    }
  
    public void DiminuirChave(FHeapNode<T> x, T k) {
        if (ItemMinimo == null) {
            throw new ArgumentException($"{nameof(x)} não pertence à fila");
        }
        if (x.Chave == null) {
            throw new ArgumentException("x não tem valor");
        }
        if (k.CompareTo(x.Chave) > 0) {
            throw new InvalidOperationException("Valor não pode ser aumentado");
        }
        x.Chave = k;
        var y = x.Pai;
        if (y != null && x.Chave.CompareTo(y.Chave) < 0) {
            Cortar(x, y);
            CorteCascata(y);
        }
        if (x.Chave.CompareTo(ItemMinimo.Chave) < 0) {
            ItemMinimo = x;
        }
    }

    protected void Cortar(FHeapNode<T> x, FHeapNode<T> y) {
        if (ItemMinimo == null) {
            throw new InvalidOperationException("Fila malformada");
        }
        if (y.Grau == 1) {
            y.Filho = null;
            ItemMinimo.AdicionarDireita(x);
        } else if (y.Grau > 1) {
            x.Remover();
        } else {
            throw new InvalidOperationException("Fila malformada");
        }
        y.Grau--;
        x.Marcado = false;
        x.Pai = null;
    }

    protected void CorteCascata(FHeapNode<T> y) {
        var z = y.Pai;
        if (z != null) {
            if (!y.Marcado) {
                y.Marcado = true;
            } else {
                Cortar(y, z);
                CorteCascata(z);
            }
        }
    }

    protected void Consolidar() {
        if (ItemMinimo == null) {
            return;
        }
        var grauMaximo = 1 + (int)Math.Log(Contagem, (1 + Math.Sqrt(5)) / 2);
        var a = new FHeapNode<T>?[grauMaximo];
        var irmaos = IteradorIrmaos(ItemMinimo).ToList();
        foreach (var w in irmaos) {
            var x = w;
            var d = x.Grau;
            var y = a[d]; 
            while (y != null) {
                if (x.Chave.CompareTo(y.Chave) > 0) {
                    var temp = x;
                    x = y;
                    y = temp;
                }
                LigacaoFilaFibonacci(y, x);
                a[d] = null;
                d++;
                y = a[d];
            }
            a[d] = x;
        }
        ReconstruirFila(a);
    }

    private void ReconstruirFila(FHeapNode<T>?[] a) {
        ItemMinimo = null;
        for (var i = 0; i < a.Length; i++) {
            var r = a[i];
            if (r == null) {
                continue;
            }
            if (ItemMinimo == null) {
                ItemMinimo = r;
                ItemMinimo.DefinirIrmaos(ItemMinimo, ItemMinimo);
                ItemMinimo.Pai = null;
            } else  {
                ItemMinimo.AdicionarDireita(r);
                if (ItemMinimo.Chave.CompareTo(r.Chave) > 0) {
                    ItemMinimo = a[i];
                }
            }
        }
    }

    private void LigacaoFilaFibonacci(FHeapNode<T> y, FHeapNode<T> x) {
        y.Remover();
        x.AdicionarFilho(y);
        y.Marcado = false;
    }

    private IEnumerable<FHeapNode<T>> IteradorIrmaos(FHeapNode<T> no) {
        var noAtual = no;
        yield return noAtual;
        noAtual = no.Direita;
        while (noAtual != no) {
            yield return noAtual;
            noAtual = noAtual.Direita;
        }
    }
}
