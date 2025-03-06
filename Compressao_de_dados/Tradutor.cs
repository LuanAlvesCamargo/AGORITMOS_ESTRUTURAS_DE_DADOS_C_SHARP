using System.Collections.Generic;
using System.Text;

namespace Algoritmos.CompressaoDados;

/// <summary>
///     Fornece métodos para conversão de texto por mapeamento de chaves.
/// </summary>
public class Tradutor {
    public string Traduzir(string texto, Dictionary<string, string> chavesDeTraducao) {
        var sb = new StringBuilder();

        var inicio = 0;
        for (var i = 0; i < texto.Length; i++) {
            var chave = texto.Substring(inicio, i - inicio + 1);
            if (chavesDeTraducao.ContainsKey(chave)) {
                _ = sb.Append(chavesDeTraducao[chave]);
                inicio = i + 1;
            }
        }

        return sb.ToString();
    }
}