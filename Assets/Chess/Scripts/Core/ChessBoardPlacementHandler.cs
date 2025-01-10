using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    private GameObject[,] _chessBoard;
    private  Dictionary<Vector2Int, ChessPiece> pieceTraker;
    internal static ChessBoardPlacementHandler _instance;
    public static ChessBoardPlacementHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ChessBoardPlacementHandler instance is null. Make sure it is present in the scene.");
            }
            return _instance;
        }
    }
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Multiple ChessBoardPlacementHandler instances found. Destroying the duplicate.");
            Destroy(gameObject);
            return;
        }
        _instance = this;
        pieceTraker = new Dictionary<Vector2Int, ChessPiece>();
        GenerateArray();
    }

    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {

                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;

                pieceTraker.Add(new Vector2Int(i, j), null);

            }
        }
    }

    internal GameObject GetTile(int i, int j) {
        try {
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(int row, int col,Color color) {
        var tile = GetTile(row, col).transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform).GetComponent<SpriteRenderer>().color =color;
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }



    #region Dictionary to track the positions of chess pieces
    internal void AddPiece(ChessPiece piece) {
        //Debug.Log("Adding piece"+  piece);  
        if (pieceTraker.ContainsKey(new Vector2Int(piece.row, piece.column)))
        {
            pieceTraker[new Vector2Int(piece.row, piece.column)] = piece;
        }
        else
        {
            Debug.LogError($"Attempting to add a piece at an invalid position: {piece.row}, {piece.column}");
        }
    }
    internal void RemovePiece(ChessPiece piece) {
        var position = new Vector2Int(piece.row, piece.column);
        if (pieceTraker.TryGetValue(position, out var existingPiece) && existingPiece == piece)
        {
            pieceTraker.Remove(position);
        }
        else
        {
            Debug.LogWarning($"No piece to remove at position: {piece.row}, {piece.column}");
        }

    }
    public ChessPiece GetPieceAt(Vector2Int position)
    {
        Debug.Log("is taking piece" + pieceTraker[position].name);
        if (pieceTraker.ContainsKey(position))
        {
            return pieceTraker[position]; // Return the piece at the specified position
        }

        return null; // Return null if there's no piece at the given position
    }

    internal bool IsTileNotEquipped(int row , int col)
    {
        return pieceTraker.TryGetValue(new Vector2Int(row,col), out var _piece) && _piece == null;

    }

    internal ChessPiece GetChessPiece(int row, int col)
    {
        pieceTraker.TryGetValue(new Vector2Int(row, col), out var piece);
        return piece;
    }
    #endregion





    #region Highlight Testing

    private void Start() {
          // StartCoroutine(Testing());
          //StartCoroutine(PrintDictionary());
        }

    //    private IEnumerator Testing() {
    //        Highlight(2, 7,Color.green);
    //        yield return new WaitForSeconds(1f);

    //        ClearHighlights();
    //    Highlight(2, 7, Color.green );
    //    Highlight(2, 6, Color.green);
    //    Highlight(2, 5, Color.green);
    //    Highlight(2, 4, Color.green);
    //    yield return new WaitForSeconds(1f);

    //    ClearHighlights();
    //    Highlight(7, 7, Color.green);
    //    Highlight(2, 7, Color.green);
    //    yield return new WaitForSeconds(1f);
    //}

    private IEnumerator PrintDictionary()
    {
        yield return new WaitForSeconds(5);
        if (pieceTraker.Count == 0)
        {
            Debug.Log("The dictionary is empty.");
            yield return null;
        }

        foreach (var entry in pieceTraker)
        {
            Vector2Int position = entry.Key;
            ChessPiece piece = entry.Value;

            string pieceName = piece != null ? piece.name : "No piece";
            Debug.Log($"Position: ({position.x}, {position.y}) - Piece: {pieceName}");
        }
    }

    #endregion
}