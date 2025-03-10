namespace EstruturasDeDados.ArvoreAA;


// Classe genérica de nó para a Árvore AA.
public class NoArvoreAA<TChave> {
    public NoArvoreAA(TChave chave, int nivel) {
        Chave = chave;
        Nivel = nivel;
    }
    // Obtém ou define a chave deste nó.
    public TChave Chave { get; set; }
    // Obtém ou define o nível deste nó.
    public int Nivel { get; set; }
    // Obtém ou define a subárvore esquerda deste nó.
    public NoArvoreAA<TChave>? Esquerda { get; set; }
    // Obtém ou define a subárvore direita deste nó.
    public NoArvoreAA<TChave>? Direita { get; set; }
}