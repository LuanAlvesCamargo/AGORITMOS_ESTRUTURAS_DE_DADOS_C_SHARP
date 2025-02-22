using System;

namespace Algoritmos.Numericos;

    // Um quadrado perfeito é um número que resulta do quadrado de outro número inteiro.
    // Número quadrado ou quadrado perfeito, em matemática, é um inteiro que pode ser escrito 
    // como o quadrado de outro número inteiro. Ou ainda se a raiz quadrada de um número inteiro for outro inteiro, 
    // o primeiro é um número quadrado ou quadrado perfeito.
public static class VerificadorQuadradoPerfeito {
    public static bool EhQuadradoPerfeito(int numero) {
        if (numero < 0) {
            return false;
        }

        var raiz = (int)Math.Sqrt(numero);
        return raiz * raiz == numero;
    }
}
