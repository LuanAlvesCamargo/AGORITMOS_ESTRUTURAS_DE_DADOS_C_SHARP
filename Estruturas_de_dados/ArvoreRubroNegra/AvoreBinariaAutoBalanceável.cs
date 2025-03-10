using System;
using System.Collections.Generic;

namespace EstruturasDeDados.ArvoreRubroNegra;

// Uma árvore binária auto-balanceável. 
// Uma árvore rubro-negra é uma árvore de busca binária (BST) auto-balanceável que
// armazena uma cor com cada nó. A cor de um nó pode ser vermelha ou
// preta. Várias propriedades são mantidas para garantir que a árvore permaneça balanceada.
// <term>Um nó vermelho não tem um filho vermelho.
// Todos os nós nulos são considerados pretos.
// Todo caminho de um nó para seus nós folha descendentes
// tem o mesmo número de nós pretos.
// (Opcional) A raiz é sempre preta.
// Árvores rubro-negras são geralmente ligeiramente mais desbalanceadas do que uma
// árvore AVL, mas a inserção e exclusão são geralmente mais rápidas.

public class ArvoreRubroNegra<TChave> {
    // Obtém o número de nós na árvore.
    public int Contagem { get; private set; }
    // Comparador a ser usado ao comparar valores de chave.
    private readonly Comparer<TChave> comparador;
    // Referência ao nó raiz.
    private NoRubroNegro<TChave>? raiz;
    public ArvoreRubroNegra() {
        comparador = Comparer<TChave>.Default;
    }
    // Inicializa uma nova instância da classe <see cref="ArvoreRubroNegra{TChave}"/>
    // usando o comparador especificado.
    public ArvoreRubroNegra(Comparer<TChave> comparadorPersonalizado) {
        comparador = comparadorPersonalizado;
    }
    // Adiciona um único nó à árvore.
    public void Adicionar(TChave chave) {
        if (raiz is null) {
            // Caso 3
            // Novo nó é a raiz
            raiz = new NoRubroNegro<TChave>(chave, null) {
                Cor = CorNo.Preto,
            };
            Contagem++;
            return;
        }
        // Inserção regular de árvore binária
        var no = Adicionar(raiz, chave);
        // Obtém qual lado o filho foi adicionado
        var direcaoFilho = comparador.Compare(no.Chave, no.Pai!.Chave);
        // Define o nó como o pai do novo nó para facilitar o manuseio
        no = no.Pai;
        // Retorna a árvore a um estado válido
        int casoAdicao;
        do {
            casoAdicao = ObterCasoAdicao(no);

            switch(casoAdicao) {
                case 1:
                    break;
                case 2:
                    var paiAntigo = no.Pai;
                    no = AdicionarCaso2(no);

                    if (no is not null) {
                        direcaoFilho = comparador.Compare(paiAntigo!.Chave, paiAntigo.Pai!.Chave);
                    }
                    break;
                case 4:
                    no.Cor = CorNo.Preto;
                    break;
                case 56:
                    AdicionarCaso56(no, comparador.Compare(no.Chave, no.Pai!.Chave), direcaoFilho);
                    break;
                default:
                    throw new InvalidOperationException("Não deveria ser possível chegar aqui!");
            }
        } while (casoAdicao == 2 && no is not null);
        Contagem++;
    }
    // Adiciona vários nós à árvore.
    public void AdicionarIntervalo(IEnumerable<TChave> chaves) {
        foreach (var chave in chaves) {
            Adicionar(chave);
        }
    }
    // Remove um nó da árvore.
    public void Remover(TChave chave) {
        // Busca o nó
        var no = Remover(raiz, chave);
        // Casos simples
        no = RemoverCasosSimples(no);
        // Sai se o nó excluído não era uma folha preta não raiz
        if (no is null) {
            return;
        }
        // Exclui o nó
        ExcluirFolha(no.Pai!, comparador.Compare(no.Chave, no.Pai!.Chave));
        // Recolorir árvore
        do {
            no = RemoverRecolorir(no);
        }
        while (no is not null && no.Pai is not null);    // Caso 2: Alcançou a raiz
        Contagem--;
    }
    // Verifica se o nó fornecido está na árvore.
    public bool Contem(TChave chave) {
        var no = raiz;
        while (no is not null) {
            var resultadoComparacao = comparador.Compare(chave, no.Chave);
            if (resultadoComparacao < 0) {
                no = no.Esquerda;
            } else if (resultadoComparacao > 0) {
                no = no.Direita;
            } else {
                return true;
            }
        }
        return false;
    }
    // Obtém o valor mínimo na árvore.
    public TChave ObterMinimo() {
        if (raiz is null) {
            throw new InvalidOperationException("Árvore está vazia!");
        }
        return ObterMinimo(raiz).Chave;
    }
    // Obtém o valor máximo na árvore.
    public TChave ObterMaximo() {
        if (raiz is null) {
            throw new InvalidOperationException("Árvore está vazia!");
        }
        return ObterMaximo(raiz).Chave;
    }
    // Obtém as chaves em ordem do menor para o maior, conforme definido pelo comparador.
    // Chaves na árvore em ordem do menor para o maior.
    public IEnumerable<TChave> ObterChavesEmOrdem() {
        var resultado = new List<TChave>();
        CaminharEmOrdem(raiz);
        return resultado;

        void CaminharEmOrdem(NoRubroNegro<TChave>? no) {
            if (no is null) {
                return;
            }
            CaminharEmOrdem(no.Esquerda);
            resultado.Add(no.Chave);
            CaminharEmOrdem
        }
    }
}