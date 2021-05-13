using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _Board;
    [SerializeField] private Dice _Dice;
    [SerializeField] private CameraController _CamController;
    [SerializeField] private PlayerManager _PlayerManager;

    public delegate void OnGameIsOver();
    public static event OnGameIsOver onGameIsOver;

    private void Awake()
    {
        InitializeGameComponent();

        onGameIsOver += GameIsOver;
    }

    private void InitializeGameComponent()
    {
        _Board.InitializeBoard();
        _PlayerManager.InitializePlayer();

        // Centering camera follow position relative to Board
        Vector2 camLookAtPos = new Vector2((_Board.colCount / 2f) -
            0.5f, (_Board.rowCount / 2f) - 0.5f);
        _CamController.SetCameraLookAtPosition(camLookAtPos);
    }

    private bool HasPlayerWin(Player inputPlayer)
    {
        if (inputPlayer.tilePosition == _Board.tiles.Count - 1) return true;
        else return false;
    }

    private void SetPlayerHasWin(Player inputPlayer)
    {
        inputPlayer.hasWin = true;
        _PlayerManager._NumOfActivePlayer--;
    }

    private bool IsGameOver()
    {
        if (_PlayerManager._NumOfActivePlayer > 0) return false;
        else return true;
    }

    private void GameIsOver()
    {
        Debug.Log("GAME IS OVER!!!");
    }

    public void PlayerAction()
    {
        if (IsGameOver())
            return;

        Player player = _PlayerManager.GetCurrentPlayingPlayer();
        _PlayerManager.SetNextPlayingPlayer();

        // Get queue of player step
        Queue<Vector2> stepQueue = _Board.GetStepQueue(player, player.tilePosition, _Dice.diceNumber);

        // Move currently playing player step by step to destination tile
        player.JumpStepByStep(stepQueue, () => _Dice.SetActiveRollDiceButton(false), () => _Dice.SetActiveRollDiceButton(true));

        // Check if player is on last tile
        if (HasPlayerWin(player))
            SetPlayerHasWin(player);

        // Check if game is over
        if (IsGameOver())
            onGameIsOver?.Invoke();
    }
}
