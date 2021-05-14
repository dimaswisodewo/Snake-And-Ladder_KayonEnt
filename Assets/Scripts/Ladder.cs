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

    public void MovePlayerToTop(Player player, System.Action onMoveStart = null, System.Action onMoveFinish = null)
    {
        StartCoroutine(MovePlayerToTopCoroutine(player, onMoveStart, onMoveFinish));
    }

    private IEnumerator MovePlayerToTopCoroutine(Player player, System.Action onMoveStart = null, System.Action onMoveFinish = null)
    {
        onMoveStart?.Invoke();

        Vector2 to = Board.Instance.tiles[top].transform.position;
        Tweening.MoveTo(player.transform, to);

        while ((Vector2)player.transform.position != to)
            yield return null;

        onMoveFinish?.Invoke();
    }
}