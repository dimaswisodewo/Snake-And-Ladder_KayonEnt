using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _playerCustomizationPanel;
    [SerializeField] private GameObject _playerDetailPanel;
    [SerializeField] private GameObject _boardCustomizationPanel;
    [SerializeField] private GameObject _boardComponentCustomizationPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _gameOverPanelContentPrefab;
    [SerializeField] private Transform _gameOverContentParent;
    [SerializeField] private Text _playerText;

    [Header("Board Customization")]
    [SerializeField] private InputField _rowCountIF;
    [SerializeField] private InputField _colCountIF;
    [SerializeField] private InputField _ladderCountIF;
    [SerializeField] private InputField _snakeCountIF;
    [SerializeField] private Text _notValidText;

    [Header("Player Customization")]
    [SerializeField] private Text _playerCountText;

    [Header("Player Detail Customization")]
    [SerializeField] private Text _playerDetailTitle;
    [SerializeField] private Image _playerPreview;
    [SerializeField] private InputField _playerNameIF;

    [HideInInspector]
    public List<GameOverContent> gameOverContents = new List<GameOverContent>();

    // Singleton initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SetPlayerText(string.Empty);
        SetPlayerCountText(Config.MIN_PLAYER_COUNT.ToString());
        SetActivePlayerCustomizationPanel(true);
        SetActivePlayerDetailPanel(false);
        SetActiveBoardCustomizationPanel(false);
        SetActiveBoardComponentCustomizationPanel(false);
        SetActiveGameOverPanel(false);
    }

    public void SetPlayerText(string inputString)
    {
        _playerText.text = inputString;
    }

    public void SetPlayerDetailTitleText(string inputString)
    {
        _playerDetailTitle.text = inputString;
    }

    public string GetPlayerDetailTitleText()
    {
        return _playerDetailTitle.text;
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

    public string GetPlayerNameInputFieldValue()
    {
        return _playerNameIF.text;
    }

    public void ResetPlayerNameInputFieldValue()
    {
        _playerNameIF.text = string.Empty;
    }

    public Color GetPlayerPreviewColor()
    {
        return _playerPreview.color;
    }

    public void ChangePlayerPreviewColor()
    {
        _playerPreview.color = MathUtility.ConvertToColor(MathUtility.GetRandomColor());
    }

    public void SetActivePlayerCustomizationPanel(bool setActive)
    {
        _playerCustomizationPanel.SetActive(setActive);
    }

    public void SetActivePlayerDetailPanel(bool setActive)
    {
        _playerDetailPanel.SetActive(setActive);
    }

    public void SetActiveBoardCustomizationPanel(bool setActive)
    {
        _boardCustomizationPanel.SetActive(setActive);
    }

    public void SetActiveGameOverPanel(bool setActive)
    {
        _gameOverPanel.SetActive(setActive);
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

    public void OnRestartButtonClick()
    {
        SceneLoader.Instance.LoadScene(Config.SCENE_GAMEPLAY);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void InitGameOverPanelContent()
    {
        for (int i = 0; i < GetPlayerCount(); i++)
        {
            GameObject go = Instantiate(_gameOverPanelContentPrefab, _gameOverContentParent);
            GameOverContent gameOverContent = go.GetComponent<GameOverContent>();
            gameOverContents.Add(gameOverContent);
        }
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
