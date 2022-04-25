namespace xadrez_console.Tabuleiro
{
    class Posicao
    {
        public int line { get; set; }
        public int column { get; set; }

        public Posicao() { }

        public Posicao(int line, int column)
        {
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return $"{line}, {column}";
        }
    }
}
