using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private int _currentlyPlayingIndex = 0;
    public Queue<PlayerData> playerDatas = new Queue<PlayerData>();

    [HideInInspector]
    public int playerToSpawn = 2;

    [HideInInspector]
    public int numOfActivePlayer;
    public List<Player> players = new List<Player>();

    public int CurrentlyPlayingIndex { get { return _currentlyPlayingIndex; } }

    public void InitializePlayer()
    {
        for (int i = 0; i < playerToSpawn; i++)
        {
            InstantiatePlayer(i);
        }

        numOfActivePlayer = playerToSpawn;
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
        obj.name = string.Concat("Player_", (index + 1));

        PlayerData playerData = playerDatas.Dequeue();

        Player player = obj.GetComponent<Player>();
        player.SetSpriteColor(playerData.playerColor);
        player.playerName = playerData.playerName;

        players.Add(player);
    }

}

public struct PlayerData
{
    public string playerName;
    public Color playerColor;
}