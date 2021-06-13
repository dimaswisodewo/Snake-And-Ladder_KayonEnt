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

    public static int ClampNumber(int inputNumber, int min, int max)
    {
        return Mathf.Clamp(inputNumber, min, max);
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

    public static int MIN_PLAYER_COUNT = 2;
    public static int MAX_PLAYER_COUNT = 6;

    public static string INPUT_FIELD_EMPTY = "Eh, masih ada yang belum diisi tuh!";
    public static string BOARD_TILE_NOT_VALID_MESSAGE = "Jumlah baris atau kolom paling sedikit setidaknya 2 ya!";
    public static string BOARD_SNAKE_LADDER_NOT_VALID_MESSAGE = "jumlah ular atau tangga paling sedikit setidaknya 0 ya!";
    public static string BOARD_CONFIG_NOT_VALID_MESSAGE = "Jumlah keseluruhan ular dan tangga jangan sampai lebih dari ";

    public static string SCENE_GAMEPLAY = "SnL_Gameplay";

    public static string TEXT_RED = "Merah";
    public static string TEXT_GREEN = "Hijau";
    public static string TEXT_BLUE = "Biru";
    public static string TEXT_YELLOW = "Kuning";
    public static string TEXT_PURPLE = "Ungu";
    public static string TEXT_PINK = "Merah Muda";

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

    public static string GetPlayerText(COLOR color)
    {
        string playerText = string.Empty;
        switch (color)
        {
            case COLOR.RED:
                playerText = TEXT_RED;
                break;

            case COLOR.GREEN:
                playerText = TEXT_GREEN;
                break;

            case COLOR.BLUE:
                playerText = TEXT_BLUE;
                break;

            case COLOR.YELLOW:
                playerText = TEXT_YELLOW;
                break;

            case COLOR.PURPLE:
                playerText = TEXT_PURPLE;
                break;

            case COLOR.PINK:
                playerText = TEXT_PINK;
                break;

            default:
                playerText = TEXT_RED;
                break;
        }

        return playerText;
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