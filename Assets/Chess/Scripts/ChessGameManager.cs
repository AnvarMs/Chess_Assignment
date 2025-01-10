using Chess.Scripts.Core;
using System;
using UnityEngine;

public class ChessGameManager : MonoBehaviour
{
    [SerializeField] private ChessPiece selectedPiece;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectPiece();
        }
    }

    private void SelectPiece()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null)
        {
            var piece = hit.GetComponent<ChessPiece>();
            if (piece != null)
            {
                selectedPiece = piece;
                selectedPiece.OnSelected();
            }
        }
    }
}