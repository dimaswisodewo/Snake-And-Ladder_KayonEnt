using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int top;
    public int bottom;

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

    public void SetLookAt(Transform lookAt)
    {
        Transform ladderObject = null;
        foreach (Transform child in transform)
        {
            if (child.CompareTag(Config.LADDER_TAG))
            {
                ladderObject = child;
                break;
            }
        }

        // Set rotation
        ladderObject.up = lookAt.position - transform.position;

        // Set sprite height and position
        float distance = Vector3.Distance(ladderObject.position, lookAt.position);
        ladderObject.GetComponent<SpriteRenderer>().size = new Vector2(1, distance);
        ladderObject.transform.position += (lookAt.position - transform.position) / 2f;
    }
}
