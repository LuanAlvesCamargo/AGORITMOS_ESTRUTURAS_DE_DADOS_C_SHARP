using System;
using System.Collections.Generic;

namespace DataStructures.LinkedList.SinglyLinkedList;

// Implementação de uma lista encadeada simples.

public class ListaEncadeadaSimples<T> {
    // Aponta para o início da lista
    private SinglyLinkedListNode<T>? Cabeca { get; set; }
    public SinglyLinkedListNode<T> AdicionarPrimeiro(T dados) {
        var novoElementoLista = new SinglyLinkedListNode<T>(dados) {
            Proximo = Cabeca,
        };
        Cabeca = novoElementoLista;
        return novoElementoLista;
    }
    public SinglyLinkedListNode<T> AdicionarUltimo(T dados) {
        var novoElementoLista = new SinglyLinkedListNode<T>(dados);
        // Se a cabeça for nula, o elemento adicionado é o primeiro, portanto, é a cabeça
        if (Cabeca is null) {
            Cabeca = novoElementoLista;
            return novoElementoLista;
        }

        // Elemento temporário para evitar sobrescrever o original
        var elementoTemporario = Cabeca;
        // Itera por todos os elementos
        while (elementoTemporario.Proximo is not null) {
            elementoTemporario = elementoTemporario.Proximo;
        }
        // Adiciona o novo elemento ao último
        elementoTemporario.Proximo = novoElementoLista;
        return novoElementoLista;
    }
    public T ObterElementoPorIndice(int indice) {
        if (indice < 0) {
            throw new ArgumentOutOfRangeException(nameof(indice));
        }

        var elementoTemporario = Cabeca;

        for (var i = 0; elementoTemporario is not null && i < indice; i++) {
            elementoTemporario = elementoTemporario.Proximo;
        }

        if (elementoTemporario is null) {
            throw new ArgumentOutOfRangeException(nameof(indice));
        }
        return elementoTemporario.Dados;
    }

    public int Comprimento() {
        // Verifica se há uma cabeça
        if (Cabeca is null) {
            return 0;
        }
        var elementoTemporario = Cabeca;
        var comprimento = 1;
        while (elementoTemporario.Proximo is not null) {
            elementoTemporario = elementoTemporario.Proximo;
            comprimento++;
        }
        return comprimento;
    }

    public IEnumerable<T> ObterDadosLista() {
        // Elemento temporário para evitar sobrescrever o original
        var elementoTemporario = Cabeca;
        // Todos os elementos onde um atributo próximo existe
        while (elementoTemporario is not null) {
            yield return elementoTemporario.Dados;
            elementoTemporario = elementoTemporario.Proximo;
        }
    }

    public bool ExcluirElemento(T elemento) {
        var elementoAtual = Cabeca;
        SinglyLinkedListNode<T>? elementoAnterior = null;

        // Itera por todos os elementos
        while (elementoAtual is not null) {
            // Verifica se o elemento a ser excluído está neste elemento da lista
            if (elementoAtual.Dados is null && elemento is null || elementoAtual.Dados is not null && elementoAtual.Dados.Equals(elemento)) {
                // Se o elemento for a cabeça, basta pegar o próximo como cabeça
                if (elementoAtual.Equals(Cabeca)) {
                    Cabeca = Cabeca.Proximo;
                    return true;
                }
                // Caso contrário, pegue o anterior e sobrescreva o próximo com o que está atrás do excluído
                if (elementoAnterior is not null) {
                    elementoAnterior.Proximo = elementoAtual.Proximo;
                    return true;
                }
            }
            // Iterando
            elementoAnterior = elementoAtual;
            elementoAtual = elementoAtual.Proximo;
        }

        return false;
    }

    public bool ExcluirPrimeiro() {
        // Verifica se a lista está vazia
        if (Cabeca is null) {
            return false;
        }
        // Caso contrário, a cabeça é sobrescrita com o próximo elemento e a cabeça antiga é excluída
        Cabeca = Cabeca.Proximo;
        return true;
    }
    public bool ExcluirUltimo() {
        // Verifica se a lista está vazia
        if (Cabeca is null) {
            return false;
        }
        // Verifica se a lista tem apenas um elemento
        if (Cabeca.Proximo is null) {
            Cabeca = null;
            return true;
        }
        // Caso contrário, itera pela lista até o penúltimo elemento e exclui o último
        SinglyLinkedListNode<T>? penultimo = Cabeca;
        while (penultimo.Proximo?.Proximo is not null) {
            penultimo = penultimo.Proximo;
        }
        penultimo.Proximo = null;
        return true;
    }
}