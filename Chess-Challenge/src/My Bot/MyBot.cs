using ChessChallenge.API;
using System;
using System.Collections.Generic;
using System.Linq;

public class MyBot : IChessBot
{
    // ZajBot
    // The worst chess AI ever
    
    // its more artificial than intelligent

    Random rng = new Random();
    
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves(); //i have no idea what im doing (i cant even play chess good)
        Move bestMove = moves[0]; //its 10x more over-engineered than evilbot and zajbot lost every sinle game lmao

        List<Move> goodMoves = new List<Move>();
        List<Move> okMoves = new List<Move>();
        List<Move> mehMoves = new List<Move>();

        foreach (Move move in moves)
        {
            Console.WriteLine(move); //#DEBUG

            board.MakeMove(move);

            bool isCheckmate = board.IsInCheckmate();
            bool isCheck = board.IsInCheck();

            bool isTargetSquareUnderAttack = board.SquareIsAttackedByOpponent(move.TargetSquare);
            bool isDraw = board.IsDraw();

            Console.WriteLine(String.Format("IsCheckmate? {0}", isCheckmate)); //#DEBUG
            Console.WriteLine(String.Format("isCheck? {0}", isCheck)); //#DEBUG
            Console.WriteLine(String.Format("isTargetSquareUnderAttack? {0}", isTargetSquareUnderAttack)); //#DEBUG
            Console.WriteLine(String.Format("isDraw? {0}", isDraw)); //#DEBUG

            if (isCheckmate)
            {
                board.UndoMove(move);
                return move;
            }

            if (isCheck)
            {
                if (!isTargetSquareUnderAttack && !isDraw)
                {
                    goodMoves.Append(move);
                }
                else if (!isDraw)
                {
                    okMoves.Append(move);
                }
            }
            else if (!isTargetSquareUnderAttack)
            {
                mehMoves.Append(move);
            }

            board.UndoMove(move);
            
            Console.WriteLine(""); //#DEBUG
        }

        if (goodMoves.Count > 0)
        {
            bestMove = goodMoves[rng.Next(goodMoves.Count)];
        }
        else if (okMoves.Count > 0)
        {
            bestMove = okMoves[rng.Next(okMoves.Count)];
        }
        else if (mehMoves.Count > 0)
        {
            bestMove = mehMoves[rng.Next(mehMoves.Count)];
        }
        else
        {
            bestMove = moves[rng.Next(moves.Length)];
        }

        Console.WriteLine("********************"); //#DEBUG
        Console.WriteLine(String.Format("\"Best\" {0}", bestMove)); //#DEBUG
        Console.WriteLine("********************"); //#DEBUG

        return bestMove; //def not the best move lol
    }
}