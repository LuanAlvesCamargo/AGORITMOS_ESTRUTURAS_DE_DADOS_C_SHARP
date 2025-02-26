using System;

namespace Algoritmos.Busca {

    //  Classe que implementa o algoritmo de busca de Fibonacci.
    //  O algoritmo de busca de Fibonacci é uma técnica de busca em arrays ordenados baseada na sequência de Fibonacci.
    //  Ele divide recursivamente o array em segmentos que seguem a razão de Fibonacci, reduzindo a área de busca
    //  em aproximadamente 1/3 a cada iteração. Essa abordagem é semelhante à busca binária, mas pode ser mais eficiente
    //  para certos tipos de dados estruturados.
    //   Encontra o índice do elemento buscado dentro do array.
    //  Complexidade de tempo:
    //  Caso pior: O(log n),
    //  Caso médio: O(log n),
    //  Melhor caso: O(1).
    // Array ordenado onde será feita a busca. Não pode ser nulo
    // Elemento a ser buscado. Não pode ser nulo.
    // Se o item for encontrado, retorna o índice. Se o array estiver vazio ou o item não for encontrado, retorna -1.
    // Lançada quando o array ou o item são nulos.

    public class BuscadorFibonacci<T> where T : IComparable<T> {
        public int EncontrarIndice(T[] array, T item) {
            if (array is null) {
                throw new ArgumentNullException(nameof(array));
            }

            if (item is null) {
                throw new ArgumentNullException(nameof(item));
            }

            var tamanhoArray = array.Length;

            if (tamanhoArray > 0) {
                // Encontrar o menor número de Fibonacci que seja maior ou igual ao tamanho do array
                var fibAnteriorAnterior = 0;
                var fibAnterior = 1;
                var fibAtual = fibAnterior;

                while (fibAtual <= tamanhoArray) {
                    fibAnteriorAnterior = fibAnterior;
                    fibAnterior = fibAtual;
                    fibAtual = fibAnteriorAnterior + fibAnterior;
                }

                // Offset para eliminar a parte esquerda do array progressivamente
                var deslocamento = -1;

                while (fibAtual > 1) {
                    var indice = Math.Min(deslocamento + fibAnteriorAnterior, tamanhoArray - 1);

                    switch (item.CompareTo(array[indice])) {
                        // Se o item for maior que o elemento no índice atual,
                        // descarta aproximadamente 1/3 do array na frente
                        case > 0:
                            fibAtual = fibAnterior;
                            fibAnterior = fibAnteriorAnterior;
                            fibAnteriorAnterior = fibAtual - fibAnterior;
                            deslocamento = indice;
                            break;

                        // Se o item for menor que o elemento no índice atual,
                        // descarta aproximadamente 2/3 do array atrás
                        case < 0:
                            fibAtual = fibAnteriorAnterior;
                            fibAnterior = fibAnterior - fibAnteriorAnterior;
                            fibAnteriorAnterior = fibAtual - fibAnterior;
                            break;
                        
                        // Item encontrado
                        default:
                            return indice;
                    }
                }

                // Verifica o último elemento
                if (fibAnterior == 1 && item.Equals(array[^1])) {
                    return tamanhoArray - 1;
                }
            }
            return -1;
        }
    }
}
x'