using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private int _PlayerCount = 2;

    public void InitializePlayer()
    {
        for (int i = 0; i < _PlayerCount; i++)
        {
            InstantiatePlayer();
        }
    }

    private void InstantiatePlayer()
    {
        GameObject player = Instantiate(_PlayerPrefab, transform);
    }
}
