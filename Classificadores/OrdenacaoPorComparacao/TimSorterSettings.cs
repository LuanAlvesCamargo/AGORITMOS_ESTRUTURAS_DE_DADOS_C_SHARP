namespace Algoritmos.Ordenacao.Comparacao;

    // Classe que representa as configurações do algoritmo Tim Sort.
    // Estas configurações controlam o comportamento do algoritmo,
    // permitindo ajustar o desempenho para diferentes tipos de dados.
public class ConfiguracoesTimSort {
    public int TamanhoMinimoMescla { get; }
    public int LimiteMinimoGalope { get; }
    public ConfiguracoesTimSort(int tamanhoMinimoMescla = 32, int limiteMinimoGalope = 7) {
        TamanhoMinimoMescla = tamanhoMinimoMescla;
        LimiteMinimoGalope = limiteMinimoGalope;
    }
}