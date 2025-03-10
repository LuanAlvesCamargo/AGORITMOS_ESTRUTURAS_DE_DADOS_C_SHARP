using System;

namespace EstruturasDeDados.ArvoresSegmentadas;

// Esta é uma extensão de uma árvore segmentada, que permite aplicar operações distributivas a um sub-array
// (neste caso, multiplicação).

public class ArvoreSegmentadaAplicar : ArvoreSegmentada {
    public ArvoreSegmentadaAplicar(int[] arr) : base(arr) {
        // Inicializa e preenche o array "operando" com o elemento neutro (neste caso 1, porque valor * 1 = valor)
        Operando = new int[Arvore.Length];
        Array.Fill(Operando, 1);
    }
    public int[] Operando { get; }
    public void Aplicar(int l, int r, int valor) {
        // A aplicação começa no nó com índice 1
        // O nó com índice 1 inclui todo o sub-array de entrada
        Aplicar(++l, ++r, valor, 1, Arvore.Length / 2, 1);
    }
    protected override int Consulta(int l, int r, int a, int b, int i) {
        if (l <= a && b <= r) {
            return Arvore[i];
        }
        if (r < a || b < l) {
            return 0;
        }
        var m = (a + b) / 2;
        // Aplicação do operando salvo aos nós filhos diretos e indiretos
        return Operando[i] * (Consulta(l, r, a, m, Esquerda(i)) + Consulta(l, r, m + 1, b, Direita(i)));
    }
    private void Aplicar(int l, int r, int valor, int a, int b, int i) {
        // Se a e b estiverem no sub-array especificado por l e r
        if (l <= a && b <= r) {
            // Aplica a operação ao nó atual e a salva para os nós filhos diretos e indiretos
            Operando[i] = valor * Operando[i];
            Arvore[i] = valor * Arvore[i];
            return;
        }

        // Se a ou b estiverem fora do sub-array especificado por l e r, interrompe a aplicação neste nó
        if (r < a || b < l) {
            return;
        }

        // Calcula o índice m do nó que corta o sub-array atual pela metade
        var m = (a + b) / 2;

        // Aplica a operação a ambas as metades
        Aplicar(l, r, valor, a, m, Esquerda(i));
        Aplicar(l, r, valor, m + 1, b, Direita(i));

        // Recalcula o valor deste nó por seus filhos (possivelmente novos).
        Arvore[i] = Operando[i] * (Arvore[Esquerda(i)] + Arvore[Direita(i)]);
    }
}