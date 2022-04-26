namespace xadrez_console.board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMoves { get; set; }
        public Board Board { get; set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Board = board;
            Color = color;
            NumberOfMoves = 0;
        }
    }
}
