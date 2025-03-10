namespace DataStructures.LinkedList.DoublyLinkedList;

public class NoDuplamenteLigado<T> {
    public NoDuplamenteLigado(T dados) => Dados = dados;
    public T Dados { get; }
    public NoDuplamenteLigado<T>? Proximo { get; set; }
    public NoDuplamenteLigado<T>? Anterior { get; set; }
}