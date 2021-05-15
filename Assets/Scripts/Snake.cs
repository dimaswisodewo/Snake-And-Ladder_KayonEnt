using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public int head;
    public int tail;

    public void MovePlayerToTail(Player player, System.Action onMoveStart = null, System.Action onMoveFinish = null)
    {
        StartCoroutine(MovePlayerToTailCoroutine(player, onMoveStart, onMoveFinish));
    }

    private IEnumerator MovePlayerToTailCoroutine(Player player, System.Action onMoveStart = null, System.Action onMoveFinish = null)
    {
        onMoveStart?.Invoke();

        Vector2 to = Board.Instance.tiles[tail].transform.position;
        Tweening.MoveTo(player.transform, to);

        while ((Vector2)player.transform.position != to)
            yield return null;

        onMoveFinish?.Invoke();
    }

    public void SetLookAt(Transform lookAt)
    {
        Transform snakeObject = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag(Config.SNAKE_TAG))
            {
                snakeObject = child;
                break;
            }
        }

        snakeObject.up = lookAt.position - transform.position;
        snakeObject.Rotate(new Vector3(0f, 0f, 180f), Space.World);
    }
}
