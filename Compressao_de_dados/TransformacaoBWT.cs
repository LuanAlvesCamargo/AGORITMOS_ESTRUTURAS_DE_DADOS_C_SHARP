using System;
using System.Linq;

namespace Algoritmos.CompressaoDados;

// A Transformação de Burrows-Wheeler (BWT) reorganiza uma string de caracteres em sequências de caracteres semelhantes.
// Isso é útil para compressão, pois tende a ser fácil comprimir uma string que possui sequências de caracteres repetidos.

public class TransformacaoBurrowsWheeler {
    // Codifica a string de entrada usando BWT e retorna a string codificada e o índice da string original na matriz de rotação ordenada.
    public (string Codificado, int Indice) Codificar(string s) {
        if (s.Length == 0) {
            return (string.Empty, 0);
        }

        var rotacoes = ObterRotacoes(s);
        Array.Sort(rotacoes, StringComparer.Ordinal);
        var ultimaColuna = rotacoes
            .Select(x => x[^1])
            .ToArray();
        var codificado = new string(ultimaColuna);
        return (codificado, Array.IndexOf(rotacoes, s));
    }
    // Decodifica a string de entrada e retorna a string original.
    public string Decodificar(string s, int indice) {
        if (s.Length == 0) {
            return string.Empty;
        }

        var rotacoes = new string[s.Length];

        for (var i = 0; i < s.Length; i++) {
            for (var j = 0; j < s.Length; j++) {
                rotacoes[j] = s[j] + rotacoes[j];
            }

            Array.Sort(rotacoes, StringComparer.Ordinal);
        }

        return rotacoes[indice];
    }
    // Gera todas as rotações cíclicas da string de entrada.
    private string[] ObterRotacoes(string s) {
        var resultado = new string[s.Length];

        for (var i = 0; i < s.Length; i++) {
            resultado[i] = s.Substring(i) + s.Substring(0, i);
        }

        return resultado;
    }
}