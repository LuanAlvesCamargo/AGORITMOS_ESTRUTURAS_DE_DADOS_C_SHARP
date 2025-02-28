using System.Text;

namespace Algoritmos.Codificadores;

// Implementação do algoritmo de Cifra de César para criptografar e descriptografar textos.
// A Cifra de César é um método de substituição onde cada letra do texto é deslocada
// um número fixo de posições no alfabeto, determinado por uma chave.
public class CifraDeCesar : ICodificador<int> {
    
    // Método para codificar (criptografar) um texto utilizando a chave especificada.
    // Complexidade de tempo: O(n), pois percorremos cada caractere do texto uma única vez.
    // Complexidade de espaço: O(n), já que criamos um novo texto de mesmo tamanho que o original.
    public string Codificar(string texto, int chave) => AplicarCifra(texto, chave);

    // Método para decodificar (descriptografar) um texto criptografado pela Cifra de César.
    // Utiliza o mesmo método de codificação, mas com o deslocamento inverso.
    public string Decodificar(string texto, int chave) => AplicarCifra(texto, -chave);

    // Método privado que executa a lógica da Cifra de César.
    private static string AplicarCifra(string texto, int chave) {
        var textoModificado = new StringBuilder(texto.Length);
        
        // Percorre cada caractere do texto
        for (var i = 0; i < texto.Length; i++) {
            // Se o caractere não for uma letra, mantém ele inalterado.
            if (!char.IsLetter(texto[i])) { 
                textoModificado.Append(texto[i]);
                continue;
            }
            
            // Determina os limites do alfabeto para diferenciar maiúsculas de minúsculas
            var letraInicial = char.IsUpper(texto[i]) ? 'A' : 'a';
            var letraFinal = char.IsUpper(texto[i]) ? 'Z' : 'z';
            
            // Aplica o deslocamento da cifra de César dentro dos limites do alfabeto
            var novoCaractere = texto[i] + chave;
            
            // Se ultrapassar o limite superior, faz um ajuste para voltar ao início do alfabeto
            novoCaractere -= novoCaractere > letraFinal ? 26 * (1 + (novoCaractere - letraFinal - 1) / 26) : 0;
            
            // Se ultrapassar o limite inferior, faz um ajuste para voltar ao final do alfabeto
            novoCaractere += novoCaractere < letraInicial ? 26 * (1 + (letraInicial - novoCaractere - 1) / 26) : 0;
            
            // Adiciona o caractere codificado ao texto modificado
            textoModificado.Append((char)novoCaractere);
        }
        
        // Retorna o texto já codificado ou decodificado
        return textoModificado.ToString();
    }
}
