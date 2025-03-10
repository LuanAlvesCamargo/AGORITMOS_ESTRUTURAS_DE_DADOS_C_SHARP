using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataStructures.LinkedList.SkipList;

[DebuggerDisplay("Contagem = {Contagem}")]
public class SkipList<TValor> {
    private const double Probabilidade = 0.5;
    private readonly int niveisMaximos;
    private readonly SkipListNode<TValor> cabeca;
    private readonly SkipListNode<TValor> cauda;
    private readonly Random aleatorio = new Random();

    public SkipList(int capacidade = 255) {
        niveisMaximos = (int)Math.Log2(capacidade) + 1;
        cabeca = new(int.MinValue, default(TValor), niveisMaximos);
        cauda = new(int.MaxValue, default(TValor), niveisMaximos);
        for(int i = 0; i < niveisMaximos; i++) {
            cabeca.Proximo[i] = cauda;
        }
    }

    public int Contagem { get; private set; }

    public TValor this[int chave] {
        get {
            var noAnterior = ObterSkipNodes(chave).First();
            if(noAnterior.Proximo[0].Chave == chave) {
                return noAnterior.Proximo[0].Valor!;
            } else {
                throw new KeyNotFoundException();
            }
        }
        set => AdicionarOuAtualizar(chave, value);
    }

    public void AdicionarOuAtualizar(int chave, TValor valor) {
        var skipNodes = ObterSkipNodes(chave);
        var noAnterior = skipNodes.First();
        if (noAnterior.Proximo[0].Chave == chave) {
            noAnterior.Proximo[0].Valor = valor;
            return;
        }
        var novoNo = new SkipListNode<TValor>(chave, valor, ObterAlturaAleatoria());
        for (var nivel = 0; nivel < novoNo.Altura; nivel++) {
            novoNo.Proximo[nivel] = skipNodes[nivel].Proximo[nivel];
            skipNodes[nivel].Proximo[nivel] = novoNo;
        }
        Contagem++;
    }

    public bool Contem(int chave) {
        var noAnterior = ObterSkipNodes(chave).First();
        return noAnterior.Proximo[0].Chave == chave;
    }

    public bool Remover(int chave) {
        var skipNodes = ObterSkipNodes(chave);
        var noAnterior = skipNodes.First();
        if (noAnterior.Proximo[0].Chave != chave) {
            return false;
        }
        var noParaRemover = noAnterior.Proximo[0];
        for (var nivel = 0; nivel < noParaRemover.Altura; nivel++) {
            skipNodes[nivel].Proximo[nivel] = noParaRemover.Proximo[nivel];
        }
        Contagem--;
        return true;
    }

    public IEnumerable<TValor> ObterValores() {
        var atual = cabeca.Proximo[0];
        while (atual.Chave != cauda.Chave) {
            yield return atual.Valor!;
            atual = atual.Proximo[0];
        }
    }

    private SkipListNode<TValor>[] ObterSkipNodes(int chave) {
        var skipNodes = new SkipListNode<TValor>[niveisMaximos];
        var atual = cabeca;
        for (var nivel = cabeca.Altura - 1; nivel >= 0; nivel--) {
            while (atual.Proximo[nivel].Chave < chave) {
                atual = atual.Proximo[nivel];
            }
            skipNodes[nivel] = atual;
        }
        return skipNodes;
    }

    private int ObterAlturaAleatoria() {
        int altura = 1;
        while (aleatorio.NextDouble() < Probabilidade && altura < niveisMaximos) {
            altura++;
        }
        return altura;
    }
}

public class SkipListNode<TValor>{
    public SkipListNode(int chave, TValor valor, int altura) {
        Chave = chave;
        Valor = valor;
        Altura = altura;
        Proximo = new SkipListNode<TValor>[altura];
    }

    public int Chave { get; }
    public TValor Valor { get; set; }
    public int Altura { get; }
    public SkipListNode<TValor>[] Proximo { get; }
}