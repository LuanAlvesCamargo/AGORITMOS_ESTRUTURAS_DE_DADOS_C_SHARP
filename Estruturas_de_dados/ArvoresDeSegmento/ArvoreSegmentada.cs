namespace EstruturasDeDados.ArvoresSegmentadas;

///     Esta é uma extensão de uma árvore segmentada, que permite a atualização de um único elemento.
public class ArvoreSegmentadaAtualizacao : ArvoreSegmentada {
    public ArvoreSegmentadaAtualizacao(int[] arr) : base(arr) {
    }
    public void Atualizar(int no, int valor) {
        Arvore[no + Arvore.Length / 2] = valor;
        Propagar(Pai(no + Arvore.Length / 2));
    }
    private void Propagar(int no) {
        if (no == 0) {
            // Passou da raiz
            return;
        }
        Arvore[no] = Arvore[Esquerda(no)] + Arvore[Direita(no)];
        Propagar(Pai(no));
    }
}

public class ArvoreSegmentada {
    protected int[] Arvore;
    public ArvoreSegmentada(int[] arr) {
        int altura = (int)Math.Ceiling(Math.Log2(arr.Length));
        int tamanhoArvore = (int)Math.Pow(2, altura + 1);
        Arvore = new int[tamanhoArvore];
        ConstruirArvore(arr, 0, arr.Length - 1, 1);
    }

    protected int ConstruirArvore(int[] arr, int inicio, int fim, int no) {
        if (inicio == fim) {
            Arvore[no] = arr[inicio];
            return Arvore[no];
        }
        int meio = (inicio + fim) / 2;
        Arvore[no] = ConstruirArvore(arr, inicio, meio, Esquerda(no)) +
                      ConstruirArvore(arr, meio + 1, fim, Direita(no));
        return Arvore[no];
    }

    protected int Esquerda(int no) {
        return 2 * no;
    }

    protected int Direita(int no) {
        return 2 * no + 1;
    }

    protected int Pai(int no) {
        return no / 2;
    }

    public int[] ObterArvore() {
        return Arvore;
    }
}