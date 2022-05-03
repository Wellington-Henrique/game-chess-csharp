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
                    try
                    {
                        Console.Clear();
                        Screen.PrintGame(game);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = game.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(game.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        game.ValidateDestinationPosition(origin, destination);

                        game.MakesMove(origin, destination);

                    } catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    
                }
                Console.Clear();
                Screen.PrintGame(game);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        Console.ReadLine();
        }
    }
}
