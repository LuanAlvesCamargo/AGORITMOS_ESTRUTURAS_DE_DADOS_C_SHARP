namespace DataStructures.Heap.PairingHeap;

public class NoHeapPareamento<T> {

    // O nó representava o valor e as conexões.
    public NoHeapPareamento(T valor) {
        Valor = valor;
    }

    public T Valor { get; set; }
    public NoHeapPareamento<T> CabecaFilhos { get; set; } = null!;
    public bool EhFilhoCabeca => Anterior != null && Anterior.CabecaFilhos == this;
    public NoHeapPareamento<T> Anterior { get; set; } = null!;
    public NoHeapPareamento<T> Proximo { get; set; } = null!;
}

