using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algoritmos.Codificadores {
    // Classe responsável por codificar e decodificar textos utilizando o Cifra de Vigenère.

    public class CodificadorVigenere : ICodificador<string> {
        private readonly CodificadorCesar codificadorCesar = new(); 
        // Codifica um texto utilizando a chave especificada.
        // Complexidade de tempo: O(n),
        // Complexidade de espaço: O(n),
        // onde n é o tamanho do texto.
        
        public string Codificar(string texto, string chave) => Cifrar(texto, chave, codificadorCesar.Codificar);
        public string Decodificar(string texto, string chave) => Cifrar(texto, chave, codificadorCesar.Decodificar);
        private string Cifrar(string texto, string chave, Func<string, int, string> cifrador) {
            chave = AjustarChave(chave, texto.Length);
            var textoCodificado = new StringBuilder(texto.Length); 

            for (var i = 0; i < texto.Length; i++) {
                if (!char.IsLetter(texto[i])) {
                    _ = textoCodificado.Append(texto[i]);
                    continue;
                }

                var ultimaLetra = char.IsUpper(chave[i]) ? 'Z' : 'z';
                var simboloCodificado = cifrador(texto[i].ToString(), ultimaLetra - chave[i]);
                _ = textoCodificado.Append(simboloCodificado);
            }
            return textoCodificado.ToString();
        }
        // Ajusta a chave para que tenha o mesmo tamanho do texto.
        // Se a chave for menor que o texto, ela será repetida até atingir o tamanho necessário.
        private string AjustarChave(string chave, int tamanho) {
            if (string.IsNullOrEmpty(chave)) {
                throw new ArgumentOutOfRangeException($"{nameof(chave)} deve ser uma string não vazia");
            }

            var chaveAjustada = new StringBuilder(chave, tamanho);
            while (chaveAjustada.Length < tamanho) {
                _ = chaveAjustada.Append(chave);
            }

            return chaveAjustada.ToString();
        }
    }
    private IEnumerable<int> RemoverVogais(IEnumerable<int> numeros) => numeros.Where(i => i != 0);
    private IEnumerable<char> RemoverHWEW(string texto) => texto.Where(c => c != 'h' && c != 'w');
    private IEnumerable<int> ColapsarDuplicatas(IEnumerable<int> numeros) {
        var anterior = int.MinValue;
        foreach (var i in numeros) {
            if (anterior != i) {
                yield return i;
                anterior = i;
            }
        }
    }
    // Converte os caracteres do texto em números correspondentes de acordo com um mapeamento específico.
    private IEnumerable<int> GerarCodificacaoNumerica(IEnumerable<char> texto) => texto.Select(MapearParaNumero);
    // Mapeia caracteres para números com base em regras específicas.
    private int MapearParaNumero(char caractere) {
        var mapeamento = new Dictionary<char, int> {
            ['a'] = 0, ['e'] = 0, ['i'] = 0, ['o'] = 0, ['u'] = 0, ['y'] = 0,
            ['h'] = 8, ['w'] = 8,
            ['b'] = 1, ['f'] = 1, ['p'] = 1, ['v'] = 1,
            ['c'] = 2, ['g'] = 2, ['j'] = 2, ['k'] = 2, ['q'] = 2, ['s'] = 2, ['x'] = 2, ['z'] = 2,
            ['d'] = 3, ['t'] = 3,
            ['l'] = 4,
            ['m'] = 5, ['n'] = 5,
            ['r'] = 6,
        };
        return mapeamento[caractere];
    }
}
