using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _CameraFollow;
    [SerializeField] private CinemachineVirtualCamera _CineCam;

    private void Awake()
    {
        SetCameraFollow(_CameraFollow);
    }

    public void SetCameraFollow(Transform follow)
    {
        _CineCam.Follow = follow;
    }

    public void SetCameraFollowPosition(Vector2 toPosition)
    {
        _CameraFollow.position = toPosition;
    }
}
