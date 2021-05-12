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
