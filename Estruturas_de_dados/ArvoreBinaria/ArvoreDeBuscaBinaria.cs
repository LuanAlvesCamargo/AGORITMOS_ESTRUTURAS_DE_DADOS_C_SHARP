using System;
using System.Collections.Generic;

namespace DataStructures.AATree;


// Uma árvore de busca binária auto-balanceável simples.
// Árvores AA são uma forma de árvore de busca binária auto-balanceável, nomeadas em homenagem ao seu inventor,
// Arne Anderson. As Árvores AA são projetadas para serem simples de entender e implementar.
// Isso é conseguido limitando como os nós podem ser adicionados à árvore.
// Isso simplifica as operações de rebalanceamento.

public class AaTree<TKey> {
    
    // A função comparadora a ser usada para comparar as chaves.
    private readonly Comparer<TKey> comparer;
    public AaTree()
        : this(Comparer<TKey>.Default) {
    }
    public AaTree(Comparer<TKey> customComparer) => comparer = customComparer;
    // Obtém a raiz da árvore. 
    public AaTreeNode<TKey>? Root { get; private set; }
    // Obtém o número de elementos na árvore.
    public int Count { get; private set; }
    // Adicione um único elemento à árvore.
    public void Add(TKey key) {
        Root = Add(key, Root);
        Count++;
    }
    // Adicione vários elementos à árvore.
    public void AddRange(IEnumerable<TKey> keys) {
        foreach (var key in keys) {
            Root = Add(key, Root);
            Count++;
        }
    }

    // Remova um único elemento da árvore.
    public void Remove(TKey key) {
        if (!Contains(key, Root)) {
            throw new InvalidOperationException($"{nameof(key)} is not in the tree");
        }

        Root = Remove(key, Root);
        Count--;
    }
    // Verifica se o elemento especificado está na árvore.
    public bool Contains(TKey key) => Contains(key, Root);

    // Obtém o maior elemento na árvore. (ou seja, o elemento no nó mais à direita).
    // O maior elemento na árvore de acordo com o comparador armazenado.

    public TKey GetMax() {
        if (Root is null) {
            throw new InvalidOperationException("Tree is empty!");
        }
        return GetMax(Root).Key;
    }

    // Obtém o menor elemento na árvore. (ou seja, o elemento no nó mais à esquerda).
    // O menor elemento na árvore de acordo com o comparador armazenado.
    public TKey GetMin() {
        if (Root is null) {
            throw new InvalidOperationException("Tree is empty!");
        }
        return GetMin(Root).Key;
    }

    // Obtém todos os elementos na árvore em ordem.
    // Sequência de elementos em ordem.
    public IEnumerable<TKey> GetKeysInOrder() {
        var result = new List<TKey>();
        InOrderWalk(Root);
        return result;

        void InOrderWalk(AaTreeNode<TKey>? node) {
            if (node is null) {
                return;
            }

            InOrderWalk(node.Left);
            result.Add(node.Key);
            InOrderWalk(node.Right);
        }
    }
    // Obtém todos os elementos na árvore em ordem de pré-ordem.
    // Sequência de elementos em ordem de pré-ordem.
    public IEnumerable<TKey> GetKeysPreOrder() {
        var result = new List<TKey>();
        PreOrderWalk(Root);
        return result;

        void PreOrderWalk(AaTreeNode<TKey>? node) {
            if (node is null) s{
                return;
            }
            result.Add(node.Key);
            PreOrderWalk(node.Left);
            PreOrderWalk(node.Right);
        }
    }
    // Obtém todos os elementos na árvore em ordem pós-ordem.
    // Sequência de elementos em ordem pós-ordem.
    public IEnumerable<TKey> GetKeysPostOrder() {
        var result = new List<TKey>();
        PostOrderWalk(Root);
        return result;

        void PostOrderWalk(AaTreeNode<TKey>? node) {
            if (node is null) {
                return;
            }
            PostOrderWalk(node.Left);
            PostOrderWalk(node.Right);
            result.Add(node.Key);
        }
    }
    // Função recursiva para adicionar um elemento à árvore.
    private AaTreeNode<TKey> Add(TKey key, AaTreeNode<TKey>? node) {
        if (node is null) {
            return new AaTreeNode<TKey>(key, 1);
        }

        if (comparer.Compare(key, node.Key) < 0) {
            node.Left = Add(key, node.Left);
        } else if (comparer.Compare(key, node.Key) > 0) {
            node.Right = Add(key, node.Right);
        } else {
            throw new ArgumentException($"Key \"{key}\" already in tree!", nameof(key));
        }
        return Split(Skew(node))!;
    }
    // Recursive function to remove an element from the tree.
    // The node with the specified element removed.
    private AaTreeNode<TKey>? Remove(TKey key, AaTreeNode<TKey>? node) {
        if (node is null) {
            return null;
        }

        if (comparer.Compare(key, node.Key) < 0) {
            node.Left = Remove(key, node.Left);
        }
        else if (comparer.Compare(key, node.Key) > 0) {
            node.Right = Remove(key, node.Right);
        } else {
            if (node.Left is null && node.Right is null) {
                return null;
            }
 
            if (node.Left is null) {
                var successor = GetMin(node.Right!);
                node.Right = Remove(successor.Key, node.Right);
                node.Key = successor.Key;
            } else {
                var predecessor = GetMax(node.Left);
                node.Left = Remove(predecessor.Key, node.Left);
                node.Key = predecessor.Key;
            }
        }

        node = DecreaseLevel(node);
        node = Skew(node);
        node!.Right = Skew(node.Right);

        if (node.Right is not null) {
            node.Right.Right = Skew(node.Right.Right);
        }

        node = Split(node);
        node!.Right = Split(node.Right);
        return node;
    }

    // Função recursiva para verificar se o elemento existe na árvore.
    private bool Contains(TKey key, AaTreeNode<TKey>? node) =>
        node is { }
        && comparer.Compare(key, node.Key) is { } v
        && v switch {
            { } when v > 0 => Contains(key, node.Right),
            { } when v < 0 => Contains(key, node.Left),
            _ => true,
        };
    // Recursivo para encontrar o elemento máximo/mais à direita.

    private AaTreeNode<TKey> GetMax(AaTreeNode<TKey> node) {
        while (true) {
            if (node.Right is null) {
                return node;
            }
            node = node.Right;
        }
    }

    private AaTreeNode<TKey> GetMin(AaTreeNode<TKey> node) {
        while (true) {
            if (node.Left is null) {
                return node;
            }
            node = node.Left;
        }
    }
    private AaTreeNode<TKey>? Skew(AaTreeNode<TKey>? node) {
        if (node?.Left is null || node.Left.Level != node.Level) {
            return node;
        }
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;
        return left;
    }
    
    private AaTreeNode<TKey>? Split(AaTreeNode<TKey>? node) {
        if (node?.Right?.Right is null || node.Level != node.Right.Right.Level) {
            return node;
        }
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;
        right.Level++;
        return right;
    }

    private AaTreeNode<TKey> DecreaseLevel(AaTreeNode<TKey> node) {
        var newLevel = Math.Min(GetLevel(node.Left), GetLevel(node.Right)) + 1;
        if (newLevel >= node.Level) {
            return node;
        }
        node.Level = newLevel; 
        if (node.Right is { } && newLevel < node.Right.Level) {
            node.Right.Level = newLevel;
        }
        return node;
        static int GetLevel(AaTreeNode<TKey>? x) => x?.Level ?? 0;
    }
}
