using System;
using xadrez_console.Tabuleiro;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            Posicao p = new Posicao(1, 2);
            Console.WriteLine("Posição: " + p);
            Console.ReadLine();
        }
    }
}
