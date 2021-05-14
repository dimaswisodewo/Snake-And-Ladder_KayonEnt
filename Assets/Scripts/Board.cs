using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    [SerializeField] private GameObject _TilePrefab;
    
    [Min(2f)]
    [SerializeField] private int _RowCount = 10;
    
    [Min(2f)]
    [SerializeField] private int _ColCount = 10;

    [SerializeField] private int _LadderCount = 5;
    [SerializeField] private int _SnakeCount = 5;

    private GameObject _DummyObject;
    
    public List<Tile> tiles = new List<Tile>();

    public List<int> ladderTop = new List<int>();
    public List<int> ladderBottom = new List<int>();

    public int rowCount { get { return _RowCount; } }
    public int colCount {  get { return _ColCount; } }

    // Singleton initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitializeBoard()
    {
        if (_DummyObject == null)
            _DummyObject = new GameObject();

        InstantiateTiles();

        GenerateLadderIndex();
        Destroy(_DummyObject);
    }

    // TODO: Refactor function ini kalo sempet
    public Queue<Vector2> GetStepQueue(Player player, int currentTilePos, int diceNumber)
    {
        Queue<Vector2> stepQueue = new Queue<Vector2>();
        int maxTileIndex = tiles.Count - 1;
        int numOfStep = diceNumber;
        int stepIndex = currentTilePos;
        bool isReversed = false;

        while (numOfStep > 0)
        {
            if (isReversed)
            {
                if ((stepIndex - 1) >= 0)
                {
                    stepIndex -= 1;
                }
                else
                {
                    isReversed = false;
                    continue;
                }
            }

            if (!isReversed)
            {
                if ((stepIndex + 1) <= maxTileIndex)
                {
                    stepIndex += 1;
                }
                else
                {
                    isReversed = true;
                    continue;
                }
            }

            stepQueue.Enqueue(tiles[stepIndex].transform.position);
            numOfStep--;
        }

        // TODO: Misahin set tilePosition dari function ini
        player.tilePosition = stepIndex; // get destination tile index
        return stepQueue;
    }

    private void InstantiateTiles()
    {
        int iteration = 1;
        bool isReversed = false;
        for (int i = 0; i < _RowCount; i++)
        {
            GameObject row = Instantiate(_DummyObject, transform);
            row.transform.name = "Row_" + i;
            row.transform.position = new Vector2(0, i);

            for (int j = 0; j < _ColCount; j++)
            {
                // Get new position for instantiated tile
                Vector2 tilePos;
                if (isReversed) tilePos = new Vector2((_ColCount - 1) - j, i);
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

    private void InstantiateLadder()
    {

    }

    private void InstantiateSnake()
    {

    }

    private void GenerateLadderIndex()
    {
        Debug.Log("tiles count: " + tiles.Count);

        int minBottom = 1;
        int maxBottom = tiles.Count - _ColCount;

        int finishIndex = tiles.Count - 1;
        int maxTop = finishIndex - 1;
        int minTop = _ColCount;
        
        while (true)
        {
            if (ladderBottom.Count >= _LadderCount || ladderTop.Count >= _LadderCount)
                break;

            int bottomIndex = MathUtility.GetRandomNumberNoRepeat(minBottom, maxBottom, ladderBottom);
            ladderBottom.Add(bottomIndex);

            int topIndex = MathUtility.GetRandomNumberNoRepeat(minTop, maxTop, ladderTop);
            while (topIndex <= bottomIndex || IsOnTheSameRow(topIndex, bottomIndex))
            {
                topIndex = MathUtility.GetRandomNumberNoRepeat(minTop, maxTop, ladderTop);
            }
            ladderTop.Add(topIndex);
        }
    }

    private bool IsOnTheSameRow(int i, int j)
    {
        return (tiles[i].transform.position.y == tiles[j].transform.position.y);
    }

}
