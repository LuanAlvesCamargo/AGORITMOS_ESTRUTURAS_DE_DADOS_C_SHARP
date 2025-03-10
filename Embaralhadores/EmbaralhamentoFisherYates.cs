using System;

namespace Algoritmos.Embaralhadores;

// O embaralhamento Fisher-Yates é um algoritmo de embaralhamento simples,
// geralmente usado para embaralhar um baralho de cartas.
public class EmbaralhadorFisherYates<T> : IEmbaralhador<T> {
    public void Embaralhar(T[] array, int? semente = null) {
        var aleatorio = semente is null ? new Random() : new Random(semente.Value);

        for (var i = array.Length - 1; i > 0; i--) {
            var j = aleatorio.Next(0, i + 1);

            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
public interface IEmbaralhador<T> {
    void Embaralhar(T[] array, int? semente = null);
}

