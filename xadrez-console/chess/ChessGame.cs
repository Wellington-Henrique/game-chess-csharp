using System;
using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            StartGame();
        }

        public void SetMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);    
            p.IncrementMove();
            Piece CapturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(p, destination);
        }

        public void MakesMove(Position origin, Position destination)
        {
            SetMove(origin, destination);
            Round++;
            ChangePlayer();
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
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;

            //CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        private void StartGame()
        {
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.SetPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.SetPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
