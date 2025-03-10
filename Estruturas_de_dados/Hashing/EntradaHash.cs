using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Hashing.NumberTheory;

namespace DataStructures.Hashing;


// Entrada na tabela hash.
// Esta classe é usada para armazenar os pares chave-valor na tabela hash.

public class Entrada<TChave, TValor> {
    public TChave? Chave { get; set; }

    public TValor? Valor { get; set; }

    public Entrada(TChave chave, TValor valor) {
        Chave = chave;
        Valor = valor;
    }
}

