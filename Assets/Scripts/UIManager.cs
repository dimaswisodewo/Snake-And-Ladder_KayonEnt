using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Text _playerText;

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
}
