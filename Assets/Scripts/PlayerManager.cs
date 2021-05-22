using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _PlayerPrefab;
    private int _CurrentlyPlayingIndex = 0;
    
    [Range(1, 6)]
    public int _PlayerToSpawn = 2;

    [HideInInspector]
    public int _NumOfActivePlayer;
    public List<Player> players = new List<Player>();

    public int CurrentlyPlayingIndex { get { return _CurrentlyPlayingIndex; } }

    private void Awake()
    {
        _NumOfActivePlayer = _PlayerToSpawn;
    }

    public void InitializePlayer()
    {
        for (int i = 0; i < _PlayerToSpawn; i++)
        {
            InstantiatePlayer(i);
        }
    }

    public Player GetCurrentPlayingPlayer()
    {
        return players[_CurrentlyPlayingIndex];
    }

    public void SetNextPlayingPlayer()
    {
        // TODO: Refactor code dibawah, geleuh euy
        _CurrentlyPlayingIndex++;
        if (_CurrentlyPlayingIndex > players.Count - 1)
            _CurrentlyPlayingIndex = 0;

        while (players[_CurrentlyPlayingIndex].hasWin && _NumOfActivePlayer > 0)
        {
            _CurrentlyPlayingIndex++;
            if (_CurrentlyPlayingIndex > players.Count - 1)
                _CurrentlyPlayingIndex = 0;
        }
    }

    private void InstantiatePlayer(int index)
    {
        GameObject obj = Instantiate(_PlayerPrefab);
        obj.name = "Player_" + (index + 1);

        Player player = obj.GetComponent<Player>();
        player.SetSpriteColor((COLOR)index);
        players.Add(player);
    }

}
