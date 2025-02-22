using System;
using System.Linq;

namespace Algoritmos.Numericos {

    // A série de Maclaurin calcula aproximações de funções não lineares
    // a partir do ponto x = 0, representadas como uma soma infinita de potências:
    // f(x) = f(0) + f'(0) * x + ... + (f^(n)(0) * (x^n)) / n! + ...,
    // onde n é um número natural.
    // A série de MacLaurin é um caso específico da série de Taylor. 
    // A série de Taylor é uma maneira de representar funções a partir de uma soma de infinitas parcelas.  
    public static class Maclaurin {

    // Calcula a aproximação da função e^x usando a série de Maclaurin:
    // e^x = 1 + x + x^2 / 2! + ... + x^n / n! + ...
    // Ponto no qual a função será avaliada.
    // Número de termos na aproximação.
    // Valor aproximado da função no ponto dado.
        public static double Exp(double x, int n) => 
            Enumerable.Range(0, n).Sum(i => TermoExp(x, i));

        // Calcula a aproximação da função seno(x) usando a série de Maclaurin:
        // sin(x) = x - x^3 / 3! + ... + (-1)^n * x^(2n + 1) / (2n + 1)! + ...
        // Ponto no qual a função será avaliada.
        // Número de termos na aproximação.
        // Valor aproximado da função no ponto dado.
        public static double Seno(double x, int n) => 
            Enumerable.Range(0, n).Sum(i => TermoSeno(x, i));

        // Calcula a aproximação da função cosseno(x) usando a série de Maclaurin:
        // cos(x) = 1 - x^2 / 2! + ... + (-1)^n * x^(2n) / (2n)! + ...
        // Ponto no qual a função será avaliada.
        // Número de termos na aproximação.
        // Valor aproximado da função no ponto dado.
        public static double Cosseno(double x, int n) =>
            Enumerable.Range(0, n).Sum(i => TermoCosseno(x, i));


        // Calcula a aproximação de e^x com base em um erro máximo permitido.
        // Ponto no qual a função será avaliada.
        // Valor máximo permitido para o erro do último termo.
        // Valor aproximado da função no ponto dado.
        // Se o erro não estiver no intervalo (0.0; 1.0).
        public static double Exp(double x, double erro = 0.00001) => EnvolverErro(x, erro, TermoExp);


        // Calcula a aproximação do seno(x) com base em um erro máximo permitido.
        // Ponto no qual a função será avaliada.
        // Valor máximo permitido para o erro do último termo.
        // Valor aproximado da função no ponto dado.
        public static double Seno(double x, double erro = 0.00001) => EnvolverErro(x, erro, TermoSeno);


        // Calcula a aproximação do cosseno(x) com base em um erro máximo permitido.
        // Ponto no qual a função será avaliada.
        // Valor máximo permitido para o erro do último termo.
        // Valor aproximado da função no ponto dado.
        public static double Cosseno(double x, double erro = 0.00001) => EnvolverErro(x, erro, TermoCosseno);
         
        // Função auxiliar que calcula a aproximação iterando até que o erro seja menor que o permitido.
         
        private static double EnvolverErro(double x, double erro, Func<double, int, double> termo)
        {
            if (erro <= 0.0 || erro >= 1.0)
                throw new ArgumentException("O valor do erro deve estar no intervalo (0.0; 1.0).");

            int i = 0;
            double coeficienteTermo = 0.0;
            double resultado = 0.0;

            do
            {
                resultado += coeficienteTermo;
                coeficienteTermo = termo(x, i);
                i++;
            } while (Math.Abs(coeficienteTermo) > erro);

            return resultado;
        }

         
        // Calcula um termo individual da série de Maclaurin para e^x: x^i / i!.
         
        private static double TermoExp(double x, int i) => Math.Pow(x, i) / (long)Fatorial.Calcular(i);

         
        // Calcula um termo individual da série de Maclaurin para sin(x): (-1)^i * x^(2i + 1) / (2i + 1)!.
         
        private static double TermoSeno(double x, int i) =>
            Math.Pow(-1, i) / ((long)Fatorial.Calcular(2 * i + 1)) * Math.Pow(x, 2 * i + 1);
       
        // Calcula um termo individual da série de Maclaurin para cos(x): (-1)^i * x^(2i) / (2i)!.
         
        private static double TermoCosseno(double x, int i) =>
            Math.Pow(-1, i) / ((long)Fatorial.Calcular(2 * i)) * Math.Pow(x, 2 * i);
    }
}
