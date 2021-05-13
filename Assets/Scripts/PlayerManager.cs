using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    private int _CurrentlyPlayingIndex = 0;
    [Range(1, 6)]
    public int _PlayerToSpawn = 2;
    public List<Player> activePlayers = new List<Player>();

    public void InitializePlayer()
    {
        for (int i = 0; i < _PlayerToSpawn; i++)
        {
            InstantiatePlayer(i);
        }
    }

    public Player GetCurrentPlayingPlayer()
    {
        return activePlayers[_CurrentlyPlayingIndex];
    }

    public void SetNextPlayingPlayer()
    {
        _CurrentlyPlayingIndex++;
        if (_CurrentlyPlayingIndex > activePlayers.Count - 1)
            _CurrentlyPlayingIndex = 0;
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
