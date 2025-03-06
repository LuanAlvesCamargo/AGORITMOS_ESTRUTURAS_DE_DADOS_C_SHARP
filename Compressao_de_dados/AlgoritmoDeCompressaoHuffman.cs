using System;
using System.Collections.Generic;
using System.Linq;
using Algoritmos.Ordenacao.Comparacao;
using Utilidades.Extensoes;

namespace Algoritmos.CompressaoDeDados;

/// <summary>
///     Algoritmo de compressão sem perdas guloso (Huffman).
/// </summary>
public class CompressorHuffman {
    // TODO: Usar um ordenador parcial
    private readonly IOrdenadorComparacao<NoLista> ordenador;
    private readonly Tradutor tradutor;

    public CompressorHuffman(IOrdenadorComparacao<NoLista> ordenador, Tradutor tradutor) {
        this.ordenador = ordenador;
        this.tradutor = tradutor;
    }
    public (string TextoCompactado, Dictionary<string, string> ChavesDeDescompactacao) Compactar(string textoNaoCompactado) {
        if (string.IsNullOrEmpty(textoNaoCompactado)) {
            return (string.Empty, new Dictionary<string, string>());
        }

        if (textoNaoCompactado.Distinct().Count() == 1)
        {
            var dicionario = new Dictionary<string, string> {
                { "1", textoNaoCompactado[0].ToString() },
            };
            return (new string('1', textoNaoCompactado.Length), dicionario);
        }

        var nos = ObterNosListaDoTexto(textoNaoCompactado);
        var arvore = GerarArvoreHuffman(nos);
        var (chavesDeCompactacao, chavesDeDescompactacao) = ObterChaves(arvore);
        return (tradutor.Traduzir(textoNaoCompactado, chavesDeCompactacao), chavesDeDescompactacao);
    }

    private static NoLista[] ObterNosListaDoTexto(string texto) {
        var contagensDeOcorrencia = new Dictionary<char, int>();

        foreach (var caractere in texto) {
            if (!contagensDeOcorrencia.ContainsKey(caractere)) {
                contagensDeOcorrencia.Add(caractere, 0);
            }
            contagensDeOcorrencia[caractere]++;
        }

        return contagensDeOcorrencia.Select(kvp => new NoLista(kvp.Key, 1d * kvp.Value / texto.Length)).ToArray();
    }

    private (Dictionary<string, string> ChavesDeCompactacao, Dictionary<string, string> ChavesDeDescompactacao) ObterChaves(
        NoLista arvore) {
        var chavesDeCompactacao = new Dictionary<string, string>();
        var chavesDeDescompactacao = new Dictionary<string, string>();

        if (arvore.PossuiDados) {
            chavesDeCompactacao.Add(arvore.Dados.ToString(), string.Empty);
            chavesDeDescompactacao.Add(string.Empty, arvore.Dados.ToString());
            return (chavesDeCompactacao, chavesDeDescompactacao);
        }

        if (arvore.FilhoEsquerdo is not null) {
            var (chavesEsquerdaCompactacao, chavesEsquerdaDescompactacao) = ObterChaves(arvore.FilhoEsquerdo);
            chavesDeCompactacao.AdicionarMuitos(chavesEsquerdaCompactacao.Select(kvp => (kvp.Key, "0" + kvp.Value)));
            chavesDeDescompactacao.AdicionarMuitos(chavesEsquerdaDescompactacao.Select(kvp => ("0" + kvp.Key, kvp.Value)));
        }

        if (arvore.FilhoDireito is not null) {
            var (chavesDireitaCompactacao, chavesDireitaDescompactacao) = ObterChaves(arvore.FilhoDireito);
            chavesDeCompactacao.AdicionarMuitos(chavesDireitaCompactacao.Select(kvp => (kvp.Key, "1" + kvp.Value)));
            chavesDeDescompactacao.AdicionarMuitos(chavesDireitaDescompactacao.Select(kvp => ("1" + kvp.Key, kvp.Value)));

            return (chavesDeCompactacao, chavesDeDescompactacao);
        }

        return (chavesDeCompactacao, chavesDeDescompactacao);
    }

    private NoLista GerarArvoreHuffman(NoLista[] nos) {
        var comparador = new ComparadorNoLista();
        while (nos.Length > 1) {
            ordenador.Ordenar(nos, comparador);

            var esquerda = nos[0];
            var direita = nos[1];

            var novosNos = new NoLista[nos.Length - 1];
            Array.Copy(nos, 2, novosNos, 1, nos.Length - 2);
            novosNos[0] = new NoLista(esquerda, direita);
            nos = novosNos;
        }

        return nos[0];
    }

    public class NoLista {
        public NoLista(char dados, double frequencia) {
            PossuiDados = true;
            Dados = dados;
            Frequencia = frequencia;
        }

        public NoLista(NoLista filhoEsquerdo, NoLista filhoDireito) {
            FilhoEsquerdo = filhoEsquerdo;
            FilhoDireito = filhoDireito;
            Frequencia = filhoEsquerdo.Frequencia + filhoDireito.Frequencia;
        }

        public char Dados { get; }
        public bool PossuiDados { get; }
        public double Frequencia { get; }
        public NoLista? FilhoDireito { get; }
        public NoLista? FilhoEsquerdo { get; }
    }

    public class ComparadorNoLista : IComparer<NoLista> {
        public int Compare(NoLista? x, NoLista? y) {
            if (x is null || y is null) {
                return 0;
            }

            return x.Frequencia.CompareTo(y.Frequencia);
        }
    }
}