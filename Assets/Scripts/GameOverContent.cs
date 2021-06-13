using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverContent : MonoBehaviour
{
    [SerializeField] private Text _positionText;
    [SerializeField] private Text _playerName;
    [SerializeField] private Image _playerImage;

    public void SetPositionText(string positionString)
    {
        _positionText.text = positionString;
    }

    public void SetPlayerName(string playerName)
    {
        _playerName.text = playerName;
    }

    public void SetPlayerImageColor(Color playerColor)
    {
        _playerImage.color = playerColor;
    }
}
