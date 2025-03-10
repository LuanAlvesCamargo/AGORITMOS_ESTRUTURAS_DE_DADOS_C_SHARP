namespace EstruturasDeDados.Fenwick;

    // Representa a implementação clássica da Árvore de Fenwick ou Árvore Binária Indexada.
    // arvoreFenwick[0..n] --> Array que representa a Árvore Binária Indexada.
    // arrayOriginal[0..n-1] --> Array de entrada para o qual a soma de prefixos é calculada.
public class ArvoreBinariaIndexada {
    private readonly int[] arvoreFenwick;  

    // Inicializa uma nova instância da classe
    // Cria a Árvore Binária Indexada a partir do array fornecido.
    public ArvoreBinariaIndexada(int[] arrayOriginal) {
        arvoreFenwick = new int[arrayOriginal.Length + 1];
        for (var i = 0; i < arrayOriginal.Length; i++) {
            AtualizarArvore(i, arrayOriginal[i]);
        }
    }
    public int ObterSoma(int indice) {
        var soma = 0;
        var comecarDe = indice + 1;

        while (comecarDe > 0) {
            soma += arvoreFenwick[comecarDe];
            comecarDe -= comecarDe & (-comecarDe);
        }

        return soma;
    }

    public void AtualizarArvore(int indice, int valor) {
        var comecarDe = indice + 1;

        while (comecarDe <= arvoreFenwick.Length) {
            arvoreFenwick[comecarDe] += valor;
            comecarDe += comecarDe & (-comecarDe);
        }
    }
}