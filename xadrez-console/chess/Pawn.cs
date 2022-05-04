using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class Pawn : Piece
    {
        private ChessGame Game;
        public Pawn(Board board, Color color, ChessGame game) : base(color, board)
        {
            this.Game = game;
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

                // jogada especial - en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && HasOponnent(left) && Board.Piece(left) == Game.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HasOponnent(right) && Board.Piece(right) == Game.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
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

                // jogada especial en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && HasOponnent(left) && Board.Piece(left) == Game.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && HasOponnent(right) && Board.Piece(right) == Game.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
