using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;

    [Range(1, 6)]
    [SerializeField] private int _PlayerCount = 2;

    public void InitializePlayer()
    {
        for (int i = 0; i < _PlayerCount; i++)
        {
            InstantiatePlayer(i);
        }
    }

    private void InstantiatePlayer(int index)
    {
        GameObject player = Instantiate(_PlayerPrefab, transform);
        player.name = "Player_" + (index + 1);
        player.GetComponent<Player>().SetSpriteColor((COLOR)index);
    }

}
