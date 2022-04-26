using System;
using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class ChessGame
    {
        public Board Board { get; private set; }
        private int Round { get; set; }
        private Color PlayerColor { get; set; }

        public bool Finished { get; set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            PlayerColor = Color.White;
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
