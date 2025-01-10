
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core{ 
public class Pawn : ChessPiece
{
        private void Start()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            ChessBoardPlacementHandler.Instance.AddPiece(this);
        }
        public override List<Vector2Int> GetPossibleMoves()
        {
            var moves = new List<Vector2Int>();
            Vector2Int[] directions;
          
            if (isWhite)
            {
                directions = new Vector2Int[]
                {
            new Vector2Int(-1, 0),  // Forward for white
            new Vector2Int(-1, 1),  // Diagonal right capture
            new Vector2Int(-1, -1)  // Diagonal left capture
                };
            }
            else
            {
                directions = new Vector2Int[]
                {
            new Vector2Int(1, 0),   // Forward for black
            new Vector2Int(1, 1),   // Diagonal right capture
            new Vector2Int(1, -1)   // Diagonal left capture
                };
            }

            // Special case for pawn: Only move one square at a time
            // except for first move which can be two squares
            foreach (Vector2Int dir in directions)
            {
                int newRow = row + dir.x;
                int newCol = column + dir.y;
                // Forward movement
                if (dir.y == 0)
                {
                    

                    if (IsValidMove(newRow, newCol))
           
                    {
                        moves.Add(new Vector2Int(newRow, newCol));

                        // First move can be 2 squares
                        if ((isWhite && row == 6) || (!isWhite && row == 1))
                        {
                            newRow = row + (dir.x * 2);
                            if (IsValidMove(newRow, newCol))
                            {
                               
                                moves.Add(new Vector2Int(newRow, newCol));
                            }
                        }
                    }
                }
                // Diagonal captures
                else if(!ChessBoardPlacementHandler.Instance.IsTileNotEquipped(newRow, newCol) && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                  

                   
                        if (ChessBoardPlacementHandler.Instance.GetChessPiece(newRow, newCol).isWhite != this.isWhite)
                        moves.Add(new Vector2Int(newRow, newCol));
                      
                }
            }
            return moves;
        }

         private bool IsValidMove(int targetRow, int targetColumn)
    {
            // Check if the target position is within bounds
            // Check if the tile is occupied using ChessBoardPlacementHandler's dictionary

            return targetRow >= 0 && targetRow < 8 && targetColumn >= 0 && targetColumn < 8 && ChessBoardPlacementHandler.Instance.IsTileNotEquipped(targetRow, targetColumn);
    }


    }
}

