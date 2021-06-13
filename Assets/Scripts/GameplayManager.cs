using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Dice _dice;
    [SerializeField] private CameraController _camController;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerTag _playerTag;

    private int _historyFrom;
    private int _historyTo;

    private Queue<Player> _winningQueue = new Queue<Player>();

    public void OnGenerateButtonClick()
    {
        if (_board.IsBoardConfigurationValid())
        {
            UIManager.Instance.SetActiveBoardCustomizationPanel(false);
            UIManager.Instance.SetActiveBoardComponentCustomizationPanel(true);

            _board.InitializeBoard();

            // Centering camera follow position relative to Board
            Vector2 camFollowPos = new Vector2((_board.ColCount / 2f) -
                0.5f, (_board.RowCount / 2f) - 0.5f);
            _camController.SetCameraLookAtPosition(camFollowPos);
        }
    }

    public void OnPlayerCustomizationDoneButtonClick()
    {
        UIManager.Instance.SetActivePlayerCustomizationPanel(false);
        UIManager.Instance.SetActiveBoardCustomizationPanel(true);

        _playerManager.playerToSpawn = UIManager.Instance.GetPlayerCount();
    }

    public void OnRandomizeGameComponentButtonClick()
    {
        _board.RandomizeBoardComponents();
    }

    public void OnStartPlayButtonClick()
    {
        _board.FinishBoardComponentPlacement();
        _playerManager.InitializePlayer();
        _playerTag.gameObject.SetActive(true);
        _playerTag.SetPlayerTagPosition(_playerManager.GetCurrentPlayingPlayer().transform.position);

        UIManager.Instance.SetActiveBoardComponentCustomizationPanel(false);
        UIManager.Instance.SetPlayerText(string.Concat((_playerManager.CurrentlyPlayingIndex +1).ToString(), ". ",  _playerManager.GetCurrentPlayingPlayer().playerName));
    }

    public void OnBackButtonClick()
    {
        _board.ResetBoardComponentPosition();

        UIManager.Instance.SetActiveBoardCustomizationPanel(true);
        UIManager.Instance.SetActiveBoardComponentCustomizationPanel(false);
        UIManager.Instance.SetBoardConfigNotValidText(string.Empty);
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
        UIManager.Instance.SetActiveGameOverPanel(true);
        for (int i = 0; i < _playerManager.playerToSpawn; i++)
        {
            Player player = _winningQueue.Dequeue();

            UIManager.Instance.gameOverContents[i].SetPositionText(string.Concat("#", (i + 1).ToString("00")));
            UIManager.Instance.gameOverContents[i].SetPlayerImageColor(player.GetSpriteColor());
            UIManager.Instance.gameOverContents[i].SetPlayerName(player.playerName);
        }
    }

    // On player start moving according to dice number
    private void OnPlayerStartJumping(Player player = null)
    {
        _dice.SetActiveRollDiceButton(false);
        _playerTag.gameObject.SetActive(false);

        _historyTo = player.tilePosition + 1; // Ditambah 1, soalnya tilePosition masih dlm bentuk index, mulai dari 0 bukan 1
        _dice.SetDiceHistoryText(string.Concat(player.playerName, "\n", _historyFrom, " - ", _historyTo));
    }

    // On player finish moving according to dice number
    private void OnPlayerFinishJumping(Player player = null)
    {
        PlayerTilePositionChecking(player);
    }

    // On player start moving by ladder or snake
    private void OnPlayerStartMoving(Player player = null)
    {
        _dice.SetDiceHistoryText(string.Concat(player.playerName, "\n", _historyFrom, " - ", _historyTo, " - ", player.tilePosition + 1));
    }

    // On player finish moved by ladder or snake
    private void OnPlayerFinishMoving(Player player = null)
    {
        _dice.SetActiveRollDiceButton(true);
        _playerTag.gameObject.SetActive(true);
        _playerTag.SetPlayerTagPosition(_playerManager.GetCurrentPlayingPlayer().transform.position);

        UIManager.Instance.SetPlayerText(string.Concat((_playerManager.CurrentlyPlayingIndex + 1).ToString(), ". ", _playerManager.GetCurrentPlayingPlayer().playerName));

        // Check if player is on last tile
        if (HasPlayerWin(player))
        {
            SetPlayerHasWin(player);
            _winningQueue.Enqueue(player);
        }

        // Check if game is over
        if (IsGameOver())
            GameIsOver();
    }

    private void PlayerTilePositionChecking(Player player)
    {
        Tile tile = _board.tiles[player.tilePosition];
        switch (tile.tileType)
        {
            case TILE_TYPE.LADDER_BOTTOM:
                Ladder ladder = tile.GetComponent<Ladder>();
                player.tilePosition = ladder.top;

                ladder.MovePlayerToTop(player, onMoveStart: () => OnPlayerStartMoving(player), onMoveFinish: () => OnPlayerFinishMoving(player));
                break;

            case TILE_TYPE.SNAKE_HEAD:
                Snake snake = tile.GetComponent<Snake>();
                player.tilePosition = snake.tail;

                snake.MovePlayerToTail(player, onMoveStart: () => OnPlayerStartMoving(player), onMoveFinish: () => OnPlayerFinishMoving(player));
                break;

            default:
                OnPlayerFinishMoving(player);
                break;
        }
    }

    public void PlayerAction()
    {
        if (IsGameOver())
            return;

        Player player = _playerManager.GetCurrentPlayingPlayer();
        _historyFrom = player.tilePosition + 1; // Ditambah 1, soalnya tilePosition masih dlm bentuk index, mulai dari 0 bukan 1
        _playerManager.SetNextPlayingPlayer();

        // Get queue of player step
        Queue<Vector2> stepQueue = _board.GetStepQueue(player, player.tilePosition, _dice.diceNumber);

        // Move currently playing player step by step to destination tile
        player.JumpStepByStep(stepQueue, onJumpStart: () => OnPlayerStartJumping(player), onJumpFinish: () => OnPlayerFinishJumping(player));
    }
}
