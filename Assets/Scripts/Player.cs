using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public string playerName;
    public int tilePosition = 0;
    public int steps = 0;
    public bool hasWin;

    public void SetSpriteColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public Color GetSpriteColor()
    {
        return spriteRenderer.color;
    }

    public void JumpStepByStep(Queue<Vector2> stepQueue, System.Action onJumpStart = null, System.Action onJumpFinish = null)
    {
        StartCoroutine(JumpProgressively(stepQueue, onJumpStart, onJumpFinish));
    }

    private IEnumerator JumpProgressively(Queue<Vector2> stepQueue, System.Action onJumpStart = null, System.Action onJumpFinish = null)
    {
        onJumpStart?.Invoke();

        while (stepQueue.Count > 0)
        {
            Vector2 to = stepQueue.Dequeue();
            yield return StartCoroutine(JumpToNextTile(to));
        }

        onJumpFinish?.Invoke();
    }

    private IEnumerator JumpToNextTile(Vector2 to)
    {
        Tweening.JumpTo(transform, to);

        while ((Vector2)transform.position != to)
            yield return null;
    }

}
