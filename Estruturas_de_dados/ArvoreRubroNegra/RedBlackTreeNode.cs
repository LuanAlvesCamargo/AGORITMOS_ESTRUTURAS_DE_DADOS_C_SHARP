namespace EstruturasDeDados.ArvoreRubroNegra;

// Enumeração para representar as cores dos nós.
public enum CorNo : byte {
    // Representa um nó vermelho.
    Vermelho,
    // Representa um nó preto.
    Preto,
}
    // Classe genérica para representar nós em uma instância de ArvoreRubroNegra{TChave}.
public class NoRubroNegro<TChave> {
    // Obtém ou define o valor da chave do nó.
    public TChave Chave { get; set; }
    // Obtém ou define a cor do nó.
    public CorNo Cor { get; set; }
    // Obtém ou define o pai do nó.
    public NoRubroNegro<TChave>? Pai { get; set; }
    // Obtém ou define o filho esquerdo do nó.
    public NoRubroNegro<TChave>? Esquerda { get; set; }
    // Obtém ou define o filho direito do nó.
    public NoRubroNegro<TChave>? Direita { get; set; }
    // Inicializa uma nova instância da classe. 
    public NoRubroNegro(TChave chave, NoRubroNegro<TChave>? pai) {
        Chave = chave;
        Pai = pai;
    }
}