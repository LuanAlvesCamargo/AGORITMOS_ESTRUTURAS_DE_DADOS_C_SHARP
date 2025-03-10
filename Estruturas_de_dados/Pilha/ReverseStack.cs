using System;
using System.Collections.Generic;

namespace Algorithms.Stack {
    public class InverterPilha {
        public void Inverter<T>(Stack<T> pilha) {
            if (pilha.Count == 0) {
                return;
            }
            T temp = pilha.Pop();
            Inverter(pilha);
            InserirNaBase(pilha, temp);
        }

        private void InserirNaBase<T>(Stack<T> pilha, T valor) {
            if (pilha.Count == 0) {
                pilha.Push(valor);
            } else {
                T temp = pilha.Pop();
                InserirNaBase(pilha, valor);
                pilha.Push(temp);
            }
        }
    }
}