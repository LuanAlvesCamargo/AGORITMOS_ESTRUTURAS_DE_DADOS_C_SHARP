using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;

    // O Cocktail Sort (Ordenação Coquetel) é uma variação do Bubble Sort (Ordenação por Bolha),
    // onde o Cocktail Sort percorre um array dado em ambas as direções alternadamente.
public class OrdenadorCoquetel<T> : IOrdenadorComparacao<T> {
    // Ordena um array usando o algoritmo Cocktail Sort (Ordenação Coquetel).
    public void Ordenar(T[] array, IComparer<T> comparador) => OrdenarCoquetel(array, comparador);
    private static void OrdenarCoquetel(IList<T> array, IComparer<T> comparador) {
        var houveTroca = true;
        var indiceInicio = 0;
        var indiceFim = array.Count - 1;

        while (houveTroca) {
            houveTroca = false; // Resetar para cada iteração
            // Percorre da esquerda para a direita
            for (var i = indiceInicio; i < indiceFim; i++) {
                if (comparador.Compare(array[i], array[i + 1]) > 0) {
                    var valorMaior = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = valorMaior;
                    houveTroca = true;
                }
            }

            indiceFim--;

            if (!houveTroca) {
                break; // Se não houve trocas, o array está ordenado
            }

            houveTroca = false; // Resetar para a próxima iteração

            // Percorre da direita para a esquerda
            for (var i = indiceFim; i > indiceInicio; i--) {
                if (comparador.Compare(array[i], array[i - 1]) < 0) {
                    var valorMaior = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = valorMaior;
                    houveTroca = true;
                }
            }
            indiceInicio++;
        }
    }
}
public interface IOrdenadorComparacao<T> {
    void Ordenar(T[] array, IComparer<T> comparador);
}