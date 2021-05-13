using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _TilePrefab;
    [SerializeField] private Vector2 tileCount = new Vector2(10, 10);
    private GameObject _DummyObject;
    public List<Tile> tiles = new List<Tile>();
    public int rowCount { get { return (int)tileCount.x; } }
    public int colCount {  get { return (int)tileCount.y; } }

    public void InitializeBoard()
    {
        if (_DummyObject == null)
            _DummyObject = new GameObject();

        InstantiateTiles();
        Destroy(_DummyObject);
    }

    public Queue<Vector2> GetStepQueue(int currentTilePos, int diceNumber)
    {
        Queue<Vector2> stepQueue = new Queue<Vector2>();
        int maxTileIndex = tiles.Count - 1;
        int tileDestinationIndex = currentTilePos + diceNumber;

        // When player overlap the finish tile and moving backward
        if (tileDestinationIndex > maxTileIndex)
        {
            int remainder = tileDestinationIndex - maxTileIndex;
            tileDestinationIndex = maxTileIndex - remainder;

            for (int i = currentTilePos + 1; i <= maxTileIndex; i++)
                stepQueue.Enqueue(tiles[i].transform.position);
            for (int i = maxTileIndex - 1; i >= tileDestinationIndex; i--)
                stepQueue.Enqueue(tiles[i].transform.position);
        }
        else
        {
            for (int i = currentTilePos + 1; i <= tileDestinationIndex; i++)
                stepQueue.Enqueue(tiles[i].transform.position);
        }
        
        return stepQueue;
    }

    private void InstantiateTiles()
    {
        int iteration = 1;
        bool isReversed = false;
        for (int i = 0; i < tileCount.y; i++)
        {
            GameObject row = Instantiate(_DummyObject, transform);
            row.transform.name = "Row_" + i;
            row.transform.position = new Vector2(0, i);

            for (int j = 0; j < tileCount.x; j++)
            {
                // Get new position for instantiated tile
                Vector2 tilePos;
                if (isReversed) tilePos = new Vector2((tileCount.x - 1) - j, i);
                else tilePos = new Vector2(j, i);

                GameObject obj = Instantiate(_TilePrefab, row.transform);
                obj.transform.name = "Tile_" + iteration;
                obj.transform.position = tilePos;

                Tile tile = obj.GetComponent<Tile>();
                tile.SetText(iteration.ToString());
                tiles.Add(tile);

                iteration++;
            }

            isReversed = !isReversed;
        }
    }

}
