using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenadores.Comparacao {

    // Uma implementação básica do algoritmo TimSort para ordenar arrays.
    // O tipo dos elementos no array.
    // TimSort é um algoritmo híbrido de ordenação baseado no MergeSort e InsertionSort. 
    // O algoritmo baseia-se na ideia de que, no mundo real, um vetor de dados a ser ordenado contém sub-vetores já ordenados, 
    // não importando como (decrescentemente ou crescentemente).
    public class OrdenadorTimBasico<T> {
        // Tamanho mínimo de um "run" (sequência ordenada) para o Insertion Sort.
        private readonly int tamanhoMinimoRun = 32;
        // Comparador usado para comparar elementos.
        private readonly IComparer<T> comparador;
        // Inicializa uma nova instância da classe <see cref="OrdenadorTimBasico{T}"/>.
        // O comparador a ser usado para comparar elementos.
        public OrdenadorTimBasico(IComparer<T> comparador) {
            // Se nenhum comparador for fornecido, usa o comparador padrão para o tipo T.
            this.comparador = comparador ?? Comparer<T>.Default;
        }
        // Ordena o array especificado usando o algoritmo TimSort.
        // O array a ser ordenado.
        public void Ordenar(T[] array) {
            var tamanhoArray = array.Length;
            // Passo 1: Ordena pequenos pedaços do array usando Insertion Sort.
            // O TimSort começa dividindo o array em "runs" de tamanho mínimo,
            // e cada "run" é ordenado individualmente usando Insertion Sort.
            for (var indiceInicioRun = 0; indiceInicioRun < tamanhoArray; indiceInicioRun += tamanhoMinimoRun) {
                // Calcula o índice final do "run" atual, garantindo que não ultrapasse os limites do array.
                var indiceFinalRun = Math.Min(indiceInicioRun + tamanhoMinimoRun - 1, tamanhoArray - 1);
                OrdenarPorInsercao(array, indiceInicioRun, indiceFinalRun);
            }
            // Passo 2: Mescla "runs" ordenados usando Merge Sort.
            // Após ordenar os "runs" iniciais, o TimSort mescla esses "runs" de forma eficiente,
            // aumentando gradualmente o tamanho dos "runs" mesclados até que todo o array esteja ordenado.
            for (var tamanhoRunMesclado = tamanhoMinimoRun; tamanhoRunMesclado < tamanhoArray; tamanhoRunMesclado *= 2) {
                for (var indiceEsquerda = 0; indiceEsquerda < tamanhoArray; indiceEsquerda += 2 * tamanhoRunMesclado) {
                    // Calcula os índices do meio e da direita para a mesclagem.
                    var indiceMeio = indiceEsquerda + tamanhoRunMesclado - 1;
                    var indiceDireita = Math.Min(indiceEsquerda + 2 * tamanhoRunMesclado - 1, tamanhoArray - 1);
                    // Verifica se há algo para mesclar (se o índice do meio é menor que o índice da direita).
                    if (indiceMeio < indiceDireita) {
                        Mesclar(array, indiceEsquerda, indiceMeio, indiceDireita);
                    }
                }
            }
        }
        // Ordena uma porção do array usando o algoritmo Insertion Sort.
        // O array a ser ordenado.
        // O índice inicial da porção a ser ordenada.
        // índice final da porção a ser ordenada.
        private void OrdenarPorInsercao(T[] array, int indiceEsquerda, int indiceDireita) {
            for (var i = indiceEsquerda + 1; i <= indiceDireita; i++) {
                var chave = array[i];
                var j = i - 1;
                // Move os elementos de array[0..i-1], que são maiores que a chave,
                // uma posição à frente de sua posição atual.
                while (j >= indiceEsquerda && comparador.Compare(array[j], chave) > 0) {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = chave;
            }
        }
            // Mescla dois subarrays ordenados em um único subarray ordenado.
            // O array contendo os subarrays a serem mesclados.
            // índice inicial do primeiro subarray.
            // O índice final do primeiro subarray.
            // O índice final do segundo subarray.
        private void Mesclar(T[] array, int indiceEsquerda, int indiceMeio, int indiceDireita) {
            // Cria segmentos para os subarrays esquerdo e direito.
            var segmentoEsquerdo = new ArraySegment<T>(array, indiceEsquerda, indiceMeio - indiceEsquerda + 1);
            var segmentoDireito = new ArraySegment<T>(array, indiceMeio + 1, indiceDireita - indiceMeio);
            // Converte os segmentos em arrays.
            var arrayEsquerdo = segmentoEsquerdo.ToArray();
            var arrayDireito = segmentoDireito.ToArray();
            var i = 0;
            var j = 0;
            var k = indiceEsquerda;
            // Mescla os dois subarrays de volta no array principal.
            while (i < arrayEsquerdo.Length && j < arrayDireito.Length) {
                array[k++] = comparador.Compare(arrayEsquerdo[i], arrayDireito[j]) <= 0 ? arrayEsquerdo[i++] : arrayDireito[j++];
            }
            // Copia os elementos restantes do arrayEsquerdo, se houver.
            while (i < arrayEsquerdo.Length) {
                array[k++] = arrayEsquerdo[i++];
            }
            // Copia os elementos restantes do arrayDireito, se houver.
            while (j < arrayDireito.Length) {
                array[k++] = arrayDireito[j++];
            }
        }
    }
}
