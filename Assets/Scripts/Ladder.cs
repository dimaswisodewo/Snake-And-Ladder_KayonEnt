using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int top;
    public int bottom;

    public int GetLadderTopIndex()
    {
        return top;
    }

    public void MovePlayerToTop(Player player)
    {
        //Transform ladderTop = Board.Instance.tiles[top].transform;
        //transform.position
    }
}
