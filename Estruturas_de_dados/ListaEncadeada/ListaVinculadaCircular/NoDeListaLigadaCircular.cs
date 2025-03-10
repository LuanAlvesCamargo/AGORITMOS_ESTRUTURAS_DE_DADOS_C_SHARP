namespace EstruturasDeDados.ListaLigada.ListaLigadaCircular {
    public class NoListaLigadaCircular<T> {
        public NoListaLigadaCircular(T dados) {
            Dados = dados;
        }
        public T Dados { get; set; }
        public NoListaLigadaCircular<T>? Proximo { get; set; }
    }
}

