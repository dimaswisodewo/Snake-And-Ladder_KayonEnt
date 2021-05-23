using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraFollow;
    [SerializeField] private CinemachineVirtualCamera _cineCam;

    private float _cameraPanSpeed = 0.006f;
    private Vector2 _mouseInitialPos = new Vector2();
    private Vector2 _mouseCurrentPos = new Vector2();

    private void Awake()
    {
        SetCameraFollow(_cameraFollow);
    }

    private void Update()
    {
        CameraPan();
    }

    private void CameraPan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseInitialPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _mouseCurrentPos = Input.mousePosition;

            if (_mouseInitialPos != _mouseCurrentPos)
            {
                Vector2 camCurrentPos = _cameraFollow.transform.position;
                _cameraFollow.transform.position = camCurrentPos + (new Vector2(_mouseInitialPos.x - _mouseCurrentPos.x, _mouseInitialPos.y - _mouseCurrentPos.y) * _cameraPanSpeed);
            }

            _mouseInitialPos = Input.mousePosition;
        }
    }

    // TODO clamp camera follow position based on board size
    //public void SetCameraPanClamping()
    //{

    //}

    public void SetCameraFollow(Transform follow)
    {
        _cineCam.Follow = follow;
    }

    public void SetCameraLookAtPosition(Vector2 toPosition)
    {
        _cameraFollow.position = toPosition;
    }
}
