namespace Algoritmos.Strings.Similaridade;

public class DistanciaJaccard {
    private readonly SimilaridadeJaccard similaridadeJaccard = new();
    public double Calcular(string esquerda, string direita) {
        return 1.0 - similaridadeJaccard.Calcular(esquerda, direita);
    }
}