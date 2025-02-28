using System.Globalization;
using System.Linq;
using System.Text;

namespace Algoritmos.Codificadores;
    // Classe para codificação de strings usando o algoritmo NYSIIS.

public class CodificadorNysiis {
    private static readonly char[] Vogais = { 'A', 'E', 'I', 'O', 'U' };

    public string Codificar(string texto) {
        texto = texto.ToUpper(CultureInfo.CurrentCulture);
        texto = RemoverEspacos(texto);
        texto = SubstituirInicio(texto);
        texto = SubstituirFim(texto);

        for (var i = 1; i < texto.Length; i++) {
            texto = AplicarSubstituicao(texto, i);
        }

        texto = RemoverDuplicatas(texto);
        return RemoverTerminais(texto);
    }

    private string RemoverEspacos(string texto) => texto.Replace(" ", string.Empty);

    private string RemoverDuplicatas(string texto) {
        var sb = new StringBuilder();
        sb.Append(texto[0]);
        foreach (var c in texto) {
            if (sb[^1] != c) {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    private string RemoverTerminais(string texto) {
        var substituicoes = new (string De, string Para)?[] {
            ("S", string.Empty),
            ("AY", "Y"),
            ("A", string.Empty),
        };
        var substituicao = substituicoes.FirstOrDefault(t => texto.EndsWith(t!.Value.De));
        if (substituicao is { }) {
            var (de, para) = substituicao!.Value;
            texto = Substituir(texto, texto.Length - de.Length, de.Length, para);
        }
        return texto;
    }

    private string AplicarSubstituicao(string texto, int i) {
        (string De, string Para)[] substituicoes = {
            ("EV", "AF"),
            ("E", "A"),
            ("I", "A"),
            ("O", "A"),
            ("U", "A"),
            ("Q", "G"),
            ("Z", "S"),
            ("M", "N"),
            ("KN", "NN"),
            ("K", "C"),
            ("SCH", "SSS"),
            ("PH", "FF"),
        };
        var substituido = TentarSubstituir(texto, i, substituicoes, out texto);
        if (substituido) {
            return texto;
        }

        // Substituições baseadas em contexto:
        // H seguido ou precedido por vogal -> substitui pelo caractere anterior
        if (texto[i] == 'H') {
            if (!Vogais.Contains(texto[i - 1]))
            {
                return SubstituirPorAnterior();
            }

            if (i < texto.Length - 1 && !Vogais.Contains(texto[i + 1]))
            {
                return SubstituirPorAnterior();
            }
        }

        // Vogal seguida de W -> mantém a vogal
        if (texto[i] == 'W' && Vogais.Contains(texto[i - 1])) {
            return SubstituirPorAnterior();
        }

        return texto;

        string SubstituirPorAnterior() => Substituir(texto, i, 1, texto[i - 1].ToString());
    }

    private bool TentarSubstituir(string texto, int indice, (string, string)[] opcoes, out string resultado) {
        foreach (var (de, para) in opcoes) {
            if (texto.Length >= indice + de.Length && texto.Substring(indice, de.Length) == de) {
                resultado = Substituir(texto, indice, de.Length, para);
                return true;
            }
        }
        resultado = texto;
        return false;
    }

    private string SubstituirInicio(string inicio) {
        var substituicoes = new (string De, string Para)?[] {
            ("MAC", "MCC"),
            ("KN", "NN"),
            ("K", "C"),
            ("PH", "FF"),
            ("PF", "FF"),
            ("SCH", "SSS"),
        };
        var substituicao = substituicoes.FirstOrDefault(t => inicio.StartsWith(t!.Value.De));
        if (substituicao is { }) {
            var (de, para) = substituicao!.Value;
            inicio = Substituir(inicio, 0, de.Length, para);
        }
        return inicio;
    }

    private string SubstituirFim(string fim) {
        var substituicoes = new (string De, string Para)?[] {
            ("EE", "Y"),
            ("IE", "Y"),
            ("DT", "D"),
            ("RT", "D"),
            ("NT", "D"),
            ("ND", "D"),
        };
        var substituicao = substituicoes.FirstOrDefault(t => fim.EndsWith(t!.Value.De));
        if (substituicao is { }) {
            var (de, para) = substituicao!.Value;
            fim = Substituir(fim, fim.Length - de.Length, de.Length, para);
        }
        return fim;
    }

    private string Substituir(string texto, int indice, int comprimento, string substituto) =>
        texto[..indice] + substituto + texto[(indice + comprimento)..];
}
