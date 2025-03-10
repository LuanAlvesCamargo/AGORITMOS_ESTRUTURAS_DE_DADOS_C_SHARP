using System;

namespace Algorithms.Strings.Similarity;

public static class DistanciaDamerauLevenshtein {
    public static int Calcular(string esquerda, string direita) {
        // Obtém os comprimentos das strings de entrada.
        var tamanhoEsquerda = esquerda.Length;
        var tamanhoDireita = direita.Length;
        // Inicializa uma matriz de distâncias entre as duas strings.
        var distancias = InicializarMatrizDistancias(tamanhoEsquerda, tamanhoDireita);
        // Itera sobre cada caractere na string esquerda.
        for (var i = 1; i < tamanhoEsquerda + 1; i++) {
            // Itera sobre cada caractere na string direita.
            for (var j = 1; j < tamanhoDireita + 1; j++) {
                // Calcula o custo da operação atual.
                // Se os caracteres nas posições atuais forem os mesmos, o custo é 0.
                // Caso contrário, o custo é 1.
                var custo = esquerda[i - 1] == direita[j - 1] ? 0 : 1;
                // Calcula a distância mínima considerando três operações possíveis:
                // exclusão, inserção e substituição.
                distancias[i, j] = Math.Min(
                    Math.Min(// exclusão
                        distancias[i - 1, j] + 1, // exclui o caractere da string esquerda
                        distancias[i, j - 1] + 1), // insere o caractere na string direita
                    distancias[i - 1, j - 1] + custo); // substitui o caractere na string esquerda pelo caractere na string direita

                // Se o caractere atual na string esquerda for o mesmo que o caractere
                // duas posições à esquerda na string direita e o caractere atual
                // na string direita for o mesmo que o caractere uma posição à direita
                // na string esquerda, então também podemos considerar uma operação de transposição.
                if (i > 1 && j > 1 && esquerda[i - 1] == direita[j - 2] && esquerda[i - 2] == direita[j - 1]) {
                    distancias[i, j] = Math.Min(
                        distancias[i, j], // distância mínima atual
                        distancias[i - 2, j - 2] + custo); // transpõe os dois últimos caracteres
                }
            }
        }

        // Retorna a distância entre as duas strings.
        return distancias[tamanhoEsquerda, tamanhoDireita];
    }

    private static int[,] InicializarMatrizDistancias(int tamanhoEsquerda, int tamanhoDireita) {
        // Inicializa uma matriz de distâncias com dimensões uma maior que as strings de entrada.
        var matriz = new int[tamanhoEsquerda + 1, tamanhoDireita + 1];
        // Define os valores na primeira linha como os comprimentos da string esquerda.
        // Isso representa a distância quando a string esquerda está vazia.
        for (var i = 1; i < tamanhoEsquerda + 1; i++) {
            matriz[i, 0] = i;
        }
        // Define os valores na primeira coluna como os comprimentos da string direita.
        // Isso representa a distância quando a string direita está vazia.
        for (var i = 1; i < tamanhoDireita + 1; i++) {
            matriz[0, i] = i;
        }
        // Retorna a matriz de distâncias inicializada.
        return matriz;
    }
}