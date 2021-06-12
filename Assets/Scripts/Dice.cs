using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private Text _diceText;
    [SerializeField] private Text _diceHistoryText;
    [SerializeField] private Button _rollButton;
    public int diceNumber;

    [Header("Events")]
    public UnityEvent onRollDiceFinished;

    private void Awake()
    {
        SetDiceText(string.Empty);
        SetDiceHistoryText(string.Empty);
    }

    public void RollDice()
    {
        diceNumber = MathUtility.GetRandomDiceNumber();
        SetDiceText(diceNumber.ToString());

        onRollDiceFinished?.Invoke();
    }

    public void SetActiveRollDiceButton(bool setActive)
    {
        _rollButton.interactable = setActive;
    }

    private void SetDiceText(string newText)
    {
        _diceText.text = newText;
    }

    public void SetDiceHistoryText(string newText)
    {
        _diceHistoryText.text = newText;
    }
}
