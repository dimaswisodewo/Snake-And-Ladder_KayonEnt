using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Tweening
{
    private static float _JumpPower = 1f;
    private static float _Duration = 0.3f;
    private static int _JumpNum = 1;

    public static void JumpTo(Transform transform, Vector2 endValue)
    {
        transform.DOJump(endValue, _JumpPower, _JumpNum, _Duration);
    }
}
