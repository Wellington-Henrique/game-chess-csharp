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
        public ChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
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

            return capturedPiece;
        }

        public void MakesMove(Position origin, Position destination)
        {
            Piece capturedPiece = SetMove(origin, destination);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Check = IsInCheck(Opponent(CurrentPlayer)) ? true : false;

            Round++;
            ChangePlayer();
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

        public void SetNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void StartGame()
        {
            SetNewPiece('c', 1, new Tower(Board, Color.White));
            SetNewPiece('c', 2, new Tower(Board, Color.White));
            SetNewPiece('d', 2, new Tower(Board, Color.White));
            SetNewPiece('e', 2, new Tower(Board, Color.White));
            SetNewPiece('e', 1, new Tower(Board, Color.White));
            SetNewPiece('d', 1, new King(Board, Color.White));
            
            SetNewPiece('c', 7, new Tower(Board, Color.Black));
            SetNewPiece('c', 8, new Tower(Board, Color.Black));
            SetNewPiece('d', 7, new Tower(Board, Color.Black));
            SetNewPiece('e', 7, new Tower(Board, Color.Black));
            SetNewPiece('e', 8, new Tower(Board, Color.Black));
            SetNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
