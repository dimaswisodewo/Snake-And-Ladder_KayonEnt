using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int tilePosition = 1;

    public void SetSpriteColor(COLOR colorEnum)
    {
        Color32 color32 = Config.GetColor32(colorEnum);
        Debug.Log(color32.ToString());
        Color newColor = Config.ConvertToColor(color32);
        spriteRenderer.color = newColor;
        Debug.Log(newColor.ToString());
    }

}
