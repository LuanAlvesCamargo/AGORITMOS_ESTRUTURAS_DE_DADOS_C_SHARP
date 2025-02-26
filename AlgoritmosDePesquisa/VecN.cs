using System;

namespace Algoritmos.Busca.AEstrela {
    // Estrutura de Vetor com N Dimensões (VetorN)  

    // A estrutura `VetorN` representa um vetor matemático de N dimensões, 
    // ou seja, um conjunto ordenado de números reais que 
    // define um ponto ou direção no espaço N-dimensional. 
    // Essa estrutura é útil em diversos contextos, como algoritmos de busca, 
    // computação gráfica, aprendizado de máquina e física computacional.

    // Principais Características da Estrutura VetorN
    // 1. Armazena Componentes:  
    //    O vetor é armazenado internamente como um array (double[]), onde cada posição do array representa uma coordenada 
    //    em uma dimensão específica.

    // 2. Cálculo do Comprimento (Norma do Vetor):  
    //    A função Comprimento() calcula a norma do vetor, que é a distância do ponto representado 
    // pelo vetor até a origem do espaço.  
    //    Sua versão ComprimentoQuadrado() calcula o valor sem a raiz quadrada, economizando processamento quando 
    //    não é necessário o valor exato.

    // 3. Cálculo da Distância entre Vetores:  
    //    Distancia(VetorN outro) calcula a distância euclidiana entre dois vetores.  
    //    DistanciaQuadrada(VetorN outro) retorna a distância ao quadrado, 
    //    sendo útil para comparar distâncias sem o custo computacional da raiz quadrada.

    // 4. Operações entre Vetores:
    //    Subtrair(VetorN outro) permite subtrair um
    public struct VetorN : IEquatable<VetorN> {
        private readonly double[] componentes;

        // Inicializa uma nova instância da estrutura 
        // Componentes do vetor como um array.
        public VetorN(params double[] valores) => componentes = valores;
        // Obtém a quantidade de dimensões deste vetor.
        public int Dimensao => componentes.Length;

        // Retorna o quadrado do comprimento do vetor.
        // O comprimento ao quadrado do vetor.
        public double ComprimentoQuadrado() {
            double soma = 0;
            for (var i = 0; i < componentes.Length; i++) {
                soma += componentes[i] * componentes[i];
            }
            return soma;
        }

        // Retorna o comprimento do vetor.
        // O comprimento do vetor.
        public double Comprimento() => Math.Sqrt(ComprimentoQuadrado());

        public double Distancia(VetorN outro) {
            var diferenca = Subtrair(outro);
            return diferenca.Comprimento();
        }
        public double DistanciaQuadrada(VetorN outro) {
            var diferenca = Subtrair(outro);
            return diferenca.ComprimentoQuadrado();
        }

        public VetorN Subtrair(VetorN outro) {
            var resultado = new double[Math.Max(componentes.Length, outro.componentes.Length)];
            for (var i = 0; i < resultado.Length; i++) {
                double valor = 0;
                if (componentes.Length > i) {
                    valor = componentes[i];
                }

                if (outro.componentes.Length > i) {
                    valor -= outro.componentes[i];
                }

                resultado[i] = valor;
            }
            return new VetorN(resultado);
        }
        // Compara este vetor com outro vetor.
        
        public bool Equals(VetorN outro) {
            if (outro.Dimensao != Dimensao) {
                return false;
            }

            for (var i = 0; i < outro.componentes.Length; i++) {
                if (Math.Abs(componentes[i] - outro.componentes[i]) > 0.000001) {
                    return false;
                }
            }
            return true;
        }
    }
}
