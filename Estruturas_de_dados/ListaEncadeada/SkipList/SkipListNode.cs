using System.Diagnostics;

namespace EstruturasDeDados.ListaLigada.ListaPular;

[DebuggerDisplay("Chave = {Chave}, Altura = {Altura}, Valor = {Valor}")]
internal class NoListaPular<TValor> {
    public NoListaPular(int chave, TVAlor? valor, int altura)
    {
        Chave = chave;
        Valor = valor;
        Altura = altura;
        Proximo = new NoListaPular<TValor>[altura];
    }
    public int Chave { get; }
    public TVAlor? Valor { get; set; }
    public NoListaPular<TValor>[] Proximo { get; }
    public int Altura { get; }
}