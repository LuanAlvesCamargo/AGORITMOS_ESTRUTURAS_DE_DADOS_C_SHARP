using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmos.Codificadores;

    // Implementa a cifra de Feistel para codificação e decodificação de dados.

    // A cifra de Feistel é uma estrutura simétrica usada na construção de cifras de bloco.
    // Foi nomeada em homenagem a Horst Feistel, físico e criptógrafo pioneiro.
    // Essa estrutura é utilizada em diversos algoritmos de criptografia, como DES, GOST, Blowfish e Twofish.

public class CifraFeistel : ICodificador<uint> {
    // Número de rodadas para transformar o bloco de dados. Em cada rodada, uma nova chave de rodada é gerada.
    private const int Rodadas = 32;

    // Codifica um texto utilizando a cifra de Feistel.
    public string Codificar(string texto, uint chave) {
        List<ulong> blocosTexto = DividirTextoEmBlocos(texto);
        StringBuilder textoCodificado = new();

        foreach (ulong bloco in blocosTexto) {
            uint temporario = 0;
            uint subblocoDireito = (uint)(bloco & 0x00000000FFFFFFFF);
            uint subblocoEsquerdo = (uint)(bloco >> 32);
            uint chaveRodada;

            // Aplicação da rede de Feistel
            for (int rodada = 0; rodada < Rodadas; rodada++) {
                chaveRodada = ObterChaveRodada(chave, rodada);
                temporario = subblocoDireito ^ ModificarBloco(subblocoEsquerdo, chaveRodada);
                subblocoDireito = subblocoEsquerdo;
                subblocoEsquerdo = temporario;
            }

            ulong blocoCodificado = subblocoEsquerdo;
            blocoCodificado = (blocoCodificado << 32) | subblocoDireito;
            textoCodificado.Append(string.Format("{0:X16}", blocoCodificado));
        }

        return textoCodificado.ToString();
    }


    // Decodifica um texto codificado pela cifra de Feistel.
    public string Decodificar(string texto, uint chave)
    {
        if (texto.Length % 16 != 0) {
            throw new ArgumentException($"O tamanho de {nameof(chave)} deve ser divisível por 16");
        }

        List<ulong> blocosCodificados = ObterBlocosDoTextoCodificado(texto);
        StringBuilder textoDecodificado = new();

        foreach (ulong bloco in blocosCodificados) {
            uint temporario = 0;
            uint subblocoDireito = (uint)(bloco & 0x00000000FFFFFFFF);
            uint subblocoEsquerdo = (uint)(bloco >> 32);

            // Processo inverso da rede de Feistel para decodificação
            uint chaveRodada;
            for (int rodada = Rodadas - 1; rodada >= 0; rodada--) {
                chaveRodada = ObterChaveRodada(chave, rodada);
                temporario = subblocoEsquerdo ^ ModificarBloco(subblocoDireito, chaveRodada);
                subblocoEsquerdo = subblocoDireito;
                subblocoDireito = temporario;
            }

            ulong blocoDecodificado = subblocoEsquerdo;
            blocoDecodificado = (blocoDecodificado << 32) | subblocoDireito;

            for (int i = 0; i < 8; i++) {
                ulong caractere = (blocoDecodificado & 0xFF00000000000000) >> 56;

                if (caractere != 0) {
                    textoDecodificado.Append((char)caractere);
                }

                blocoDecodificado <<= 8;
            }
        }

        return textoDecodificado.ToString();
    }

    // Divide o texto em blocos de 8 bytes, preenchendo o último bloco se necessário.
    private static List<ulong> DividirTextoEmBlocos(string texto) {
        List<ulong> blocosTexto = new();
        byte[] textoBytes = Encoding.ASCII.GetBytes(texto);
        int deslocamento = 8;

        for (int i = 0; i < texto.Length; i += 8) {
            if (i > texto.Length - 8)
            {
                deslocamento = texto.Length - i;
            }

            string bloco = Convert.ToHexString(textoBytes, i, deslocamento);
            blocosTexto.Add(Convert.ToUInt64(bloco, 16));
        }

        return blocosTexto;
    }

    // Converte o texto codificado em blocos de 64 bits para decodificação.
    private static List<ulong> ObterBlocosDoTextoCodificado(string texto) {
        List<ulong> blocosTexto = new();

        for (int i = 0; i < texto.Length; i += 16) {
            ulong bloco = Convert.ToUInt64(texto.Substring(i, 16), 16);
            blocosTexto.Add(bloco);
        }

        return blocosTexto;
    }


    // Modifica um bloco de dados de forma determinística utilizando a chave fornecida.
    private static uint ModificarBloco(uint bloco, uint chave) {
        for (int i = 0; i < 32; i++) {
            bloco = ((bloco ^ 0x55555555) * bloco) % chave;
            bloco ^= chave;
        }

        return bloco;
    }

    // Gera uma chave de rodada com base na chave principal e no número da rodada.
    private static uint ObterChaveRodada(uint chave, int rodada) s{
        uint resultado = (uint)Math.Pow((double)chave, rodada + 2);
        return resultado ^ chave;
    }
}
