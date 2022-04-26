using xadrez_console.board;

namespace xadrez_console.chess
{
    internal class Tower : Piece
    {
    public Tower(Board board, Color color)
        : base(color, board)
    {
    }

    public override string ToString()
    {
        return "T";
    }
    }
}
