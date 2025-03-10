using System;

namespace EstruturasDeDados.ArvoreBodeExpiatorio;

// Classe que representa um nó da árvore bode expiatório. Tipo da chave do nó da árvore bode expiatório.
public class No<TChave> where TChave : IComparable {
    private No<TChave>? direita;
    private No<TChave>? esquerda;
    public TChave Chave { get; }
    public No<TChave>? Direita {
        get => direita;
        set {
            if (value != null && !value.EhMaiorOuIgualA(Chave)) {
                throw new ArgumentException("A chave do valor é menor ou igual à chave do filho direito do nó.", nameof(value));
            }
            direita = value;
        }
    }

    public No<TChave>? Esquerda {
        get => esquerda;
        set {
            if (value != null && value.EhMaiorOuIgualA(Chave)) {
                throw new ArgumentException("A chave do valor é maior ou igual à chave do filho esquerdo do nó.", nameof(value));
            }
            esquerda = value;
        }
    }

    public No(TChave chave) => Chave = chave;
    public No(TChave chave, No<TChave>? direita, No<TChave>? esquerda)
        : this(chave) {
        Direita = direita;
        Esquerda = esquerda;
    }
    public int ObterTamanho() => (Esquerda?.ObterTamanho() ?? 0) + 1 + (Direita?.ObterTamanho() ?? 0);
    public double ObterAlturaAlfa(double alfa) => Math.Floor(Math.Log(ObterTamanho(), 1.0 / alfa));
    public No<TChave> ObterNoMenorChave() => Esquerda?.ObterNoMenorChave() ?? this;
    public No<TChave> ObterNoMaiorChave() => Direita?.ObterNoMaiorChave() ?? this;
    public bool EstaBalanceadoEmPesoAlfa(double a) {
        var esquerdaBalanceada = (Esquerda?.ObterTamanho() ?? 0) <= a * ObterTamanho();
        var direitaBalanceada = (Direita?.ObterTamanho() ?? 0) <= a * ObterTamanho();
        return esquerdaBalanceada && direitaBalanceada;
    }

    private bool EhMaiorOuIgualA(TChave chave) {
        return Chave.CompareTo(chave) >= 0;
    }
}