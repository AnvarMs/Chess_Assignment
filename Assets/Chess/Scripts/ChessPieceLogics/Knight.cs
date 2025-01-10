using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;

public class Knight : ChessPiece
{
    private void Start()
    {
        transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        ChessBoardPlacementHandler.Instance.AddPiece(this);
    }
    public override List<Vector2Int> GetPossibleMoves()
    {
        var moves = new List<Vector2Int>();
        
        // Define all possible knight moves as offsets
        Vector2Int[] knightMoves = {
    new Vector2Int(2, 1), new Vector2Int(2, -1),
    new Vector2Int(-2, 1), new Vector2Int(-2, -1),
    new Vector2Int(1, 2), new Vector2Int(1, -2),
    new Vector2Int(-1, 2), new Vector2Int(-1, -2)
};

        // Check each possible move
        foreach (var move in knightMoves)
        {
            int newRow = row + move.x;
            int newCol = column + move.y;

            if (IsValidMove(newRow, newCol))
            {
                moves.Add(new Vector2Int(newRow, newCol));
            }
            else if (!ChessBoardPlacementHandler.Instance.IsTileNotEquipped(newRow, newCol) && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {



                if (ChessBoardPlacementHandler.Instance.GetChessPiece(newRow, newCol).isWhite != this.isWhite)
                    moves.Add(new Vector2Int(newRow, newCol));
                
            }
        }





        return moves;
    }

    private bool IsValidMove(int targetRow, int targetColumn)
    {
        return targetRow >= 0 && targetRow < 8 && targetColumn >= 0 && targetColumn < 8 && ChessBoardPlacementHandler.Instance.IsTileNotEquipped(targetRow, targetColumn);
    }
}
