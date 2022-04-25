using System;
using xadrez_console.board;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            Board board = new Board(8, 8);
            Console.WriteLine(board);
            Console.ReadLine();
        }
    }
}
