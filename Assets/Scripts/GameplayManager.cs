using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Dice _dice;
    [SerializeField] private CameraController _camController;
    [SerializeField] private PlayerManager _playerManager;

    public delegate void OnGameIsOver();
    public static event OnGameIsOver onGameIsOver;

    public void OnGenerateButtonClick()
    {
        if (_board.IsBoardConfigurationValid())
        {
            UIManager.Instance.SetActiveBoardCustomizationPanel(false);
            InitializeGameComponent();
        }
    }

    private void InitializeGameComponent()
    {
        _board.InitializeBoard();
        _playerManager.InitializePlayer();
        
        UIManager.Instance.SetPlayerText(string.Concat("PLAYER ", _playerManager.CurrentlyPlayingIndex + 1));

        // Centering camera follow position relative to Board
        Vector2 camFollowPos = new Vector2((_board.colCount / 2f) -
            0.5f, (_board.rowCount / 2f) - 0.5f);
        _camController.SetCameraLookAtPosition(camFollowPos);

        onGameIsOver += GameIsOver;
    }

    private bool HasPlayerWin(Player inputPlayer)
    {
        if (inputPlayer.tilePosition == _board.tiles.Count - 1) return true;
        else return false;
    }

    private void SetPlayerHasWin(Player inputPlayer)
    {
        inputPlayer.hasWin = true;
        _playerManager.numOfActivePlayer--;
    }

    private bool IsGameOver()
    {
        if (_playerManager.numOfActivePlayer > 0) return false;
        else return true;
    }

    private void GameIsOver()
    {
        Debug.Log("GAME IS OVER!!!");
    }

    // On player start moving according to dice number
    private void OnPlayerStartJumping()
    {
        _dice.SetActiveRollDiceButton(false);
    }

    // On player finish moving according to dice number
    private void OnPlayerFinishJumping(Player player = null)
    {
        PlayerTilePositionChecking(player);
    }

    // On player finish moved by ladder or snake
    private void OnPlayerFinishMoving()
    {
        _dice.SetActiveRollDiceButton(true);
        UIManager.Instance.SetPlayerText(string.Concat("PLAYER ", _playerManager.CurrentlyPlayingIndex + 1));
    }

    private void PlayerTilePositionChecking(Player player)
    {
        Tile tile = _board.tiles[player.tilePosition];
        switch (tile.tileType)
        {
            case TILE_TYPE.LADDER_BOTTOM:
                Ladder ladder = tile.GetComponent<Ladder>();
                ladder.MovePlayerToTop(player, onMoveFinish: () => OnPlayerFinishMoving());
                player.tilePosition = ladder.top;
                break;

            case TILE_TYPE.SNAKE_HEAD:
                Snake snake = tile.GetComponent<Snake>();
                snake.MovePlayerToTail(player, onMoveFinish: () => OnPlayerFinishMoving());
                player.tilePosition = snake.tail;
                break;

            default:
                OnPlayerFinishMoving();
                break;
        }
    }

    public void PlayerAction()
    {
        if (IsGameOver())
            return;

        Player player = _playerManager.GetCurrentPlayingPlayer();
        _playerManager.SetNextPlayingPlayer();

        // Get queue of player step
        Queue<Vector2> stepQueue = _board.GetStepQueue(player, player.tilePosition, _dice.diceNumber);

        // Move currently playing player step by step to destination tile
        player.JumpStepByStep(stepQueue, () => OnPlayerStartJumping(), () => OnPlayerFinishJumping(player));

        // Check if player is on last tile
        if (HasPlayerWin(player))
            SetPlayerHasWin(player);

        // Check if game is over
        if (IsGameOver())
            onGameIsOver?.Invoke();
    }
}
