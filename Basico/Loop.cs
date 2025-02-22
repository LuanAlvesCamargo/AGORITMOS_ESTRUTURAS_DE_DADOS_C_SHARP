using System;

public class Loop
{
    public static void Main() {
    
        // Entendendo o for:
        // 1.(void) Inicializador de variaveis do escopo.
        // 2.(bool) Condições da repetição.
        // 3.(void) Incrementador/Decrementador de variaveis
        

        for(/* Inicializador */ ;  /* Condição */ ; /* Incrementador/Decrementador */) {

        }

        for(int i = 0; i < 10; i++) {

        }

        for(int i = 10; i > 0; i--) {

        }

        int i = 0;

        while(i++ < 10) {
            if(1 + 1 == 2) {
                continue;
            }

        break;
        }

        i = 10;

        do {
            if(1 + 1 == 2) {
                continue;
            }

        break;
        }while(--i > 0);
    }
}
