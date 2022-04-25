namespace xadrez_console.board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMoves { get; set; }
        public Board Board { get; set; }

        public Piece(Position position, Color color, int numberOfMoves, Board board)
        {
            Position = position;
            Color = color;
            NumberOfMoves = numberOfMoves;
            Board = board;
        }
    }
}
