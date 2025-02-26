using System.Collections.Generic;

namespace Algoritmos.Busca.AEstrela;

// Implementação do algoritmo de busca A* (A Estrela).

public static class AEstrela { 
    
    public static void ResetarNos(List<No> nos) {
        foreach (var no in nos) {
            no.CustoAtual = 0;
            no.CustoEstimado = 0;
            no.Pai = null;
            no.Estado = EstadoNo.NaoConsiderado;
        }
    }
    public static List<No> GerarCaminho(No destino) {
        var caminho = new List<No>();
        var atual = destino;
        while (!(atual is null)) {
            caminho.Add(atual);
            atual = atual.Pai;
        }

        caminho.Reverse();
        return caminho;
    }

    public static List<No> Calcular(No origem, No destino) {
        var finalizados = new List<No>();

        // Fila de prioridade que ordena os nós com base no custo total estimado
        var abertos = new FilaPrioridade<No>();
        foreach (var no in origem.NosConectados) {
            // Adiciona nós conectados se forem transitáveis
            if (no.Transitavel) s{
                // Calcula os custos
                no.CustoAtual = origem.CustoAtual + origem.DistanciaPara(no) * no.MultiplicadorCustoTransito;
                no.CustoEstimado = origem.CustoAtual + no.DistanciaPara(destino);

                // Adiciona à fila de prioridade
                abertos.Enqueue(no);
            }
        } 
        
        while (true) {
            // Condição de término: Caminho não encontrado
            if (abertos.Count == 0) {
                ResetarNos(finalizados);
                ResetarNos(abertos.ObterDados());
                return new List<No>();
            }

            // Seleciona o próximo nó na fila
            var atual = abertos.Dequeue();

            // Adiciona à lista de nós processados
            finalizados.Add(atual);
            atual.Estado = EstadoNo.Fechado;

            // Condição de término: Caminho encontrado
            if (atual == destino) {
                var caminho = GerarCaminho(destino);

                // Reseta os nós utilizados
                ResetarNos(finalizados);
                ResetarNos(abertos.ObterDados());
                return caminho;
            }

            AdicionarOuAtualizarNosConectados(atual, destino, abertos);
        }
    }

    private static void AdicionarOuAtualizarNosConectados(No atual, No destino, FilaPrioridade<No> fila) {
        foreach (var conectado in atual.NosConectados) {
            if (!conectado.Transitavel || conectado.Estado == EstadoNo.Fechado) {
                continue; // Ignora nós já processados ou não transitáveis.
            }

            // Adiciona um nó não considerado anteriormente na fila
            if (conectado.Estado == EstadoNo.NaoConsiderado) {
                conectado.Pai = atual;
                conectado.CustoAtual = atual.CustoAtual + atual.DistanciaPara(conectado) * conectado.MultiplicadorCustoTransito;
                conectado.CustoEstimado = conectado.CustoAtual + conectado.DistanciaPara(destino);
                conectado.Estado = EstadoNo.Aberto;
                fila.Enqueue(conectado);
            } else if (atual != conectado) {
                // Atualiza o custo do nó se o novo caminho for mais curto
                var novoCustoAtual = atual.CustoAtual + atual.DistanciaPara(conectado);
                var novoCustoTotal = novoCustoAtual + atual.CustoEstimado;
                if (novoCustoTotal < conectado.CustoTotal) {
                    conectado.Pai = atual;
                    conectado.CustoAtual = novoCustoAtual;
                }
            } else {
                // Tratamento para evitar nós duplicados
                throw new ExcecaoCaminho("O mesmo nó foi detectado duas vezes. Isso não deveria acontecer.");
            }
        }
    }
}
