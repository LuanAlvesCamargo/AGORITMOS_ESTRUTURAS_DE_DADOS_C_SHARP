namespace Algoritmos.Embaralhadores;

// Interface para embaralhadores de array.
public interface IEmbaralhador<in T> {
    void Embaralhar(T[] array, int? semente = null);
}