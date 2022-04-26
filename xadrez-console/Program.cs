using System;
using xadrez_console.board;
using xadrez_console.chess;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            try
            {
                ChessGame game = new ChessGame();

                while (!game.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(game.Board);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition(); 
                    
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    game.SetMove(origin, destination);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
