using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _Board;
    [SerializeField] private Dice _Dice;
    [SerializeField] private CameraController _CamController;
    [SerializeField] private PlayerManager _PlayerManager;

    private delegate void OnPlayerMoveFinished();
    private OnPlayerMoveFinished onPlayerMoveFinished;

    private void Awake()
    {
        InitializeGameComponent();
    }

    private void InitializeGameComponent()
    {
        _Board.InitializeBoard();
        _PlayerManager.InitializePlayer();

        // Centering camera follow position relative to Board
        Vector2 camLookAtPos = new Vector2((_Board.rowCount / 2) -
            0.5f, (_Board.colCount / 2) - 0.5f);
        _CamController.SetCameraLookAtPosition(camLookAtPos);
    }

    private int GetTileDestinationIndex(Player player)
    {
        int maxTileIndex = _Board.tiles.Count - 1;
        int tileDestinationIndex = player.tilePosition + _Dice.diceNumber;

        if (tileDestinationIndex > maxTileIndex)
        {
            int remainder = tileDestinationIndex - maxTileIndex;
            tileDestinationIndex = maxTileIndex - remainder;
        }

        return tileDestinationIndex;
    }

    private bool HasPlayerWin(Player inputPlayer)
    {
        if (inputPlayer.tilePosition == _Board.tiles.Count - 1) return true;
        else return false;
    }

    private void SetPlayerHasWin(Player inputPlayer)
    {
        _PlayerManager.activePlayers.Remove(inputPlayer);
    }

    public void PlayerAction()
    {
        Player player = _PlayerManager.GetCurrentPlayingPlayer();
        player.tilePosition = GetTileDestinationIndex(player);

        // Move currently playing player to destination tile
        Vector2 newPosition = _Board.tiles[player.tilePosition].transform.position;
        player.JumpToPosition(newPosition);

        _PlayerManager.SetNextPlayingPlayer();

        // Check if player is on last tile
        if (HasPlayerWin(player))
            SetPlayerHasWin(player);

        onPlayerMoveFinished?.Invoke();
    }
}
