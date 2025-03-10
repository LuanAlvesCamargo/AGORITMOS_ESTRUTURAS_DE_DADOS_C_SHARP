namespace EstruturasDeDados.ArvoreDeBuscaBinaria;

/// <summary>
///     Classe genérica de nó para Árvore de Busca Binária.
///     As chaves para cada nó são imutáveis.
/// </summary>
/// <typeparam name="TChave">Tipo da chave para o nó. As chaves devem implementar IComparable.</typeparam>
public class NoArvoreBuscaBinaria<TChave>
{
    /// <summary>
    ///     Inicializa uma nova instância da classe <see cref="NoArvoreBuscaBinaria{TChave}" />.
    /// </summary>
    /// <param name="chave">A chave deste nó.</param>
    public NoArvoreBuscaBinaria(TChave chave) => Chave = chave;

    /// <summary>
    ///     Obtém a chave deste nó.
    /// </summary>
    public TChave Chave { get; }

    /// <summary>
    ///     Obtém ou define a referência para um nó filho que precede/vem antes deste nó.
    /// </summary>
    public NoArvoreBuscaBinaria<TChave>? Esquerda { get; set; }

    /// <summary>
    ///     Obtém ou define a referência para um nó filho que segue/vem depois deste nó.
    /// </summary>
    public NoArvoreBuscaBinaria<TChave>? Direita { get; set; }
}