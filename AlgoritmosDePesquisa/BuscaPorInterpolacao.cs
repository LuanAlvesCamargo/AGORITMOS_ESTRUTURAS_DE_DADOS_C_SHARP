namespace Algoritmos.Busca {
    // Classe que implementa o algoritmo de busca por interpolação.
    // Encontra o índice de um elemento dentro de um array ordenado.
    // Desempenho do algoritmo:
    // Pior caso: O(n)
    // Caso médio: O(log(log(n)))
    // Melhor caso: O(1)
    // Array de inteiros ordenados onde a busca será realizada. Não pode ser nulo.
    // Valor a ser buscado no array. Não pode ser nulo.
    // Se o valor for encontrado, retorna o índice. Caso contrário, retorna -1.
    public static class BuscaPorInterpolacao {

        public static int EncontrarIndice(int[] arrayOrdenado, int valor) {
            int inicio = 0;
            int fim = arrayOrdenado.Length - 1;

            // A busca continua enquanto a faixa de busca for válida e o valor estiver dentro dos limites do array.
            while (inicio <= fim && valor >= arrayOrdenado[inicio] && valor <= arrayOrdenado[fim]) {
                // Calcula a posição estimada do elemento no array usando interpolação.
                int denominador = (arrayOrdenado[fim] - arrayOrdenado[inicio]) * (valor - arrayOrdenado[inicio]);
                
                // Evita divisão por zero.
                if (denominador == 0) {
                    denominador = 1;
                }

                int posicao = inicio + (fim - inicio) / denominador;

                // Se o elemento na posição estimada for o valor buscado, retorna o índice.
                if (arrayOrdenado[posicao] == valor) {
                    return posicao;
                }

                // Se o valor estiver à direita da posição estimada, ajusta o intervalo para a metade superior.
                if (arrayOrdenado[posicao] < valor) {
                    inicio = posicao + 1;
                } else  { // Caso contrário, ajusta o intervalo para a metade inferior.
                    fim = posicao - 1;
                }
            }
            // Retorna -1 caso o elemento não seja encontrado no array.
            return -1;
        }
    }
}