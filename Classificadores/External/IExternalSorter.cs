using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Externa;

public interface IOrdenadorExterno<T> {
// Interface para ordenadores externos.
// Ordenadores externos são usados quando o conjunto de dados a ser ordenado é muito grande
// para caber na memória principal.
    void Ordenar(IArmazenamentoSequencial<T> memoriaPrincipal, IArmazenamentoSequencial<T> memoriaTemporaria, IComparer<T> comparador);
}

public interface IArmazenamentoSequencial<T> {
    T LerProximo();
    void Escrever(T elemento);
    void ReiniciarLeitura();
    void ReiniciarEscrita();
    bool EstaVazio();
    int ObterTamanho();
}