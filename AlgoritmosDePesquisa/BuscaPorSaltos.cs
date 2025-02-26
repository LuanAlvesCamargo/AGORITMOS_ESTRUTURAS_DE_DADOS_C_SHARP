using System;

namespace Algoritmos.Busca {

    // A Busca por Saltos (Jump Search) verifica menos elementos ao pular por etapas fixas.
    // O número ideal de elementos a serem pulados é a raiz quadrada de n, onde n é o número total de elementos no array.
    // Complexidade de tempo: O(√n)
    // Observação: O array deve estar ordenado previamente.
    // Array ordenado no qual será feita a busca. Não pode ser nulo.
    // Elemento a ser encontrado. Não pode ser nulo.
    // Se o elemento for encontrado, retorna o índice. Caso o array esteja vazio ou o item não seja encontrado, retorna -1.
    // Encontra o índice do elemento buscado dentro do array.
    public class BuscadorPorSalto<T> where T : IComparable<T> {
 

        public int EncontrarIndice(T[] arrayOrdenado, T itemBuscado)
        {
            if (arrayOrdenado is null) {
                throw new ArgumentNullException("arrayOrdenado");
            }

            if (itemBuscado is null) {
                throw new ArgumentNullException("itemBuscado");
            }

            // Define o tamanho do salto como a raiz quadrada do tamanho do array
            int tamanhoSalto = (int)Math.Floor(Math.Sqrt(arrayOrdenado.Length));
            int indiceAtual = 0;
            int proximoIndice = tamanhoSalto;

            // Verifica se o array não está vazio antes de iniciar a busca
            if (arrayOrdenado.Length != 0) {
                // Realiza saltos até encontrar um valor maior ou igual ao item buscado
                while (arrayOrdenado[proximoIndice - 1].CompareTo(itemBuscado) < 0) {
                    indiceAtual = proximoIndice;
                    proximoIndice += tamanhoSalto;

                    // Se ultrapassar os limites do array, ajusta para o último índice
                    if (proximoIndice >= arrayOrdenado.Length) {
                        proximoIndice = arrayOrdenado.Length - 1;
                        break;
                    }
                }

                // Realiza uma busca linear dentro do bloco identificado
                for (int i = indiceAtual; i <= proximoIndice; i++) {
                    if (arrayOrdenado[i].CompareTo(itemBuscado) == 0) {
                        return i; // Retorna o índice do elemento encontrado
                    }
                }
            }
            return -1; // Retorna -1 caso o elemento não seja encontrado
        }
    }
}
