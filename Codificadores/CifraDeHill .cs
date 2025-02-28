using System;
using System.Linq;
using Algorithms.Numeric;

namespace Algorithms.Criptografia {   
    // Cifra de substituição poligráfica de Lester S. Hill, 
    // sem representar letras usando mod26, mas utilizando
    // diretamente os valores Unicode correspondentes.

    public class CifraHill : ICodificador<double[,]> {
        private readonly EliminacaoGaussJordan resolvedorEquacaoLinear;

        public CifraHill() => resolvedorEquacaoLinear = new EliminacaoGaussJordan();

        public string Codificar(string texto, double[,] chave) {
            var textoPreparado = PreencherEspacos(texto);
            var segmentos = SegmentarTexto(textoPreparado);
            var caracteresSeparados = SepararCaracteres(segmentos);

            var textoCifrado = new double[segmentos.Length][];

            for (var i = 0; i < segmentos.Length; i++) {
                var vetor = new double[3];
                Array.Copy(caracteresSeparados, i * 3, vetor, 0, 3);
                var produto = CifrarMatriz(vetor, chave);
                textoCifrado[i] = produto;
            }

            var textoFinal = MesclarSegmentos(textoCifrado);

            return ConstruirTextoAPartirDeArray(textoFinal);
        }

        public string Decodificar(string texto, double[,] chave) {
            var segmentos = SegmentarTexto(texto);
            var caracteresSeparados = SepararCaracteres(segmentos);

            var textoDecifrado = new double[segmentos.Length][];

            for (var i = 0; i < segmentos.Length; i++) {
                var vetor = new double[3];
                Array.Copy(caracteresSeparados, i * 3, vetor, 0, 3);
                var produto = DecifrarMatriz(vetor, chave);
                textoDecifrado[i] = produto;
            }

            var textoFinal = MesclarSegmentos(textoDecifrado);
            var resultado = ConstruirTextoAPartirDeArray(textoFinal);

            return RemoverEspacosExtras(resultado);
        }

        private static string ConstruirTextoAPartirDeArray(double[] arr) => new(arr.Select(c => (char)c).ToArray());

        private static double[] CifrarMatriz(double[] vetor, double[,] chave) {
            var resultado = new double[vetor.Length];

            for (var i = 0; i < chave.GetLength(1); i++) {
                for (var j = 0; j < chave.GetLength(0); j++) {
                    resultado[i] += chave[i, j] * vetor[j];
                }
            }

            return resultado;
        }

        private static double[] MesclarSegmentos(double[][] lista) {
            var resultado = new double[lista.Length * 3];

            for (var i = 0; i < lista.Length; i++) {
                Array.Copy(lista[i], 0, resultado, i * 3, lista[0].Length);
            }

            return resultado;
        }

        private static char[] SepararCaracteres(string[] segmentos) {
            var resultado = new char[segmentos.Length * 3];

            for (var i = 0; i < segmentos.Length; i++) {
                for (var j = 0; j < 3; j++) {
                    resultado[i * 3 + j] = segmentos[i].ToCharArray()[j];
                }
            }

            return resultado;
        }

        private static string[] SegmentarTexto(string texto) {
            var qtdSegmentos = texto.Length / 3;
            var segmentos = new string[qtdSegmentos];

            for (var i = 0; i < qtdSegmentos; i++) {
                segmentos.SetValue(texto.Substring(i * 3, 3), i);
            }

            return segmentos;
        }

        private static string PreencherEspacos(string texto) {
            var resto = texto.Length % 3;
            return resto == 0 ? texto : texto + new string(' ', 3 - resto);
        }

        private static string RemoverEspacosExtras(string texto) => texto.TrimEnd();

        private double[] DecifrarMatriz(double[] vetor, double[,] chave) {
            var matrizAumentada = new double[3, 4];

            for (var i = 0; i < chave.GetLength(0); i++) {
                for (var j = 0; j < chave.GetLength(1); j++) {
                    matrizAumentada[i, j] = chave[i, j];
                }
            }

            for (var k = 0; k < vetor.Length; k++) {
                matrizAumentada[k, 3] = vetor[k];
            }

            _ = resolvedorEquacaoLinear.Resolver(matrizAumentada);

            return new[] { matrizAumentada[0, 3], matrizAumentada[1, 3], matrizAumentada[2, 3] };
        }
    }
}
