using System;
using xadrez_console.board;
using xadrez_console.chess;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            try
            {
                Board board = new Board(8, 8);

                board.SetPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.SetPiece(new Tower(board, Color.Black), new Position(1, 8));
                board.SetPiece(new King(board, Color.Black), new Position(2, 4));

                Screen.PrintBoard(board);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
