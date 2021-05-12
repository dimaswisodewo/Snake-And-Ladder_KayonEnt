using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
    public static int GetRandomDiceNumber()
    {
        return Random.Range(1, 6);
    }
}
public static class Config
{
    public static Color32 COLOR_RED = new Color32(243, 76, 83, 255);
    public static Color32 COLOR_GREEN = new Color32(50, 205, 50, 255);
    public static Color32 COLOR_BLUE = new Color32(0, 65, 106, 255);
    public static Color32 COLOR_YELLOW = new Color32(255, 255, 102, 255);
    public static Color32 COLOR_PURPLE = new Color32(174, 149, 238, 255);
    public static Color32 COLOR_PINK = new Color32(255, 51, 153, 255);

    public static Color32 GetColor32(COLOR color)
    {
        Color32 newColor;
        switch (color)
        {
            case COLOR.RED:
                newColor = Config.COLOR_RED;
                break;

            case COLOR.GREEN:
                newColor = Config.COLOR_GREEN;
                break;

            case COLOR.BLUE:
                newColor = Config.COLOR_BLUE;
                break;

            case COLOR.YELLOW:
                newColor = Config.COLOR_YELLOW;
                break;

            case COLOR.PURPLE:
                newColor = Config.COLOR_PURPLE;
                break;

            case COLOR.PINK:
                newColor = Config.COLOR_PINK;
                break;

            default:
                newColor = Config.COLOR_RED;
                break;
        }

        return newColor;
    }

    public static Color ConvertToColor(Color32 color)
    {
        Color newColor = new Color(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);

        return newColor;
    }
}

public enum COLOR
{
    RED,
    GREEN,
    BLUE,
    YELLOW,
    PURPLE,
    PINK
}