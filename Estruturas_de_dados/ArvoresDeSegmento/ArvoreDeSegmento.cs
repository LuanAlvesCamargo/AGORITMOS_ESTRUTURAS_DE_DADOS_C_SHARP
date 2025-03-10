using System;

namespace EstruturasDeDados.ArvoresSegmentadas;

// Objetivo: Estrutura de dados com a qual você pode realizar rapidamente consultas em um array (ou seja, soma de sub-array)
// e, ao mesmo tempo, atualizar eficientemente uma entrada
// ou aplicar uma operação distributiva a um sub-array. Pré-processamento de consultas especiais
public class ArvoreSegmentada {
    public ArvoreSegmentada(int[] array) {
        // Calcula a próxima potência de dois
        var potencia = (int)Math.Pow(2, Math.Ceiling(Math.Log(array.Length, 2)));
        Arvore = new int[2 * potencia];
        // Transfere o array de entrada para a última metade do array da árvore segmentada
        Array.Copy(array, 0, Arvore, potencia, array.Length);
        // Calcula a primeira metade
        for (var i = potencia - 1; i > 0; --i) {
            Arvore[i] = Arvore[Esquerda(i)] + Arvore[Direita(i)];
        }
    }
    public int[] Arvore { get; }
    public int Consulta(int esquerda, int direita) => Consulta(++esquerda, ++direita, 1, Arvore.Length / 2, 1);
    protected int Direita(int no) => 2 * no + 1;
    protected int Esquerda(int no) => 2 * no;
    protected int Pai(int no) => no / 2;
    protected virtual int Consulta(int esquerda, int direita, int a, int b, int i) {
        // Se a e b estiverem no sub-array especificado por esquerda e direita
        if (esquerda <= a && b <= direita) {
            return Arvore[i];
        }
        // Se a ou b estiverem fora do sub-array especificado por esquerda e direita
        if (direita < a || b < esquerda) {
            // Retorna o valor neutro da operação
            // (neste caso, 0, porque x + 0 = x)
            return 0;
        }
        // Calcula o índice m do nó que corta o sub-array atual pela metade
        var m = (a + b) / 2;
        // Inicia a consulta de dois novos sub-arrays a:m e m+1:b
        // Os filhos direito e esquerdo cobrem esses intervalos
        return Consulta(esquerda, direita, a, m, Esquerda(i)) + Consulta(esquerda, direita, m + 1, b, Direita(i));
    }
}