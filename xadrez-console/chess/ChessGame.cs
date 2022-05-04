using System.Collections.Generic;
using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }
        private HashSet<Piece> _pieces = new HashSet<Piece>();
        private HashSet<Piece> _captured = new HashSet<Piece>();
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
        public ChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            StartGame();
        }

        public Piece SetMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);    
            p.IncrementMove();

            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(p, destination);

            if (capturedPiece != null)
                _captured.Add(capturedPiece);

            // jogada especial - roque pequeno
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);

                Piece T = Board.RemovePiece(originT);
                T.IncrementMove();

                Board.SetPiece(T, destinationT);
            }

            // jogada especial - roque grande
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);

                Piece T = Board.RemovePiece(originT);
                T.IncrementMove();

                Board.SetPiece(T, destinationT);
            }

            // jogada especial - en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                        posP = new Position(destination.Line + 1, destination.Column);
                    else
                        posP = new Position(destination.Line - 1, destination.Column);

                    capturedPiece = Board.RemovePiece(posP);
                    _captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destination, Piece captured)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecrementMove();

            if (captured != null)
            {
                Board.SetPiece(captured, destination);
                _captured.Remove(captured);
            }

            Board.SetPiece(p, origin);

            // jogada especial - roque pequeno
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinationT = new Position(origin.Line, origin.Column + 1);

                Piece T = Board.RemovePiece(destinationT);
                T.DecrementMove();

                Board.SetPiece(T, origin);
            }

            // jogada especial - roque grande
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinationT = new Position(origin.Line, origin.Column - 1);

                Piece T = Board.RemovePiece(destinationT);
                T.IncrementMove();

                Board.SetPiece(T, originT);
            }

            // jogada especical - en passant
            if (p is Pawn)
            {
                if (origin.Column != destination.Column && captured != VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position posP;

                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destination.Column);
                    } else
                    {
                        posP = new Position(4, destination.Column);
                    }
                    Board.SetPiece(pawn, posP);
                }
            }
        }

        public void MakesMove(Position origin, Position destination)
        {
            Piece capturedPiece = SetMove(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Piece p = Board.Piece(destination);

            // jogada especial - promocao
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) || (p.Color == Color.Black && destination.Line == 7))
                {
                    p = Board.RemovePiece(destination);
                    _pieces.Remove(p);

                    Piece queen = new Queen(Board, p.Color);
                    Board.SetPiece(queen, destination);
                    _pieces.Add(queen);
                }
            }

            Check = IsInCheck(Opponent(CurrentPlayer)) ? true : false;

            if (TestCheckmate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }

            // jogada especial - en passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
                throw new BoardException("Não existe peça na posição de origem escolhida!");

            if (CurrentPlayer != Board.Piece(pos).Color)
                throw new BoardException("A peça de origem escolhida não é sua!");

            if (!Board.Piece(pos).HasPossibleMove())
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
                throw new BoardException("Posição de destino inválida!");
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public HashSet<Piece> _capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece piece in _captured)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            return aux;
        }

        public HashSet<Piece> _piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(_capturedPieces(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White; 
        }

        private Piece King(Color color)
        {
            foreach(Piece piece in _piecesInGame(color))
            {
                if (piece is King)
                    return piece;
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException($"Não tem Rei da cor {color} no tabuleiro!");
            }

            foreach(Piece piece in _piecesInGame(Opponent(color)))
            {
                bool[,] mat = piece.PossibleMoves();
                if (mat[king.Position.Line, king.Position.Column])
                    return true;
            }
            return false;
        }

        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach(Piece piece in _piecesInGame(color))
            {
                bool[,] mat = piece.PossibleMoves();
                for(int i = 0; i < Board.Lines; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece captured = SetMove(origin, destination);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, destination, captured);

                            if (!testCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void SetNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void StartGame()
        {
            SetNewPiece('a', 1, new Tower(Board, Color.White));
            SetNewPiece('b', 1, new Horse(Board, Color.White));
            SetNewPiece('c', 1, new Bishop(Board, Color.White));
            SetNewPiece('d', 1, new Queen(Board, Color.White));
            SetNewPiece('e', 1, new King(Board, Color.White, this));
            SetNewPiece('f', 1, new Bishop(Board, Color.White));
            SetNewPiece('g', 1, new Horse(Board, Color.White));
            SetNewPiece('h', 1, new Tower(Board, Color.White));
            SetNewPiece('a', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('b', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('c', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('d', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('e', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('f', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('g', 2, new Pawn(Board, Color.White, this));
            SetNewPiece('h', 2, new Pawn(Board, Color.White, this));

            SetNewPiece('a', 8, new Tower(Board, Color.Black));
            SetNewPiece('b', 8, new Horse(Board, Color.Black));
            SetNewPiece('c', 8, new Bishop(Board, Color.Black));
            SetNewPiece('d', 8, new Queen(Board, Color.Black));
            SetNewPiece('e', 8, new King(Board, Color.Black, this));
            SetNewPiece('f', 8, new Bishop(Board, Color.Black));
            SetNewPiece('g', 8, new Horse(Board, Color.Black));
            SetNewPiece('h', 8, new Tower(Board, Color.Black));
            SetNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            SetNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
