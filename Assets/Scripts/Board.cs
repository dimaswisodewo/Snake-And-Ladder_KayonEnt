using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject _ladderPrefab;
    [SerializeField] private GameObject _snakePrefab;
    
    private int _rowCount = 10;
    private int _colCount = 10;
    private int _ladderCount = 5;
    private int _snakeCount = 5;
    private int _availableSlot;
    private int _allowedAmountOfTileComponents;
    
    [Header("Collections")]
    public List<Tile> tiles = new List<Tile>();
    public Queue<int[]> componentsSlot = new Queue<int[]>();
    
    private Queue<int[]> ladderSlot = new Queue<int[]>();
    private Queue<int[]> snakeSlot = new Queue<int[]>();
    private List<GameObject> ladders = new List<GameObject>();
    private List<GameObject> snakes = new List<GameObject>();

    public int RowCount { get { return _rowCount; } }
    public int ColCount {  get { return _colCount; } }

    // Singleton initialization
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitializeBoard()
    {
        if (tiles.Count > 0)
        {
            foreach (Tile tile in tiles)
            {
                ObjectPool.Instance.SendTileToPool(tile.gameObject);
            }
        }

        GenerateTiles();
        RandomizeBoardComponents();
    }

    public void RandomizeBoardComponents()
    {
        GenerateTileComponentsSlot();
        GenerateLadder();
        GenerateSnake();
    }

    public void FinishBoardComponentPlacement()
    {
        int count = ladderSlot.Count;
        for (int i = 0; i < count; i++)
        {
            int[] pairNum = ladderSlot.Dequeue();
            tiles[pairNum[0]].gameObject.AddComponent<Ladder>();
            tiles[pairNum[0]].tileType = TILE_TYPE.LADDER_BOTTOM;
            tiles[pairNum[1]].tileType = TILE_TYPE.LADDER_TOP;

            Ladder ladder = tiles[pairNum[0]].GetComponent<Ladder>();
            ladder.bottom = pairNum[0];
            ladder.top = pairNum[1];
        }

        count = snakeSlot.Count;
        for (int i = 0; i < count; i++)
        {
            int[] pairNum = snakeSlot.Dequeue();
            tiles[pairNum[1]].gameObject.AddComponent<Snake>();
            tiles[pairNum[1]].tileType = TILE_TYPE.SNAKE_HEAD;
            tiles[pairNum[0]].tileType = TILE_TYPE.SNAKE_TAIL;

            Snake snake = tiles[pairNum[1]].GetComponent<Snake>();
            snake.head = pairNum[1];
            snake.tail = pairNum[0];
        }
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

    private void GenerateTiles()
    {
        tiles.Clear();
        int iteration = 1;
        bool isReversed = false;
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _colCount; j++)
            {
                // Get new position for instantiated tile
                Vector2 tilePos;
                if (isReversed) tilePos = new Vector2((_colCount - 1) - j, i);
                else tilePos = new Vector2(j, i);

                GameObject obj = ObjectPool.Instance.GetTileFromPool();
                obj.transform.name = string.Concat("Tile_", iteration);
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
        componentsSlot.Clear();

        // Generate tile index pair for ladder or snake
        List<int> temp = new List<int>();
        while (componentsSlot.Count < _allowedAmountOfTileComponents)
        {
            int firstNum = MathUtility.GetRandomNumberNoRepeat(1, _availableSlot + 1, temp);
            int secondNum = MathUtility.GetRandomNumberNoRepeat(1, _availableSlot + 1, temp);
            int[] pairNum = new int[] { firstNum, secondNum };
            componentsSlot.Enqueue(pairNum);
        }
    }

    private void GenerateLadder()
    {
        ladderSlot.Clear();
        while (ladders.Count < _ladderCount)
        {
            ladders.Add(Instantiate(_ladderPrefab));
        }

        for (int i = 0; i < _ladderCount; i++)
        {
            int[] pairNum = componentsSlot.Dequeue();
            System.Array.Sort(pairNum);
            ladderSlot.Enqueue(pairNum);

            GameObject ladderObj = ladders[i];
            ladderObj.transform.position = tiles[pairNum[0]].transform.position;
            SetLookAtLadder(ladderObj.transform, tiles[pairNum[1]].transform);
        }
    }

    private void GenerateSnake()
    {
        snakeSlot.Clear();
        while (snakes.Count < _snakeCount)
        {
            snakes.Add(Instantiate(_snakePrefab));
        }

        for (int i = 0; i < _snakeCount; i++)
        {
            int[] pairNum = componentsSlot.Dequeue();
            System.Array.Sort(pairNum);
            snakeSlot.Enqueue(pairNum);

            GameObject snakeObj = snakes[i];            
            snakeObj.transform.position = tiles[pairNum[1]].transform.position;
            SetLookAtSnake(snakeObj.transform, tiles[pairNum[0]].transform);
        }
    }

    // TODO pindahin function buat set look at ke masing-masing script Ladder dan Snake
    public void SetLookAtLadder(Transform ladder, Transform lookAt)
    {
        // Set rotation
        ladder.up = lookAt.position - ladder.position;

        // Set sprite height and position
        float distance = Vector3.Distance(ladder.position, lookAt.position);
        ladder.GetComponent<SpriteRenderer>().size = new Vector2(1, distance);
        ladder.position += (lookAt.position - ladder.position) / 2f;
    }

    public void SetLookAtSnake(Transform snake, Transform lookAt)
    {
        // Set rotation
        snake.up = lookAt.position - snake.position;
        snake.Rotate(new Vector3(0f, 0f, 180f), Space.World);

        // Set sprite height and position
        float distance = Vector3.Distance(snake.position, lookAt.position);
        snake.GetComponent<SpriteRenderer>().size = new Vector2(1, distance);
        snake.position += (lookAt.position - snake.position) / 2f;
    }

    public void ResetBoardComponentPosition()
    {
        foreach (GameObject ladder in ladders)
            ladder.transform.position = ObjectPool.Instance._poolPosition;

        foreach (GameObject snake in snakes)
            snake.transform.position = ObjectPool.Instance._poolPosition;
    }

    public bool IsBoardConfigurationValid()
    {
        if (UIManager.Instance.IsBoardConfigFieldEmpty())
        {
            UIManager.Instance.SetBoardConfigNotValidText(Config.INPUT_FIELD_EMPTY);
            return false;
        }

        // Get max amount of snake and ladder allowed
        _rowCount = UIManager.Instance.GetRowCountInputFieldValue();
        _colCount = UIManager.Instance.GetColCountInputFieldValue();

        if (_rowCount < 2 || _colCount < 2)
        {
            UIManager.Instance.SetBoardConfigNotValidText(Config.BOARD_TILE_NOT_VALID_MESSAGE);
            return false;
        }

        _ladderCount = UIManager.Instance.GetLadderCountInputFieldValue();
        _snakeCount = UIManager.Instance.GetSnakeCountInputFieldValue();

        if (_ladderCount < 0 || _snakeCount < 0)
        {
            UIManager.Instance.SetBoardConfigNotValidText(Config.BOARD_SNAKE_LADDER_NOT_VALID_MESSAGE);
            return false;
        }

        _availableSlot = (_rowCount * _colCount) - 2;
        _availableSlot = _availableSlot % 2 == 0 ? _availableSlot : _availableSlot--;
        _allowedAmountOfTileComponents = _availableSlot / 2;

        int tileComponentsCount = _ladderCount + _snakeCount;

        if (tileComponentsCount > _allowedAmountOfTileComponents)
        {
            UIManager.Instance.SetBoardConfigNotValidText(Config.BOARD_CONFIG_NOT_VALID_MESSAGE + _allowedAmountOfTileComponents);
            return false;
        }

        return true;
    }
}
