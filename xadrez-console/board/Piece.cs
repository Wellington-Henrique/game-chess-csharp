namespace xadrez_console.board
{
    abstract class Piece
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

        public void IncrementMove()
        {
            NumberOfMoves++;
        }

        public bool HasPossibleMove()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                        return true;
                }
            }
            return false;
        }
        public bool CanMoveTo(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }
        public abstract bool[,] PossibleMoves(); 
    }
}
