using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmos.Ordenacao.Inteiros;

    // Classe que implementa o algoritmo de ordenação Bucket Sort (Ordenação por Baldes).
public class OrdenadorBalde : IOrdenadorInteiro {
    private const int NumeroDeDigitosNaBase10 = 10;

    // Ordena os elementos do array usando o algoritmo Bucket Sort.
    public void Ordenar(int[] array) {
        if (array.Length <= 1) { 
            return;
        }

        // Armazena o número máximo de dígitos nos números a serem ordenados
        var totalDeDigitos = NumeroDeDigitos(array);

        // Array de baldes onde os números serão colocados
        var baldes = new int[NumeroDeDigitosNaBase10, array.Length + 1];

        // Percorre todas as posições de dígitos e ordena cada número
        // de acordo com o valor da posição do dígito
        for (var passagem = 1; passagem <= totalDeDigitos; passagem++) {
            DistribuirElementos(array, baldes, passagem); // Passagem de distribuição
            ColetarElementos(array, baldes); // Passagem de coleta

            if (passagem != totalDeDigitos) {
                EsvaziarBaldes(baldes); // Define o tamanho dos baldes para 0
            }
        }
    }
    private static int NumeroDeDigitos(IEnumerable<int> array) => (int)Math.Floor(Math.Log10(array.Max()) + 1);
    // Distribui os elementos nos baldes com base no dígito especificado.
    private static void DistribuirElementos(IEnumerable<int> dados, int[,] baldes, int digito) {
        // Determina o divisor usado para obter o dígito específico
        var divisor = (int)Math.Pow(10, digito);

        foreach (var elemento in dados) {
            // Exemplo de número de balde para o dígito das centenas:
            // ( 1234 % 1000 ) / 100 --> 2
            var numeroDoBalde = NumeroDeDigitosNaBase10 * (elemento % divisor) / divisor;

            // Recupera o valor em baldes[ numeroDoBalde , 0 ] para
            // determinar a localização na linha para armazenar o elemento
            var numeroDoElemento = ++baldes[numeroDoBalde, 0]; // Localização no balde para colocar o elemento
            baldes[numeroDoBalde, numeroDoElemento] = elemento;
        }
    }
    private static void ColetarElementos(IList<int> dados, int[,] baldes) {
        var subscrito = 0; // Inicializa a localização em dados

        // Loop sobre os baldes
        for (var i = 0; i < NumeroDeDigitosNaBase10; i++) {
            // Loop sobre os elementos em cada balde
            for (var j = 1; j <= baldes[i, 0]; j++) {
                dados[subscrito++] = baldes[i, j]; // Adiciona o elemento ao array
            }
        }
    }

    // Define o tamanho de todos os baldes como zero.
    private static void EsvaziarBaldes(int[,] baldes) {
        for (var i = 0; i < NumeroDeDigitosNaBase10; i++) {
            baldes[i, 0] = 0; // Define o tamanho do balde para 0
        }
    }
}
    // Interface para ordenadores de inteiros.
public interface IOrdenadorInteiro {
    // Ordena um array de inteiros.
    void Ordenar(int[] array);
}