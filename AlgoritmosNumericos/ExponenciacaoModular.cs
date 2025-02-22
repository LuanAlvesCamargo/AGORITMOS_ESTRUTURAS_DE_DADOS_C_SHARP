using System;

namespace Algoritmos.Numericos {
    // A Exponenciação Modular é um tipo de exponenciação realizada sobre um módulo.
    // O cálculo segue a fórmula: resultado = base^expoente mod modulo,
    // onde "base" é a base, "expoente" é o expoente e "modulo" é o módulo.
    // A exponenciação modular significa que estamos calculando potências em aritmética modular, ou seja, 
    // realizando uma operação da forma ab mod n, em que a, b e n são números inteiros. Se b for negativo, 
    // a exponenciação modular estará vinculada aos inversos multiplicativos modulares.
    public class ExponenciacaoModular {
        public int PotenciaModular(int baseNum, int expoente, int modulo) {
            // Inicializa a variável que armazenará o resultado
            int resultado = 1;
            
            if (modulo == 1) {
                // Qualquer número mod 1 é sempre 0
                return 0;
            }

            if (modulo <= 0) {
                // O módulo deve ser um número positivo
                throw new ArgumentException(string.Format("{0} não é um número inteiro positivo", modulo));
            }

            for (int i = 0; i < expoente; i++) {
                // Multiplica o resultado pela base e aplica o módulo
                resultado = (resultado * baseNum) % modulo;
            }
            return resultado;
        }
    }
}
