using System;

namespace EstruturasDeDados.ArvoreAVL;


// Classe genérica para representar nós em uma instância de 

internal class NoArvoreAVL<TChave> {
    public TChave Chave { get; set; }
    public int FatorDeBalanceamento { get; private set; }
    public NoArvoreAVL<TChave>? Esquerda { get; set; }
    public NoArvoreAVL<TChave>? Direita { get; set; }
    private int Altura { get; set; }
    public NoArvoreAVL(TChave chave) {
        Chave = chave;
    }

    public void AtualizarFatorDeBalanceamento() {
        if (Esquerda is null && Direita is null) {
            Altura = 0;
            FatorDeBalanceamento = 0;
        } else if (Esquerda is null) {
            Altura = Direita!.Altura + 1;
            FatorDeBalanceamento = Altura;
        } else if (Direita is null)  {
            Altura = Esquerda!.Altura + 1;
            FatorDeBalanceamento = -Altura;
        } else {
            Altura = Math.Max(Esquerda.Altura, Direita.Altura) + 1;
            FatorDeBalanceamento = Direita.Altura - Esquerda.Altura;
        }
    }
}