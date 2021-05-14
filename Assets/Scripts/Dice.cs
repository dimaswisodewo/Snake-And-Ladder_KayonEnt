using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private Text _DiceText;
    [SerializeField] private Button _RollButton;
    public int diceNumber;

    [Header("Events")]
    public UnityEvent onRollDiceFinished;

    public void RollDice()
    {
        diceNumber = MathUtility.GetRandomDiceNumber();
        SetDiceText(diceNumber.ToString());

        onRollDiceFinished?.Invoke();
    }

    public void SetActiveRollDiceButton(bool setActive)
    {
        _RollButton.interactable = setActive;
    }

    private void SetDiceText(string newText)
    {
        _DiceText.text = newText;
    }
}
