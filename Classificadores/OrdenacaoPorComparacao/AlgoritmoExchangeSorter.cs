using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // Classe que implementa o algoritmo de ordenação por troca (Exchange Sort).
    // O algoritmo Exchange Sort, também conhecido como Ordenação por Troca, é um dos algoritmos de ordenação mais simples de entender e implementar. 
    // No entanto, ele não é muito eficiente para grandes conjuntos de dados.
    // Como funciona:
    // Comparações: O algoritmo percorre o array diversas vezes. Em cada passagem, ele compara cada elemento com todos os elementos subsequentes.
    // Se um elemento for maior que um elemento subsequente (ou menor, dependendo da ordem desejada), os dois elementos são trocados de posição.
    // Trocas: As trocas de posição movem os elementos maiores para o final do array, como bolhas subindo em um líquido (daí a semelhança com o Bubble Sort).
    // Repetição: O processo de comparações e trocas se repete até que nenhuma troca seja necessária em uma passagem completa pelo array, 
    // o que significa que o array está ordenado.
    // Características:
    // Simplicidade: O Exchange Sort é fácil de entender e implementar.
    // Ineficiência: Sua complexidade de tempo é O(n²), o que o torna inadequado para grandes conjuntos de dados.
    // Estabilidade: O Exchange Sort é um algoritmo estável, o que significa que ele preserva a ordem relativa de elementos iguais.
    // In-place: O algoritmo ordena o array diretamente, sem necessidade de arrays auxiliares.
    // Exemplo: Vamos ordenar o array [5, 1, 4, 2, 8] usando o Exchange Sort:
    // Quando usar: O Exchange Sort é útil apenas para pequenos conjuntos de dados ou para fins educacionais, devido à sua simplicidade. 
    // Para conjuntos de dados maiores, algoritmos mais eficientes, como o Merge Sort ou o Quick Sort, são preferíveis.

public class OrdenadorTroca<T> : IOrdenadorComparacao<T> {
    //  Ordena um array usando o comparador especificado.
    //  Características:
    //  - Ordenação interna (in-place): ordena o array diretamente, sem criar cópias.
    //  - Ordenação estável: a ordem relativa de elementos iguais é preservada.
    //  - Complexidade de tempo: O(n^2), onde n é o comprimento do array.
    //  - Complexidade de espaço: O(1), pois usa espaço constante adicional   
    //  Algoritmo:
    //  1.  Percorre o array comparando cada elemento com todos os elementos subsequentes.
    //  2.  Se um elemento for maior que um elemento subsequente, troca-os de posição.
    //  3.  Repete os passos 1 e 2 até que o array esteja ordenado.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        for (var i = 0; i < array.Length - 1; i++) {
            for (var j = i + 1; j < array.Length; j++) {
                if (comparador.Compare(array[i], array[j]) > 0) {
                    (array[j], array[i]) = (array[i], array[j]);
                }
            }
        }
    }
}

public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}