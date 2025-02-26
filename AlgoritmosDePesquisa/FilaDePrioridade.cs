using System;
using System.Collections.Generic;

// Estrutura de Dados para a Fila de Prioridade
namespace Algoritmos.Busca.AEstrela {
    // Na ciência da computação, uma fila prioritária é um tipo de dados abstrato semelhante a uma fila 
    // regular ou tipo de dados abstratos de pilha. Cada elemento em uma fila de prioridade possui uma prioridade associada.
    // Fila de Prioridade Genérica.
    // Baseada em Lista.
    // O tipo de dado armazenado.
    // Deve implementar IComparable de T.

    public class FilaPrioridade<T> where T : IComparable<T> {
        private readonly bool ordemDecrescente;
        private readonly List<T> lista; // Lista interna usada para armazenar os elementos

        public FilaPrioridade(bool ordemDecrescente = false) {
            this.ordemDecrescente = ordemDecrescente;
            lista = new List<T>();
        }

        // Inicializa uma nova instância da classe 
        // Capacidade inicial da lista.
        // Define se a ordem deve ser invertida. Padrão: falso.
        public FilaPrioridade(int capacidade, bool ordemDecrescente = false) {
            lista = new List<T>(capacidade);
            this.ordemDecrescente = ordemDecrescente;
        }

        // Inicializa uma nova instância da classe
        // Dados iniciais a serem inseridos.
        // Define se a ordem deve ser invertida. Padrão: falso.
        public FilaPrioridade(IEnumerable<T> colecao, bool ordemDecrescente = false) : this() {
            this.ordemDecrescente = ordemDecrescente;
            foreach (var item in colecao) {
                Enfileirar(item);
            }
        }

        // Obtém o número de elementos na fila.
        public int Contagem => lista.Count;

        // Adiciona um item à fila de prioridade.
        // O item a ser enfileirado.
        public void Enfileirar(T item) {
            lista.Add(item);
            int i = Contagem - 1; // Índice do item recém-adicionado

            // Reorganiza o heap para manter a propriedade de prioridade
            while (i > 0) {
                int pai = (i - 1) / 2; // Índice do elemento pai
                if ((ordemDecrescente ? -1 : 1) * lista[pai].CompareTo(item) <= 0) {
                    break;
                }
                lista[i] = lista[pai];
                i = pai;
            }
            if (Contagem > 0) {
                lista[i] = item;
            }
        }

        // Remove e retorna o item de maior prioridade.
        public T Desenfileirar() {
            var alvo = Espiar(); // Obtém o elemento com maior prioridade
            var raiz = lista[Contagem - 1]; // Último elemento da lista
            lista.RemoveAt(Contagem - 1);

            int i = 0;
            while (i * 2 + 1 < Contagem) {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = (b < Contagem && (ordemDecrescente ? -1 : 1) * lista[b].CompareTo(lista[a]) < 0) ? b : a;

                if ((ordemDecrescente ? -1 : 1) * lista[c].CompareTo(raiz) >= 0) {
                    break;
                }

                lista[i] = lista[c];
                i = c;
            }
            if (Contagem > 0) {
                lista[i] = raiz;
            }
            return alvo;
        }

        //nRetorna o próximo item da fila sem removê-lo.
        // O próximo item da fila.
        public T Espiar() {
            if (Contagem == 0) {
                throw new InvalidOperationException("A fila está vazia.");
            }
            return lista[0];
        }

        // Limpa todos os elementos da fila.
        public void Limpar() => lista.Clear();
        // Retorna a estrutura de dados interna.
        // 1 lista interna.
        public List<T> ObterDados() => lista;
    }
}