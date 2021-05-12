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
        for (int i = 0; i < tileCount; i++)
        {
            GameObject row = Instantiate(_DummyObject, transform);
            row.transform.name = "X" + i;
            row.transform.position = new Vector2(0, i);

            for (int j = 0; j < tileCount; j++)
            {
                GameObject obj = Instantiate(_TilePrefab, row.transform);
                obj.transform.name = "Y" + j;
                obj.transform.position = new Vector2(j, i);

                Tile tile = obj.GetComponent<Tile>();
                tile.SetText(iteration.ToString());
                tiles.Add(tile);

                iteration++;
            }
        }
        
    }

}
