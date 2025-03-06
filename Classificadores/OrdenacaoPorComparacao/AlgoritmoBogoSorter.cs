using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Comparacao;
    // Classe que implementa o algoritmo de ordenação bogo sort.
    // Este algoritmo é conhecido por sua ineficiência e é usado principalmente
    // para fins educacionais e de demonstração.
    // Bogosort (também conhecido como CaseSort ou Estou com Sort), é um algoritmo de ordenação extremamente ineficiente. 
    // É baseado na reordenação aleatória dos elementos. Não é utilizado na prática, mas pode ser usado no ensino de algoritmos mais eficientes.
public class OrdenadorBogo<T> : IOrdenadorComparacao<T> {
    private readonly Random aleatorio = new();
    // Ordena um array usando o algoritmo bogo sort.
    // Este algoritmo embaralha o array repetidamente até que ele esteja ordenado.
    // É extremamente ineficiente e não deve ser usado em aplicações práticas.
    public void Ordenar(T[] array, IComparer<T> comparador) {
        while (!EstaOrdenado(array, comparador)) {
            Embaralhar(array);
        }
    }
    // Verifica se um array está ordenado de acordo com um comparador.
    private bool EstaOrdenado(T[] array, IComparer<T> comparador) {
        for (var i = 0; i < array.Length - 1; i++) {
            if (comparador.Compare(array[i], array[i + 1]) > 0) {
                return false;
            }
        }
        return true;
    }
    // Embaralha um array aleatoriamente.
    // Este método cria um novo array com os elementos do array original em ordem aleatória.
    private void Embaralhar(T[] array) {
        var posicoesOcupadas = new bool[array.Length];
        var novoArray = new T[array.Length];
        for (var i = 0; i < array.Length; i++) {
            int proximaPosicao;
            do {
                proximaPosicao = aleatorio.Next(0, int.MaxValue) % array.Length;
            } while (posicoesOcupadas[proximaPosicao]);
            posicoesOcupadas[proximaPosicao] = true;
            novoArray[proximaPosicao] = array[i];
        }

        for (var i = 0; i < array.Length; i++) {
            array[i] = novoArray[i];
        }
    }
}
    // Interface para ordenadores que usam comparações entre elementos.
    // O tipo dos elementos a serem ordenados
public interface IOrdenadorComparacao<T> {
    // Ordena um array usando um comparador especificado.
    void Ordenar(T[] array, IComparer<T> comparador);
}