using System;

namespace DataStructures.LinkedList.CircularLinkedList {

    public class ListaLigadaCircular<T> {
        // Implementação da Lista Ligada Circular aqui
    }
    public class CircularLinkedList<T>{
        private CircularLinkedListNode<T>? tail;

        public CircularLinkedList() {
            tail = null;
        }
        public CircularLinkedListNode<T>? GetHead() {
            return tail?.Next;
        }

        public bool IsEmpty() {
            return tail == null;
        }

        public void InsertAtBeginning(T data) {
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty()) {
                tail = newNode;
                tail.Next = tail;
            } else {
                newNode.Next = tail!.Next;
                tail.Next = newNode;
            }
        }
        public void InsertAtEnd(T data) {
            var newNode = new CircularLinkedListNode<T>(data);
            if (IsEmpty()) {
                tail = newNode;
                tail.Next = tail;
            } else {
                newNode.Next = tail!.Next;
                tail.Next = newNode;
                tail = newNode;
            }
        }
        public void InsertAfter(T value, T data) {
            if (IsEmpty()) {
                throw new InvalidOperationException("List is empty.");
            } 
            var current = tail!.Next;
            do {
                if (current!.Data!.Equals(value)) {
                    var newNode = new CircularLinkedListNode<T>(data);
                    newNode.Next = current.Next;
                    current.Next = newNode;
                    return;
                }
                current = current.Next;
            } while (current != tail.Next);
        }
        public void DeleteNode(T value) {
            if (IsEmpty()) {
                throw new InvalidOperationException("List is empty.");
            }
            var current = tail!.Next;
            var previous = tail;

            do {
                if (current!.Data!.Equals(value)) {
                    if (current == tail && current.Next == tail) {
                        tail = null;
                    } else if (current == tail) {
                        previous!.Next = tail.Next;
                        tail = previous;
                    } else if (current == tail.Next) {
                        tail.Next = current.Next;
                    } else {
                        previous!.Next = current.Next;
                    }
                    return;
                }
                previous = current;
                current = current.Next;
            } while (current != tail!.Next);
        }
    }
}
