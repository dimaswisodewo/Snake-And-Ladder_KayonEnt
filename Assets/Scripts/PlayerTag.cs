using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    public void SetPlayerTagPosition(Vector2 toPosition)
    {
        transform.position = toPosition;
        Debug.Log("Set player tag position to " + toPosition.x + "-" + toPosition.y);
    }
}
