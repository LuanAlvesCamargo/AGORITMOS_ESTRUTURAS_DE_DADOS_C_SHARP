namespace EstruturasDeDados.ListaEndadeada.Simples;

// Representa um nó em uma lista endadeada simples.


public class NoListaLigadaSimples<T> {
   public NoListaLigadaSimples(T dados) {
        Dados = dados;
        Proximo = null;
    }
    public T Dados { get; }
    public NoListaLigadaSimples<T>? Proximo { get; set; }
}