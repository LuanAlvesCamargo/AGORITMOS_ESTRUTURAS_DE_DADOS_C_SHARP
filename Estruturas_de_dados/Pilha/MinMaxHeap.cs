using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Heap;

public class HeapMinMax<T> {
    private readonly List<T> heap;
    public HeapMinMax(IEnumerable<T>? colecao = null, IComparer<T>? comparador = null) {
        Comparador = comparador ?? Comparer<T>.Default;
        colecao ??= Enumerable.Empty<T>();

        heap = colecao.ToList();
        for (var i = Contagem / 2 - 1; i >= 0; --i) {
            Descer(i);
        }
    }
    public IComparer<T> Comparador { get; }
    public int Contagem => heap.Count;
    public void Adicionar(T item) {
        heap.Add(item);
        Subir(Contagem - 1);
    }
    public T ExtrairMaximo() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazio");
        }
        var maximo = ObterMaximo();
        RemoverNo(ObterIndiceNoMaximo());
        return maximo;
    }
    public T ExtrairMinimo() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazio");
        }
        var minimo = ObterMinimo();
        RemoverNo(0);
        return minimo;
    }

    public T ObterMaximo() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazio");
        }
        return heap[ObterIndiceNoMaximo()];
    }
    public T ObterMinimo() {
        if (Contagem == 0) {
            throw new InvalidOperationException("Heap está vazio");
        }
        return heap[0];
    }

private int IndiceDoMaiorFilhoOuNeto(int indice) {
        var descendentes = new[] {
            2 * indice + 1,
            2 * indice + 2,
            4 * indice + 3,
            4 * indice + 4,
            4 * indice + 5,
            4 * indice + 6,
        };
        var indiceResultado = descendentes[0];
        foreach (var descendente in descendentes) {
            if (descendente >= Contagem) {
                break;
            } 
            if (Comparador.Compare(heap[descendente], heap[indiceResultado]) > 0) {
                indiceResultado = descendente;
            }
        }
        return indiceResultado;
    }
    private int IndiceDoMenorFilhoOuNeto(int indice) {
        var descendentes = new[] { 2 * indice + 1, 2 * indice + 2, 4 * indice + 3, 4 * indice + 4, 4 * indice + 5, 4 * indice + 6 };
        var indiceResultado = descendentes[0];
        foreach (var descendente in descendentes) {
            if (descendente >= Contagem) {
                break;
            }
            if (Comparador.Compare(heap[descendente], heap[indiceResultado]) < 0) {
                indiceResultado = descendente;
            }
        }
        return indiceResultado;
    }
    private int ObterIndiceNoMaximo() {
        return Contagem switch {
            0 => throw new InvalidOperationException("Heap está vazio"),
            1 => 0,
            2 => 1,
            _ => Comparador.Compare(heap[1], heap[2]) > 0 ? 1 : 2,
        };
    }
    private bool TemFilho(int indice) => indice * 2 + 1 < Contagem;
    private bool EhNeto(int no, int neto) => neto > 2 && Avô(neto) == no;
    private bool EhIndiceNivelMinimo(int indice) {
        const uint bitsNiveisMinimos = 0x55555555;
        const uint bitsNiveisMaximos = 0xAAAAAAAA;
        return ((indice + 1) & bitsNiveisMinimos) > ((indice + 1) & bitsNiveisMaximos);
    }
    private int Pai(int indice) => (indice - 1) / 2;
    private int Avô(int indice) => ((indice - 1) / 2 - 1) / 2;
    private void Descer(int indice) {
        if (EhIndiceNivelMinimo(indice)) {
            DescerMinimo(indice);
        } else {
            DescerMaximo(indice);
        }
    }
    private void DescerMaximo(int indice) {
        if (!TemFilho(indice)) {
            return;
        }
        var indiceMaximo = IndiceDoMaiorFilhoOuNeto(indice);
        if (EhNeto(indice, indiceMaximo)) {
            if (Comparador.Compare(heap[indiceMaximo], heap[indice]) > 0) {
                TrocarNos(indiceMaximo, indice);
                if (Comparador.Compare(heap[indiceMaximo], heap[Pai(indiceMaximo)]) < 0) {
                    TrocarNos(indiceMaximo, Pai(indiceMaximo));
                }
                DescerMaximo(indiceMaximo);
            }
        } else {
            if (Comparador.Compare(heap[indiceMaximo], heap[indice]) > 0) {
                TrocarNos(indiceMaximo, indice);
            }
        }
    }
    private void DescerMinimo(int indice) {
        if (!TemFilho(indice)) {
            return;
        }
        var indiceMinimo = IndiceDoMenorFilhoOuNeto(indice); {
            if (Comparador.Compare(heap[indiceMinimo], heap[indice]) < 0) {
                TrocarNos(indiceMinimo, indice);
                if (Comparador.Compare(heap[indiceMinimo], heap[Pai(indiceMinimo)]) > 0) {
                    TrocarNos(indiceMinimo, Pai(indiceMinimo));
                }
                DescerMinimo(indiceMinimo);
            }
        } else {
            if (Comparador.Compare(heap[indiceMinimo], heap[indice]) < 0) {
                TrocarNos(indiceMinimo, indice);
            }
        }
    }
    private void Subir(int indice) {
        if (indice == 0) {
            return;
        }

        var pai = Pai(indice);

        if (EhIndiceNivelMinimo(indice)) {
            if (Comparador.Compare(heap[indice], heap[pai]) > 0) {
                TrocarNos(indice, pai);
                SubirMaximo(pai);
            } else {
                SubirMinimo(indice);
            } 
        } else {
            if (Comparador.Compare(heap[indice], heap[pai]) < 0) {
                TrocarNos(indice, pai);
                SubirMinimo(pai);
            } else {
                SubirMaximo(indice);
            }
        }
    }
    private void SubirMaximo(int indice) {
        if (indice > 2) {
            var avo = Avô(indice);
            if (Comparador.Compare(heap[indice], heap[avo]) > 0) {
                TrocarNos(indice, avo);
                SubirMaximo(avo);
            }
        }
    }
    private void SubirMinimo(int indice) {
        if (indice > 2) {
            var avo = Avô(indice);
            if (Comparador.Compare(heap[indice], heap[avo]) < 0) {
                TrocarNos(indice, avo);
                SubirMinimo(avo);
            }
        }
    }
    private void RemoverNo(int indice) {
        TrocarNos(indice, Contagem - 1);
        heap.RemoveAt(Contagem - 1);
        if (Contagem != 0) {
            Descer(indice);
        }
    }
    private void TrocarNos(int i, int j) {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
