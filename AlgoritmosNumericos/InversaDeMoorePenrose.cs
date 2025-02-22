using System;
using Utilities.Extensions;

namespace Algoritmos.Numericos;

// A pseudo-inversa de Moore-Penrose A+ de uma matriz A
// é um método geral para encontrar a solução do seguinte sistema de equações lineares:
// ~b = A ~y, onde ~b pertence a R^m, ~y pertence a R^n e A pertence a Rm×n.
// Existem vários métodos para calcular a pseudo-inversa.
// Este método utiliza a Decomposição em Valores Singulares (SVD).
// Em matemática, e em particular em álgebra linear, a matriz inversa de Moore-Penrose de uma matriz 
// é a generalização mais conhecida da matriz inversa. Ela foi descrita independentemente por EH Moore em 1920, 
// Arne Bjerhammar em 1951 e Roger Penrose em 1955.
public static class PseudoInversa
{

// Retorna a pseudo-inversa de uma matriz com base no Algoritmo de Moore-Penrose,
// utilizando a Decomposição em Valores Singulares (SVD).
// Matriz de entrada para calcular sua pseudo-inversa.
// Aproximação da matriz inversa da matriz de entrada.

    public static double[,] CalcularPseudoInversa(double[,] matrizEntrada) {
        // Decompor a matriz de entrada utilizando SVD para obter U, Σ e V.
        var (u, s, v) = ThinSvd.Decompose(matrizEntrada);

        // Calcular o inverso de cada elemento não nulo da diagonal de Σ.
        var tamanho = s.Length;
        var sigmaInversa = new double[tamanho];

        for (var i = 0; i < tamanho; i++) {
            sigmaInversa[i] = Math.Abs(s[i]) < 0.0001 ? 0 : 1 / s[i];
        }

        // Criar uma matriz diagonal a partir do vetor resultante.
        var matrizDiagonal = sigmaInversa.ToDiagonalMatrix();

        // Construir a pseudo-inversa utilizando as matrizes obtidas.
        var matrizInversa = u.Multiply(matrizDiagonal).Multiply(v.Transpose());

        // Retornar a transposta da matriz resultante.
        return matrizInversa.Transpose();
    }
}
