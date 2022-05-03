using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        public bool HasOponnent(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        public bool IsFree(Position pos)
        {
            return Board.Piece(pos) ==  null;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(8, 8);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && NumberOfMoves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && HasOponnent(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && HasOponnent(pos))
                    mat[pos.Line, pos.Column] = true;
            } else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && NumberOfMoves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && HasOponnent(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && HasOponnent(pos))
                    mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
