using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    private void Start()
    {
        transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        ChessBoardPlacementHandler.Instance.AddPiece(this);
    }

    public override List<Vector2Int> GetPossibleMoves()
    {
        var moves = new List<Vector2Int>();
       
        // Define diagonal directions for bishop
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1),   // Top-right
            new Vector2Int(-1, -1), // Bottom-left
            new Vector2Int(-1, 1),  // Top-left
            new Vector2Int(1, -1)   // Bottom-right
        };

        // Check moves in each diagonal direction
        foreach (Vector2Int dir in directions)
        {
            for (int i = 1; i < 8; i++)
            {
                int newRow = row + (dir.x * i);
                int newCol = column + (dir.y * i);

                if (IsValidMove(newRow, newCol))
                {
               moves.Add(new Vector2Int(newRow, newCol));
                }
                else if (!ChessBoardPlacementHandler.Instance.IsTileNotEquipped(newRow, newCol) && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {



                    if (ChessBoardPlacementHandler.Instance.GetChessPiece(newRow, newCol).isWhite != this.isWhite)
                        moves.Add(new Vector2Int(newRow, newCol));
                    break;
                }
            }
        }

        return moves;
    }

private bool IsValidMove(int targetRow, int targetColumn)
    {
        return targetRow >= 0 && targetRow < 8 && targetColumn >= 0 && targetColumn < 8 && ChessBoardPlacementHandler.Instance.IsTileNotEquipped(targetRow, targetColumn);
    }

}
