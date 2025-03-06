namespace Algoritmos.Ordenacao.Inteiros;

// Radix sort é um algoritmo de ordenação de inteiros não comparativo que ordena dados com chaves inteiras agrupando
// as chaves pelos dígitos individuais que compartilham a mesma posição e valor significativos.
// Uma notação posicional é necessária, mas como os inteiros podem representar strings de caracteres (por exemplo, nomes ou datas)
// e números de ponto flutuante formatados especialmente, o radix sort não se limita a inteiros.
public class OrdenadorRadix : IOrdenadorInteiro {
    public void Ordenar(int[] array) {
        var bits = 4; // Número de bits a serem considerados em cada passagem
        var balde = new int[array.Length]; // Array auxiliar para armazenar os elementos ordenados temporariamente
        var deslocamentoDireita = 0; // Deslocamento de bits para a direita
        for (var mascara = ~(-1 << bits); mascara != 0; mascara <<= bits, deslocamentoDireita += bits) {
            var contagemArray = new int[1 << bits]; // Array de contagem para cada grupo de bits
            foreach (var elemento in array) {
                var chave = (elemento & mascara) >> deslocamentoDireita; // Extrai o grupo de bits relevante
                ++contagemArray[chave]; // Incrementa a contagem para o grupo de bits
            }

            for (var i = 1; i < contagemArray.Length; ++i) {
                contagemArray[i] += contagemArray[i - 1]; // Calcula as posições finais dos grupos de bits
            }

            for (var p = array.Length - 1; p >= 0; --p) {
                var chave = (array[p] & mascara) >> deslocamentoDireita; // Extrai o grupo de bits relevante
                --contagemArray[chave]; // Decrementa a contagem para a posição correta
                balde[contagemArray[chave]] = array[p]; // Coloca o elemento no balde na posição correta
            }

            var temporario = balde; // Troca os arrays para a próxima passagem
            balde = array;
            array = temporario;
        }
    }
}
public interface IOrdenadorInteiro {
    void Ordenar(int[] array);
}