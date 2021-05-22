using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraLookAt;
    [SerializeField] private CinemachineVirtualCamera _cineCam;

    private void Awake()
    {
        SetCameraLookAt(_cameraLookAt);
    }

    public void SetCameraLookAt(Transform lookAt)
    {
        _cineCam.LookAt = lookAt;
    }

    public void SetCameraLookAtPosition(Vector2 toPosition)
    {
        _cameraLookAt.position = toPosition;
    }
}
