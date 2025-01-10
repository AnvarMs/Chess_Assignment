
using System.Collections.Generic;
using UnityEngine;
namespace Chess.Scripts.Core
{
    public abstract class ChessPiece : MonoBehaviour
    {
        [SerializeField] public int row, column;
        [SerializeField] public bool isWhite;
      
        public abstract List<Vector2Int> GetPossibleMoves();

        protected void HighlightMoves(List<Vector2Int> moves)
        {
            ChessBoardPlacementHandler.Instance.ClearHighlights();
            int i = 0;
            foreach (var move in moves)
            {
                int targetRow = move.x;
                int targetCol = move.y;

                var targetTile = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
                if (targetTile != null)
                { 
                    ChessBoardPlacementHandler.Instance.Highlight(targetRow, targetCol,Color.green);
                    i++;
                }
            }
        }
             public void OnSelected()
        {
            var possibleMoves = GetPossibleMoves();
            HighlightMoves(possibleMoves);
            foreach (var move in possibleMoves)
            {
                Debug.Log(move);
            }
        }
    
    }
}