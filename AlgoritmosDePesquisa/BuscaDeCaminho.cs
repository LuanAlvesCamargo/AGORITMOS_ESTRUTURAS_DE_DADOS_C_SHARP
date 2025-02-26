using System;

namespace Algoritmos.Busca.AEstrela;

    // Exceção de busca de caminho lançada quando ocorre um erro crítico
    // que impede a continuidade do algoritmo de pathfinding.

public class ExcecaoBuscaCaminho : Exception {
    // Construtor da exceção que recebe uma mensagem de erro.
    public ExcecaoBuscaCaminho(string mensagem)
        : base(mensagem) {
    }
}