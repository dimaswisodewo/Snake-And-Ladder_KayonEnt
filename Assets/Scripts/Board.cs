using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject _TilePrefab;
    [SerializeField] private GameObject _LadderPrefab;
    [SerializeField] private GameObject _SnakePrefab;

    [Header("Resolution")]
    [Min(2)]
    [SerializeField] private int _RowCount = 10;
    [Min(2)]
    [SerializeField] private int _ColCount = 10;

    [Header("Components")]
    [Min(0)]
    [SerializeField] private int _LadderCount = 5;
    [Min(0)]
    [SerializeField] private int _SnakeCount = 5;

    private GameObject _DummyObject;
    private int _AvailableSlot;
    private int _AllowedAmountOfTileComponents;
    
    [Header("Collections")]
    public List<Tile> tiles = new List<Tile>();
    public Queue<int[]> componentsSlot = new Queue<int[]>();

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
        // Get max amount of snake and ladder allowed
        _AvailableSlot = (_RowCount * _ColCount) - 2;
        _AvailableSlot = _AvailableSlot % 2 == 0 ? _AvailableSlot : _AvailableSlot--;
        _AllowedAmountOfTileComponents = _AvailableSlot / 2;

        if (!IsTileComponentsValueValid())
        {
            Debug.LogError("Amount of Ladder and Snake should not exceed " + _AllowedAmountOfTileComponents);
            return;
        }

        if (_DummyObject == null)
            _DummyObject = new GameObject();

        InstantiateTiles();
        GenerateTileComponentsSlot();
        GenerateLadder();
        GenerateSnake();

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

    private void GenerateTileComponentsSlot()
    {
        Debug.Log("Ladder Count: " + _LadderCount + ", Snake Count: " + _SnakeCount);

        // Generate tile index pair for ladder or snake
        List<int> temp = new List<int>();
        while (componentsSlot.Count < _AllowedAmountOfTileComponents)
        {
            int firstNum = MathUtility.GetRandomNumberNoRepeat(1, _AvailableSlot + 1, temp);
            int secondNum = MathUtility.GetRandomNumberNoRepeat(1, _AvailableSlot + 1, temp);
            int[] pairNum = new int[] { firstNum, secondNum };
            componentsSlot.Enqueue(pairNum);
        }
    }

    private void GenerateLadder()
    {
        for (int i = 0; i < _LadderCount; i++)
        {
            int[] pairNum = componentsSlot.Dequeue();
            System.Array.Sort(pairNum);
            tiles[pairNum[0]].gameObject.AddComponent<Ladder>();
            tiles[pairNum[0]].tileType = TILE_TYPE.LADDER_BOTTOM;
            tiles[pairNum[1]].tileType = TILE_TYPE.LADDER_TOP;

            Instantiate(_LadderPrefab, tiles[pairNum[0]].transform);

            Ladder ladder = tiles[pairNum[0]].GetComponent<Ladder>();
            ladder.bottom = pairNum[0];
            ladder.top = pairNum[1];
            ladder.SetLookAt(tiles[ladder.top].transform);
        }
    }

    private void GenerateSnake()
    {
        for (int i = 0; i < _SnakeCount; i++)
        {
            int[] pairNum = componentsSlot.Dequeue();
            System.Array.Sort(pairNum);
            tiles[pairNum[1]].gameObject.AddComponent<Snake>();
            tiles[pairNum[1]].tileType = TILE_TYPE.SNAKE_HEAD;
            tiles[pairNum[0]].tileType = TILE_TYPE.SNAKE_TAIL;

            Instantiate(_SnakePrefab, tiles[pairNum[1]].transform);

            Snake snake = tiles[pairNum[1]].GetComponent<Snake>();
            snake.head = pairNum[1];
            snake.tail = pairNum[0];
            snake.SetLookAt(tiles[snake.tail].transform);
        }
    }

    private bool IsTileComponentsValueValid()
    {
        int tileComponentsCount = _LadderCount + _SnakeCount;
        return (tileComponentsCount <= _AllowedAmountOfTileComponents);
    }
}
