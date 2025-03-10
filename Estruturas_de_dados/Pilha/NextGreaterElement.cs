using System;
using System.Collections.Generic;

namespace Algorithms.Stack {
    public class ProximoElementoMaior {
        public int[] EncontrarProximoElementoMaior(int[] numeros) {
            int[] resultado = new int[numeros.Length];
            Stack<int> pilha = new Stack<int>();
            // Inicializa todos os elementos no array de resultado para -1
            for (int i = 0; i < numeros.Length; i++) {
                resultado[i] = -1;
            }

            for (int i = 0; i < numeros.Length; i++) {
                // Enquanto a pilha não estiver vazia e o elemento atual for maior que o elemento
                // correspondente ao índice armazenado no topo da pilha
                while (pilha.Count > 0 && numeros[i] > numeros[pilha.Peek()]) {
                    int indice = pilha.Pop();
                    resultado[indice] = numeros[i]; // Define o próximo elemento maior
                }
                pilha.Push(i); // Empurra o índice atual para a pilha
            }
            return resultado;
        }
    }
}