using System;
using System.Collections.Generic;

namespace Algorithms.Stack {
    //  Verifica se uma expressão tem parênteses correspondentes e balanceados.
    //  Determina se uma dada expressão de string contendo colchetes é balanceada.
    //  Uma string é considerada balanceada se todos os colchetes de abertura tiverem colchetes de fechamento correspondentes
    //  na ordem correta. Os colchetes suportados são '()', '{}' e '[]'.
    //  A expressão de string de entrada contendo os colchetes para verificar o balanceamento.
    //  true se os colchetes na expressão forem balanceados; caso contrário, false.
    //  Lançado quando a expressão de entrada contém caracteres inválidos ou é nula/vazia.
    //  Somente caracteres '(', ')', '{', '}', '[', ']' são permitidos.
    public class VerificadorParentesesBalanceados {
        private static readonly Dictionary<char, char> MapaParenteses = new Dictionary<char, char> {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
        };

        public bool EstaBalanceado(string expressao) {
            if (string.IsNullOrEmpty(expressao)) {
                throw new ArgumentException("A expressão de entrada não pode ser nula ou vazia.");
            }

            Stack<char> pilha = new Stack<char>();
            foreach (char c in expressao) {
                if (EhParenteseAberto(c)) {
                    pilha.Push(c);
                } else if (EhParenteseFechado(c)) {
                    if (!EhFechamentoBalanceado(pilha, c)) {
                        return false;
                    }
                } else {
                    throw new ArgumentException($"Caractere inválido '{c}' encontrado na expressão.");
                }
            }
            return pilha.Count == 0;
        }

        private static bool EhParenteseAberto(char c) {
            return c == '(' || c == '{' || c == '[';
        }

        private static bool EhParenteseFechado(char c) {
            return c == ')' || c == '}' || c == ']';
        }

        private static bool EhFechamentoBalanceado(Stack<char> pilha, char fechamento) {
            if (pilha.Count == 0) {
                return false;
            }
            char abertura = pilha.Pop();
            return EhParCorrespondente(abertura, fechamento);
        }

        private static bool EhParCorrespondente(char abertura, char fechamento) {
            return MapaParenteses.ContainsKey(abertura) && MapaParenteses[abertura] == fechamento;
        }
    }
}

