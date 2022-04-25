namespace xadrez_console.board
{
    class Position
    {
        public int line { get; set; }
        public int column { get; set; }

        public Position() { }

        public Position(int line, int column)
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
