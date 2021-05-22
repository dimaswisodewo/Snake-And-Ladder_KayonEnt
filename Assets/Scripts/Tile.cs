using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    public TILE_TYPE tileType = TILE_TYPE.TILE;

    public void SetText(string text)
    {
        _textMesh.text = text;
    }
}
