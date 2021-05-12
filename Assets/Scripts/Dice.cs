using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    [SerializeField] private Text _DiceText;
    public int diceNumber;

    public UnityEvent onRollDiceFinished;

    public void RollDice()
    {
        diceNumber = MathUtility.GetRandomDiceNumber();
        SetDiceText(diceNumber.ToString());

        onRollDiceFinished?.Invoke();
    }

    private void SetDiceText(string newText)
    {
        _DiceText.text = newText;
    }
}
