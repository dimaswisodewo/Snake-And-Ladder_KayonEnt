using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private Text _diceText;
    [SerializeField] private Button _rollButton;
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
        _rollButton.interactable = setActive;
    }

    private void SetDiceText(string newText)
    {
        _diceText.text = newText;
    }
}
