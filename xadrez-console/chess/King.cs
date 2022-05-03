using xadrez_console.board;

namespace xadrez_console.chess
{
    class King : Piece
    {
        public King(Board board, Color color): base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
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

            //acima
            pos.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //ne
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //direita
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //se
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //abaixo
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //so
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //esquerda
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //no
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}
