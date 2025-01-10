using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    private void Start()
    {
        transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        ChessBoardPlacementHandler.Instance.AddPiece(this);
    }
    public override List<Vector2Int> GetPossibleMoves()
    {
        var moves = new List<Vector2Int>();
       
        // All possible relative moves for the King using Vector2Int array
        Vector2Int[] directions = new Vector2Int[]
        {
        new Vector2Int(1, 0),   // Down
        new Vector2Int(-1, 0),  // Up
        new Vector2Int(0, 1),   // Right
        new Vector2Int(0, -1),  // Left
        new Vector2Int(1, 1),   // Down-Right
        new Vector2Int(-1, -1), // Up-Left
        new Vector2Int(1, -1),  // Down-Left
        new Vector2Int(-1, 1)   // Up-Right
        };

        foreach (Vector2Int dir in directions)
        {
            int newRow = row + dir.x;
            int newCol = column + dir.y;
            // Checking if its possible move or note
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
