using System.Collections.Generic;
using System.Linq;
using Algoritmos.Mochila;
using Utilidades.Extensoes;

namespace Algoritmos.CompressaoDados;

// Algoritmo de compressão sem perdas guloso Shannon-Fano.
public class CompressorShannonFano {
    private readonly ISolucionadorMochilaHeuristico<(char Simbolo, double Frequencia)> divisor;
    private readonly Tradutor tradutor;
    public CompressorShannonFano(
        ISolucionadorMochilaHeuristico<(char Simbolo, double Frequencia)> divisor,
        Tradutor tradutor) {
        this.divisor = divisor;
        this.tradutor = tradutor;
    }
    public (string TextoCompactado, Dictionary<string, string> ChavesDescompactacao) Compactar(string textoNaoCompactado) {
        if (string.IsNullOrEmpty(textoNaoCompactado)) {
            return (string.Empty, new Dictionary<string, string>());
        }

        if (textoNaoCompactado.Distinct().Count() == 1) {
            var dicionario = new Dictionary<string, string> {
                { "1", textoNaoCompactado[0].ToString() },
            };
            return (new string('1', textoNaoCompactado.Length), dicionario);
        }

        var no = ObterListaNoDoTexto(textoNaoCompactado);
        var arvore = GerarArvoreShannonFano(no);
        var (chavesCompactacao, chavesDescompactacao) = ObterChaves(arvore);
        return (tradutor.Traduzir(textoNaoCompactado, chavesCompactacao), chavesDescompactacao);
    }

    private (Dictionary<string, string> ChavesCompactacao, Dictionary<string, string> ChavesDescompactacao) ObterChaves(
        ListaNo arvore) {
        var chavesCompactacao = new Dictionary<string, string>();
        var chavesDescompactacao = new Dictionary<string, string>();

        if (arvore.Dados.Length == 1) {
            chavesCompactacao.Add(arvore.Dados[0].Simbolo.ToString(), string.Empty);
            chavesDescompactacao.Add(string.Empty, arvore.Dados[0].Simbolo.ToString());
            return (chavesCompactacao, chavesDescompactacao);
        }

        if (arvore.FilhoEsquerdo is not null) {
            var (chavesCompactacaoEsquerda, chavesDescompactacaoEsquerda) = ObterChaves(arvore.FilhoEsquerdo);
            chavesCompactacao.AdicionarMuitos(chavesCompactacaoEsquerda.Select(kvp => (kvp.Key, "0" + kvp.Value)));
            chavesDescompactacao.AdicionarMuitos(chavesDescompactacaoEsquerda.Select(kvp => ("0" + kvp.Key, kvp.Value)));
        }

        if (arvore.FilhoDireito is not null) {
            var (chavesCompactacaoDireita, chavesDescompactacaoDireita) = ObterChaves(arvore.FilhoDireito);
            chavesCompactacao.AdicionarMuitos(chavesCompactacaoDireita.Select(kvp => (kvp.Key, "1" + kvp.Value)));
            chavesDescompactacao.AdicionarMuitos(chavesDescompactacaoDireita.Select(kvp => ("1" + kvp.Key, kvp.Value)));
        }

        return (chavesCompactacao, chavesDescompactacao);
    }

    private ListaNo GerarArvoreShannonFano(ListaNo no) {
        if (no.Dados.Length == 1) {
            return no;
        }

        var esquerda = divisor.Resolver(no.Dados, 0.5 * no.Dados.Sum(x => x.Frequencia), x => x.Frequencia, _ => 1);
        var direita = no.Dados.Except(esquerda).ToArray();

        no.FilhoEsquerdo = GerarArvoreShannonFano(new ListaNo(esquerda));
        no.FilhoDireito = GerarArvoreShannonFano(new ListaNo(direita));

        return no;
    }

    // Encontra a frequência de cada caractere no texto.
    private ListaNo ObterListaNoDoTexto(string texto) {
        var contagemOcorrencias = new Dictionary<char, double>();

        for (var i = 0; i < texto.Length; i++) {
            var caractere = texto[i];
            if (!contagemOcorrencias.ContainsKey(caractere)) {
                contagemOcorrencias.Add(caractere, 0);
            }

            contagemOcorrencias[caractere]++;
        }

        return new ListaNo(contagemOcorrencias.Select(kvp => (kvp.Key, 1d * kvp.Value / texto.Length)).ToArray());
    }

    // Representa a estrutura de árvore para o algoritmo.
    public class ListaNo {
        public ListaNo((char Simbolo, double Frequencia)[] dados) => Dados = dados;
        public (char Simbolo, double Frequencia)[] Dados { get; }
        public ListaNo? FilhoDireito { get; set; }
        public ListaNo? FilhoEsquerdo { get; set; }
    }
}