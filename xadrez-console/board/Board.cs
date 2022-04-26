namespace xadrez_console.board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        public Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public void SetPiece(Piece p, Position pos)
        {
            if (PieceExists(pos))
                throw new BoardException("Já existe uma peça nessa posição!");
            
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if(Piece(pos) == null)
                return null;

            Piece aux = Piece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool PieceExists(Position pos)
        {
            ValidPosition(pos);
            return Piece(pos) != null;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
                return false;
            else
                return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
                throw new BoardException("Posição inválida!");
        }
    }
}
