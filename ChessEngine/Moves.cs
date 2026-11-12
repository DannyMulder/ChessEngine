namespace ChessEngine
{
    internal class StartPosition()
    {
        public static ulong whitePawns = 0x00FF000000000000;
        public static ulong whiteKnights = 0x2400000000000000;
        public static ulong whiteBishops = 0x4200000000000000;
        public static ulong whiteRooks = 0x8100000000000000;
        public static ulong whiteQueen = 0x0800000000000000;
        public static ulong whiteKing = 0x1000000000000000;

        public static ulong blackPawns = 0x000000000000FF00;
        public static ulong blackKnights = 0x0000000000000024;
        public static ulong blackBishops = 0x0000000000000042;
        public static ulong blackRooks = 0x0000000000000081;
        public static ulong blackQueen = 0x0000000000000008;
        public static ulong blackKing = 0x0000000000000010;
    }

    internal class InitializeMoves()
    {
        public static ulong[] WhitePawnMovesMove = CalcPawns("white", "move");
        public static ulong[] BlackPawnMovesMove = CalcPawns("black", "move");
        public static ulong[] WhitePawnMovesAttack = CalcPawns("white", "attack");
        public static ulong[] BlackPawnMovesAttack = CalcPawns("black", "attack");
        public static ulong[] KnightMoves = new ulong[64];
        public static ulong[] BishopMoves = new ulong[64];
        public static ulong[] RookMoves = new ulong[64];
        public static ulong[] KingMoves = new ulong[64];
        public static ulong[] QueenMoves = InitializeQueen();

        private static ulong[] InitializeQueen()
        {
            for (int i = 0; i < 64; i++)
            {
                QueenMoves[i] = BishopMoves[i] | RookMoves[i];
            }

            return QueenMoves;
        }

        private static ulong[] CalcPawns(string color, string direction)
        {
            ulong[] moves = new ulong[64];

            if (direction == "forward")
            {
                for (int i = 0; i < 64; i++)
                {

                }
            }
            else
            {

            }

            return moves;
        }

        private static void CalcKnights()
        {

        }
    }
}
