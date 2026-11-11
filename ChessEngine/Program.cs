using System.Security.Cryptography;

namespace ChessEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ulong whitePawns = 0x00FF000000000000;
            ulong whiteRooks = 0x8100000000000000;
            ulong whiteKnights = 0x2400000000000000;
            ulong whiteBishops = 0x4200000000000000;
            ulong whiteQueen = 0x0100000000000000;
            ulong whiteKing = 0x8000000000000000;


            ulong blackPawns = 0x000000000000FF00;
            ulong blackRooks = 0x0000000000000081;
            ulong blackKnights = 0x0000000000000024;
            ulong blackBishops = 0x0000000000000042;
            ulong blackQueen = 0x0000000000000001;
            ulong blackKing = 0x0000000000000080;

            Console.WriteLine(whitePawns);

            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
        }
    }
}