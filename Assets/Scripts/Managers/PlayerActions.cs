using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerData PlayerData;
    private bool m_CanPlay = false;

    public void CallPlayer(int turnNumber)
    {
        PlayerData.PlayerEconomy.AddNewTurnMana(turnNumber);
        m_CanPlay = true;
    }

    public bool EndTurn()
    {
        if (PlayerData.DataManager.TurnManager.TrySkipTurn())
        {
            m_CanPlay = false;
            return true;
        }
        return false;
    }

    public bool PlacePiece(int pieceNumber, (int, int) selectedSpot)
    {
        if (!m_CanPlay) return false;
        Piece selectedPiece = new MirrorSlash(); // DataManager.SelectablePieces.m_piecesListInfo[pieceNumber].m_piece
        int pieceCost = 1; // DataManager.SelectablePieces.m_piecesListInfo[pieceNumber].m_cost
        if (PlayerData.PlayerEconomy.PayForPlacement(pieceCost))
        {
            PlayerData.DataManager.BoardManager.PlaceOnTile(selectedPiece, selectedSpot);
            return true;
        }
        return false;
    }

    public bool DeletePiece((int, int) selectedSpot)
    {
        if (!m_CanPlay) return false;
        if (PlayerData.PlayerEconomy.PayForDeletion())
        {
            PlayerData.DataManager.BoardManager.EmptyTile(selectedSpot);
            return true;
        }
        return false;
    }

    public bool MovePiece((int, int) sourceSpot, (int, int) destinationSpot)
    {
        if (!m_CanPlay) return false;
        if (PlayerData.PlayerEconomy.PayForMovement())
        {
            //if (DataManager.Rules.IsMovementAllowed())
            //{
                Piece movedPiece = PlayerData.DataManager.BoardManager.EmptyTile(sourceSpot);
                PlayerData.DataManager.BoardManager.PlaceOnTile(movedPiece, sourceSpot);
                return true;
            //}
        }
        return false;
    }
}