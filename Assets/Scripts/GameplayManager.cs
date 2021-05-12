using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Board _Board;
    [SerializeField] private CameraController _CamController;
    [SerializeField] private PlayerManager _PlayerManager;
    [SerializeField] private Transform _CameraFollow;

    private void Awake()
    {
        InitializeGameComponent();
    }

    private void InitializeGameComponent()
    {
        _Board.InitializeBoard();

        // Centering camera follow position relative to Board
        Vector2 camFollowPos = new Vector2((_Board.rowCount / 2) -
            0.5f, (_Board.colCount / 2) - 0.5f);
        _CamController.SetCameraFollowPosition(camFollowPos);
    }

}
