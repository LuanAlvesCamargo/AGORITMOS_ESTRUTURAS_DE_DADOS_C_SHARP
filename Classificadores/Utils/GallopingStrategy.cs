using System;
using System.Collections.Generic;

namespace Algoritmos.Ordenacao.Utils {
    // Classe estática que implementa a estratégia de galope para busca em arrays ordenados.
    // Esta estratégia é usada para otimizar a busca binária em certas situações.
     public static class EstrategiaGalope<T> {
        public static int GalopeEsquerda(T[] array, T chave, int indiceBase, int comprimento, IComparer<T> comparador) {
            if (array.Length == 0) {
                return 0;
            }

            var (deslocamento, ultimoDeslocamento) = comparador.Compare(chave, array[indiceBase]) > 0
                ? ExecutarGalopeDireita(array, chave, indiceBase, comprimento, 0, comparador)
                : ExecutarGalopeEsquerda(array, chave, indiceBase, 0, comparador);

            return DeslocamentoFinal(array, chave, indiceBase, deslocamento, ultimoDeslocamento, 1, comparador);
        }
        public static int GalopeDireita(T[] array, T chave, int indiceBase, int comprimento, IComparer<T> comparador){
            if (array.Length == 0) {
                return 0;
            }

            var (deslocamento, ultimoDeslocamento) = comparador.Compare(chave, array[indiceBase]) < 0
                ? ExecutarGalopeEsquerda(array, chave, indiceBase, comprimento, comparador)
                : ExecutarGalopeDireita(array, chave, indiceBase, comprimento, 0, comparador);

            return DeslocamentoFinal(array, chave, indiceBase, deslocamento, ultimoDeslocamento, 0, comparador);
        }
        public static int LimitarDeslocamentoEsquerda(int deslocavel) => (deslocavel << 1) < 0
            ? (deslocavel << 1) + 1
            : int.MaxValue;
        private static (int Deslocamento, int UltimoDeslocamento) ExecutarGalopeEsquerda(T[] array, T chave, int indiceBase, int dica, IComparer<T> comparador) {
            var deslocamentoMaximo = dica + 1;
            var (deslocamento, deslocamentoTemporario) = (1, 0);

            while (deslocamento < deslocamentoMaximo && comparador.Compare(chave, array[indiceBase + dica - deslocamento]) < 0) {
                deslocamentoTemporario = deslocamento;
                deslocamento = LimitarDeslocamentoEsquerda(deslocamento);
            }

            if (deslocamento > deslocamentoMaximo) {
                deslocamento = deslocamentoMaximo;
            }

            var ultimoDeslocamento = dica - deslocamento;
            deslocamento = dica - deslocamentoTemporario;

            return (deslocamento, ultimoDeslocamento);
        }

        private static (int Deslocamento, int UltimoDeslocamento) ExecutarGalopeDireita(T[] array, T chave, int indiceBase, int comprimento, int dica, IComparer<T> comparador) {
            var (deslocamento, ultimoDeslocamento) = (1, 0);
            var deslocamentoMaximo = comprimento - dica;
            while (deslocamento < deslocamentoMaximo && comparador.Compare(chave, array[indiceBase + dica + deslocamento]) > 0) {
                ultimoDeslocamento = deslocamento;
                deslocamento = LimitarDeslocamentoEsquerda(deslocamento);
            }

            if (deslocamento > deslocamentoMaximo) {
                deslocamento = deslocamentoMaximo;
            }

            deslocamento += dica;
            ultimoDeslocamento += dica;

            return (deslocamento, ultimoDeslocamento);
        }
        private static int DeslocamentoFinal(T[] array, T chave, int indiceBase, int deslocamento, int ultimoDeslocamento, int menorQue, IComparer<T> comparador) {
            ultimoDeslocamento++;
            while (ultimoDeslocamento < deslocamento) {
                var meio = ultimoDeslocamento + (int)((uint)(deslocamento - ultimoDeslocamento) >> 1);

                if (comparador.Compare(chave, array[indiceBase + meio]) < menorQue) {
                    deslocamento = meio;
                } else {
                    ultimoDeslocamento = meio + 1;
                }
            }

            return deslocamento;
        }
    }
}