using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
    public static int GetRandomDiceNumber()
    {
        return Random.Range(1, 7);
    }

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static int StringToInt(string inputString)
    {
        return int.Parse(inputString);
    }

    public static int GetRandomNumberNoRepeat(int min, int max, List<int> collection)
    {
        // If all possible number already generated
        if (collection.Count == max - 1)
        {
            Debug.Log("All possible number already generated");
            return -1;
        }

        int randomNumber = GetRandomNumber(min, max);
        while (collection.Contains(randomNumber))
        {
            randomNumber = GetRandomNumber(min, max);
        }
        collection.Add(randomNumber);

        return randomNumber;
    }

    public static Color ConvertToColor(Color32 color)
    {
        Color newColor = new Color(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);

        return newColor;
    }
}
public static class Config
{
    public static string LADDER_TAG = "Ladder";
    public static string SNAKE_TAG = "Snake";

    public static string INPUT_FIELD_EMPTY = "Input Field cannot be empty!";
    public static string BOARD_TILE_NOT_VALID_MESSAGE = "The minimum amount of Row x Column are 2 x 2";
    public static string BOARD_SNAKE_LADDER_NOT_VALID_MESSAGE = "The minimum amount of Snake or Ladder are zero";
    public static string BOARD_CONFIG_NOT_VALID_MESSAGE = "The amount of Ladder and Snake should not exceed ";

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
                newColor = COLOR_RED;
                break;

            case COLOR.GREEN:
                newColor = COLOR_GREEN;
                break;

            case COLOR.BLUE:
                newColor = COLOR_BLUE;
                break;

            case COLOR.YELLOW:
                newColor = COLOR_YELLOW;
                break;

            case COLOR.PURPLE:
                newColor = COLOR_PURPLE;
                break;

            case COLOR.PINK:
                newColor = COLOR_PINK;
                break;

            default:
                newColor = COLOR_RED;
                break;
        }

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

public enum TILE_TYPE
{
    TILE,
    LADDER_TOP,
    LADDER_BOTTOM,
    SNAKE_HEAD,
    SNAKE_TAIL
}