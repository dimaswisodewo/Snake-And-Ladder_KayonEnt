using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int tilePosition = 0;

    public void SetSpriteColor(COLOR colorEnum)
    {
        Color32 color32 = Config.GetColor32(colorEnum);
        Color newColor = MathUtility.ConvertToColor(color32);
        spriteRenderer.color = newColor;
    }

    public void JumpToPosition(Vector2 newPosition)
    {
        Tweening.JumpTo(transform, newPosition);
    }
}
