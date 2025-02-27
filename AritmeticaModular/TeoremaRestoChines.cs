using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algoritmos.AritmeticaModular {
    //  Implementação do Teorema do Resto Chinês.
    //  teorema permite encontrar um número x que satisfaça um conjunto de congruências modulares.

    public static class TeoremaRestoChines {
        
        public static long Calcular(List<long> listaAs, List<long> listaNs) {
            // Verifica se os requisitos do teorema são atendidos
            VerificarRequisitos(listaAs, listaNs);

            // Calcula o produto de todos os n_i
            var produtoN = 1L;
            foreach (var n in listaNs) {
                produtoN *= n;
            }

            var resultado = 0L;
            for (var i = 0; i < listaNs.Count; i++) {
                var a_i = listaAs[i];
                var n_i = listaNs[i];
                var modulo_i = produtoN / n_i;
                
                // Calcula o coeficiente de Bézout usando o Algoritmo de Euclides Estendido
                var bezout_modulo_i = AlgoritmoEuclidianoEstendido.Calcular(n_i, modulo_i).BezoutB;
                resultado += a_i * bezout_modulo_i * modulo_i;
            }

            // Garante que o resultado esteja no intervalo [0, produtoN)
            resultado %= produtoN;
            if (resultado < 0) {
                resultado += produtoN;
            }

            return resultado;
        }

        public static BigInteger Calcular(List<BigInteger> listaAs, List<BigInteger> listaNs) {
            VerificarRequisitos(listaAs, listaNs);

            var produtoN = BigInteger.One;
            foreach (var n in listaNs) {
                produtoN *= n;
            }

            var resultado = BigInteger.Zero;
            for (var i = 0; i < listaNs.Count; i++) {
                var a_i = listaAs[i];
                var n_i = listaNs[i];
                var modulo_i = produtoN / n_i;
                
                var bezout_modulo_i = AlgoritmoEuclidianoEstendido.Calcular(n_i, modulo_i).BezoutB;
                resultado += a_i * bezout_modulo_i * modulo_i;
            }

            resultado %= produtoN;
            if (resultado < 0) {
                resultado += produtoN;
            }

            return resultado;
        }

        private static void VerificarRequisitos<T>(List<T> listaAs, List<T> listaNs) where T : struct, IComparable<T> {
            if (listaAs == null || listaNs == null || listaAs.Count != listaNs.Count) {
                throw new ArgumentException("As listas de valores 'listaAs' e 'listaNs' não podem ser nulas e devem ter o mesmo tamanho.");
            }

            if (listaNs.Any(x => x.CompareTo((T)Convert.ChangeType(1, typeof(T))) <= 0)) {
                throw new ArgumentException("Todos os valores de 'listaNs' devem ser maiores que 1.");
            }

            if (listaAs.Any(x => x.CompareTo(default(T)) < 0)) {
                throw new ArgumentException("Os valores de 'listaAs' não podem ser negativos.");
            }

            // Verifica se todos os valores de 'listaNs' são coprimos entre si
            for (var i = 0; i < listaNs.Count; i++) {
                for (var j = i + 1; j < listaNs.Count; j++) {
                    var mdc = AlgoritmoEuclidianoEstendido.Calcular(listaNs[i], listaNs[j]).Mdc;
                    if (mdc.CompareTo((T)Convert.ChangeType(1, typeof(T))) != 0)
                    {
                        throw new ArgumentException($"Os valores {listaNs[i]} e {listaNs[j]} não são coprimos (MDC = {mdc}).");
                    }
                }
            }
        }
    }
}