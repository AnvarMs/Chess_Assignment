using Chess.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    private void Start()
    {
        transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        ChessBoardPlacementHandler.Instance.AddPiece(this);
    }
    public override List<Vector2Int> GetPossibleMoves()
    {
       
        var moves = new List<Vector2Int>();

        // Directions: Up, Down, Right, Left
        Vector2Int[] directions = {
        new Vector2Int(1, 0), // Up
        new Vector2Int(-1, 0), // Down
        new Vector2Int(0, 1), // Right
        new Vector2Int(0, -1) // Left
    };

        // Loop through all directions
        foreach (var direction in directions)
        {
            for (int i = 1; i < 8; i++)
            {
                int targetRow = row + direction.x * i;
                int targetColumn = column + direction.y * i;

                if (IsValidMove(targetRow, targetColumn))
                {
                    moves.Add(new Vector2Int(targetRow, targetColumn));

     
                }
                else if (!ChessBoardPlacementHandler.Instance.IsTileNotEquipped(targetRow, targetColumn) && targetRow >= 0 && targetRow < 8 && targetColumn >= 0 && targetColumn < 8)
                {



                    if (ChessBoardPlacementHandler.Instance.GetChessPiece(targetRow, targetColumn).isWhite != this.isWhite)
                        moves.Add(new Vector2Int(targetRow, targetColumn));
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
