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
                Vector2 camToPos = camCurrentPos + (new Vector2(_mouseInitialPos.x - _mouseCurrentPos.x, _mouseInitialPos.y - _mouseCurrentPos.y) * _cameraPanSpeed);

                _cameraFollow.transform.position = SetCameraPanClamping(camToPos);
            }

            _mouseInitialPos = Input.mousePosition;
        }
    }

    public Vector2 SetCameraPanClamping(Vector2 position)
    {
        float posX = Mathf.Clamp(position.x, 0f, Board.Instance.RowCount);
        float posY = Mathf.Clamp(position.y, 0f, Board.Instance.ColCount);

        return new Vector2(posX, posY);
    }

    public void SetCameraFollow(Transform follow)
    {
        _cineCam.Follow = follow;
    }

    public void SetCameraLookAtPosition(Vector2 toPosition)
    {
        _cameraFollow.position = toPosition;
    }
}
