using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject _playerCustomizationPanel;
    [SerializeField] private GameObject _boardCustomizationPanel;
    [SerializeField] private GameObject _boardComponentCustomizationPanel;
    [SerializeField] private Text _playerText;

    [Header("Board Customization")]
    [SerializeField] private InputField _rowCountIF;
    [SerializeField] private InputField _colCountIF;
    [SerializeField] private InputField _ladderCountIF;
    [SerializeField] private InputField _snakeCountIF;
    [SerializeField] private Text _notValidText;

    [Header("Player Customization")]
    [SerializeField] private Text _playerCountText;

    // Singleton initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetPlayerText(string.Empty);
        SetPlayerCountText(Config.MIN_PLAYER_COUNT.ToString());
        SetActivePlayerCustomizationPanel(true);
        SetActiveBoardCustomizationPanel(false);
        SetActiveBoardComponentCustomizationPanel(false);
    }

    public void SetPlayerText(string inputString)
    {
        _playerText.text = inputString;
    }

    public bool IsBoardConfigFieldEmpty()
    {
        if (string.IsNullOrEmpty(_rowCountIF.text) || string.IsNullOrEmpty(_colCountIF.text) ||
            string.IsNullOrEmpty(_ladderCountIF.text) || string.IsNullOrEmpty(_snakeCountIF.text))
        {
            return true;
        }

        return false;
    }

    public int GetRowCountInputFieldValue()
    {
        return MathUtility.StringToInt(_rowCountIF.text);
    }

    public int GetColCountInputFieldValue()
    {
        return MathUtility.StringToInt(_colCountIF.text);
    }

    public int GetLadderCountInputFieldValue()
    {
        return MathUtility.StringToInt(_ladderCountIF.text);
    }

    public int GetSnakeCountInputFieldValue()
    {
        return MathUtility.StringToInt(_snakeCountIF.text);
    }

    public void SetActivePlayerCustomizationPanel(bool setActive)
    {
        _playerCustomizationPanel.SetActive(setActive);
    }

    public void SetActiveBoardCustomizationPanel(bool setActive)
    {
        _boardCustomizationPanel.SetActive(setActive);
    }

    public void SetActiveBoardComponentCustomizationPanel(bool setActive)
    {
        _boardComponentCustomizationPanel.SetActive(setActive);
    }

    public void SetBoardConfigNotValidText(string inputString)
    {
        _notValidText.text = inputString;
    }

    public void OnIncrementPlayerButtonClick()
    {
        int playerCount = GetPlayerCount();
        playerCount++;
        playerCount = MathUtility.ClampNumber(playerCount, Config.MIN_PLAYER_COUNT, Config.MAX_PLAYER_COUNT);

        SetPlayerCountText(playerCount.ToString());
    }

    public void OnDecrementPlayerButtonClick()
    {
        int playerCount = GetPlayerCount();
        playerCount--;
        playerCount = MathUtility.ClampNumber(playerCount, Config.MIN_PLAYER_COUNT, Config.MAX_PLAYER_COUNT);

        SetPlayerCountText(playerCount.ToString());
    }

    private void SetPlayerCountText(string inputString)
    {
        _playerCountText.text = inputString;
    }

    public int GetPlayerCount()
    {
        return int.Parse(_playerCountText.text);
    }
}
