using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _CameraLookAt;
    [SerializeField] private CinemachineVirtualCamera _CineCam;

    private void Awake()
    {
        SetCameraLookAt(_CameraLookAt);
    }

    public void SetCameraLookAt(Transform lookAt)
    {
        _CineCam.LookAt = lookAt;
    }

    public void SetCameraLookAtPosition(Vector2 toPosition)
    {
        _CameraLookAt.position = toPosition;
    }
}
