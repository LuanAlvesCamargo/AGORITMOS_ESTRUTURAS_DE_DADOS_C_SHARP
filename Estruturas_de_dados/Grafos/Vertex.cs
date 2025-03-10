namespace EstruturasDeDados.Grafo;

// Implementação de um vértice de grafo.

public class Vertice<T> {
    public T Dados { get; }
    public int Indice { get; internal set; }
    public GrafoPonderadoDirecionado<T>? Grafo { get; internal set; }

    public Vertice(T dados, int indice, GrafoPonderadoDirecionado<T>? grafo) {
        Dados = dados;
        Indice = indice;
        Grafo = grafo;
    }

    public Vertice(T dados, int indice) {
        Dados = dados;
        Indice = indice;
    }

    public void DefinirGrafoNulo() => Grafo = null;
    public override string ToString() => $"Dados do Vértice: {Dados}, Índice: {Indice}";
}