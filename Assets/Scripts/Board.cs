using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int tileCount = 10;
    [SerializeField] private GameObject _TilePrefab;

    private GameObject _DummyObject;

    public List<Tile> tiles = new List<Tile>();

    private void Awake()
    {
        _DummyObject = new GameObject();

        InitializeBoard();
    }

    private void InitializeBoard()
    {
        int iteration = 1;
        bool isReversed = false;
        for (int i = 0; i < tileCount; i++)
        {
            GameObject row = Instantiate(_DummyObject, transform);
            row.transform.name = "Row_" + i;
            row.transform.position = new Vector2(0, i);

            for (int j = 0; j < tileCount; j++)
            {
                // Get new position for instantiated tile
                Vector2 tilePos;
                if (isReversed) tilePos = new Vector2((tileCount - 1) - j, i);
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
