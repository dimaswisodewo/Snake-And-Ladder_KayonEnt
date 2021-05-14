using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TextMesh;
    public TILE_TYPE tileType = TILE_TYPE.TILE;

    public void SetText(string text)
    {
        _TextMesh.text = text;
    }
}
