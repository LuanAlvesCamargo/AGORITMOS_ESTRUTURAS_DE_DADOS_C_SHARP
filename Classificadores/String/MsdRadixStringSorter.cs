namespace Algoritmos.Ordenacao.Strings;

// O Radix sort é um algoritmo de ordenação não comparativo. Ele evita comparações criando
// e distribuindo elementos em baldes de acordo com seu radix.
// O Radix sort pode ser implementado para começar no dígito mais significativo (MSD)
// ou no dígito menos significativo (LSD).
// O Radix sort MSD é mais adequado para ordenar arrays de strings com comprimento variável
// em ordem lexicográfica.
public class OrdenadorRadixStringMSD : IOrdenadorString {
    public void Ordenar(string[] array) => Ordenar(array, 0, array.Length - 1, 0, new string[array.Length]);

    private static void Ordenar(string[] array, int esquerda, int direita, int digito, string[] temporario) {
        if (esquerda >= direita) {
            return;
        }

        const int k = 256; // Número de caracteres possíveis (ASCII estendido)

        var contagem = new int[k + 2];
        for (var i = esquerda; i <= direita; i++) {
            var j = Chave(array[i]);
            contagem[j + 2]++;
        }

        for (var i = 1; i < contagem.Length; i++) {
            contagem[i] += contagem[i - 1];
        }

        for (var i = esquerda; i <= direita; i++) {
            var j = Chave(array[i]);
            temporario[contagem[j + 1]++] = array[i];
        }

        for (var i = esquerda; i <= direita; i++) {
            array[i] = temporario[i - esquerda];
        }

        for (var i = 0; i < k; i++) {
            Ordenar(array, esquerda + contagem[i], esquerda + contagem[i + 1] - 1, digito + 1, temporario);
        }

        int Chave(string s) => digito >= s.Length ? -1 : s[digito];
    }
}
public interface IOrdenadorString {
    void Ordenar(string[] array);
}