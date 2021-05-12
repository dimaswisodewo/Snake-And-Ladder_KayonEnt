using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    
    [Range(1, 6)]
    public int _PlayerCount = 2;
    public List<Player> activePlayers = new List<Player>();

    public void InitializePlayer()
    {
        for (int i = 0; i < _PlayerCount; i++)
        {
            InstantiatePlayer(i);
        }
    }

    public Player GetCurrentPlayingPlayer(int index)
    {
        return activePlayers[index];
    }

    private void InstantiatePlayer(int index)
    {
        GameObject obj = Instantiate(_PlayerPrefab, transform);
        obj.name = "Player_" + (index + 1);

        Player player = obj.GetComponent<Player>();
        player.SetSpriteColor((COLOR)index);
        activePlayers.Add(player);
    }

}
