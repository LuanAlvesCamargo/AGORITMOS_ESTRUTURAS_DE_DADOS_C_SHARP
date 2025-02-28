using System.Collections.Generic;
using System.Linq;

namespace Algoritmos.Codificadores;

// Classe para codificação de strings utilizando o algoritmo Soundex.
public class CodificadorSoundex {

    public string Codificar(string texto) {
        texto = texto.ToLowerInvariant();
        var caracteres = RemoverHyW(texto);
        IEnumerable<int> numeros = GerarCodificacaoNumerica(caracteres);
        numeros = RemoverDuplicatas(numeros);
        numeros = RemoverVogais(numeros);
        numeros = AjustarPrimeiroDigito(numeros, texto[0]);
        numeros = numeros.Take(3);
        numeros = PreencherComZeros(numeros);
        var resultado = numeros.ToArray();
        return $"{texto.ToUpperInvariant()[0]}{resultado[0]}{resultado[1]}{resultado[2]}";
    }

    private IEnumerable<int> AjustarPrimeiroDigito(IEnumerable<int> numeros, char primeiroCaractere) {
        using var enumerador = numeros.GetEnumerator();
        enumerador.MoveNext();
        if (enumerador.Current == MapearParaNumero(primeiroCaractere)) {
            enumerador.MoveNext();
        } do {
            yield return enumerador.Current;
        }
        while (enumerador.MoveNext());
    }
    // Garante que a sequência tenha pelo menos três números, preenchendo com zeros se necessário.
    private IEnumerable<int> PreencherComZeros(IEnumerable<int> numeros) {
        using var enumerador = numeros.GetEnumerator();
        for (var i = 0; i < 3; i++) {
            yield return enumerador.MoveNext() ? enumerador.Current : 0;
        }
    }
    // Remove os números correspondentes a vogais (representados por 0 no mapeamento).
    private IEnumerable<int> RemoverVogais(IEnumerable<int> numeros) => numeros.Where(i => i != 0);
    // Remove os caracteres 'h' e 'w', pois eles não influenciam na codificação Soundex.
    private IEnumerable<char> RemoverHyW(string texto) => texto.Where(c => c != 'h' && c != 'w');
    // Remove números consecutivos idênticos para evitar redundância na codificação.
    private IEnumerable<int> RemoverDuplicatas(IEnumerable<int> numeros) {
        var anterior = int.MinValue;
        foreach (var numero in numeros) {
            if (anterior != numero) {
                yield return numero;
                anterior = numero;
            }
        }
    }
    // Converte os caracteres restantes em números de acordo com o mapeamento Soundex.
    private IEnumerable<int> GerarCodificacaoNumerica(IEnumerable<char> texto) => texto.Select(MapearParaNumero);
    // Mapeia caracteres para os respectivos valores numéricos no algoritmo Soundex.
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
