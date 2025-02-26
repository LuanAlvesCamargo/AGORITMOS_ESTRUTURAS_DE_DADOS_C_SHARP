using System;

namespace Algoritmos.Busca.AEstrela {

    // Representa um nó em um grafo para algoritmos de busca, contendo informações sobre posição, custo e conexões.
    // Um nó (node) de rede é um ponto de uma rede onde é possível criar, receber ou transmitir mensagens. 
    // Em uma estrutura de blockchain, é a unidade básica que armazena dados e permite que a comunicação entre os nós ocorra. 
    // Node.js é um ambiente de execução JavaScript que permite executar código JavaScript no lado do servidor. 
    // Ele é multiplataforma e de código aberto. 
    // O Node.js é uma plataforma que permite criar aplicações web complexas, APIs simples e sistemas em tempo real. 
    // Ele é baseado no motor V8 do Google Chrome. 
    // O Node.js é uma plataforma de aplicação, na qual os programas são escritos em JavaScript e compilados, 
    // otimizados e interpretados pela máquina virtual V8. 
    // O Node.js é non-blocking (sem bloqueio), o que significa que todas as funções (retornos de chamada) 
    // são delegadas ao loop de eventos. 
    // O Node.js foi criado em 2009 pelo engenheiro de software Ryan Dahl. 

    // Custo total do nó.
    // Soma do custo atual e do custo estimado até o destino.
    public class No : IComparable<No>, IEquatable<No> {
        public No(VecN posicao, bool atravessavel, double multiplicadorCusto) {
            Atravessavel = atravessavel;
            Posicao = posicao;
            MultiplicadorCusto = multiplicadorCusto;
        }

        public double CustoTotal => CustoEstimado + CustoAtual;
            // Estimativa da distância entre este nó e o nó de destino.

        public double CustoEstimado { get; set; }

            // Multiplicador de custo para atravessar este nó.
            // Indica o quão difícil é transitar por ele.
        public double MultiplicadorCusto { get; }

            // Custo acumulado para chegar até este nó a partir do nó inicial.
        public double CustoAtual { get; set; }

            // Estado do nó no processo de busca:
            // Não considerado (padrão), aberto ou fechado.
        public EstadoNo Estado { get; set; }

            // Indica se o nó pode ser atravessado.
        public bool Atravessavel { get; }

            // Lista de nós conectados a este nó.
        public No[] NosConectados { get; set; } = new No[0];

            // Nó pai, ou seja, o nó processado anteriormente no caminho.
        public No? Pai { get; set; }

            // Posição do nó no espaço.
        public VecN Posicao { get; }

            // Compara dois nós com base em seus custos totais.
            // Utilizado para definir prioridades em algoritmos de busca como A*.
            // O outro nó para comparar.
            // Resultado da comparação de custos.
        public int CompareTo(No? outro) => CustoTotal.CompareTo(outro?.CustoTotal ?? 0);

        public bool Equals(No? outro) => CompareTo(outro) == 0;

        public static bool operator ==(No esquerdo, No direito) => esquerdo?.Equals(direito) != false;

        public static bool operator >(No esquerdo, No direito) => esquerdo.CompareTo(direito) > 0;

        public static bool operator <(No esquerdo, No direito) => esquerdo.CompareTo(direito) < 0;

        public static bool operator !=(No esquerdo, No direito) => !(esquerdo == direito);

        public static bool operator <=(No esquerdo, No direito) => esquerdo.CompareTo(direito) <= 0;

        public static bool operator >=(No esquerdo, No direito) => esquerdo.CompareTo(direito) >= 0;

        public override bool Equals(object? obj) => obj is No outro && Equals(outro);

        public override int GetHashCode() =>
            Posicao.GetHashCode()
            + Atravessavel.GetHashCode()
            + MultiplicadorCusto.GetHashCode();

            // Calcula a distância até outro nó.
            // O outro nó.
            // Distância entre este nó e o nó informado.
        public double DistanciaPara(No outro) => Math.Sqrt(Posicao.SqrDistance(outro.Posicao));
    }
}
