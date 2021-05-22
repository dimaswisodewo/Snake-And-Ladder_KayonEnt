using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private int _currentlyPlayingIndex = 0;
    
    [Range(1, 6)]
    public int _playerToSpawn = 2;

    [HideInInspector]
    public int numOfActivePlayer;
    public List<Player> players = new List<Player>();

    public int CurrentlyPlayingIndex { get { return _currentlyPlayingIndex; } }

    private void Awake()
    {
        numOfActivePlayer = _playerToSpawn;
    }

    public void InitializePlayer()
    {
        for (int i = 0; i < _playerToSpawn; i++)
        {
            InstantiatePlayer(i);
        }
    }

    public Player GetCurrentPlayingPlayer()
    {
        return players[_currentlyPlayingIndex];
    }

    public void SetNextPlayingPlayer()
    {
        // TODO: Refactor code dibawah, geleuh euy
        _currentlyPlayingIndex++;
        if (_currentlyPlayingIndex > players.Count - 1)
            _currentlyPlayingIndex = 0;

        while (players[_currentlyPlayingIndex].hasWin && numOfActivePlayer > 0)
        {
            _currentlyPlayingIndex++;
            if (_currentlyPlayingIndex > players.Count - 1)
                _currentlyPlayingIndex = 0;
        }
    }

    private void InstantiatePlayer(int index)
    {
        GameObject obj = Instantiate(_playerPrefab);
        obj.name = "Player_" + (index + 1);

        Player player = obj.GetComponent<Player>();
        player.SetSpriteColor((COLOR)index);
        players.Add(player);
    }

}
