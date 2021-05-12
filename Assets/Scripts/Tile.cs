using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TextMesh;

    public void SetText(string text)
    {
        _TextMesh.text = text;
    }
}
