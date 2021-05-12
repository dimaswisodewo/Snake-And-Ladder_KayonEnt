using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _Board;
    [SerializeField] private Dice _Dice;
    [SerializeField] private CameraController _CamController;
    [SerializeField] private PlayerManager _PlayerManager;

    private int _PlayingPlayerIndex = 0;

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

    public void MoveCurrentlyPlayingPlayer()
    {
        Player player = _PlayerManager.GetCurrentPlayingPlayer(_PlayingPlayerIndex);
        player.tilePosition = GetTileDestinationIndex(player); 

        Vector2 newPosition = _Board.tiles[player.tilePosition].transform.position;
        player.SetPlayerPosition(newPosition);

        // TODO: Mindahin code dibawah, mungkin ke PlayerManager
        _PlayingPlayerIndex++;
        if (_PlayingPlayerIndex > _PlayerManager._PlayerCount - 1)
            _PlayingPlayerIndex = 0;
    }
}
