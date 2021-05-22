using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private GameObject _boardCustomizationPanel;
    [SerializeField] private Text _playerText;

    [Header("Board Customization")]
    [SerializeField] private InputField _rowCountIF;
    [SerializeField] private InputField _colCountIF;
    [SerializeField] private InputField _ladderCountIF;
    [SerializeField] private InputField _snakeCountIF;
    [SerializeField] private Text _notValidText;

    // Singleton initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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

    public void SetActiveBoardCustomizationPanel(bool setActive)
    {
        _boardCustomizationPanel.SetActive(setActive);
    }

    public void SetBoardConfigNotValidText(string inputString)
    {
        _notValidText.text = inputString;
    }
}
