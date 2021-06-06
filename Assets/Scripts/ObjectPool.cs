using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Object Prefabs")]
    [SerializeField] private GameObject _tilePrefab;

    [Header("Configuration")]
    [SerializeField] private int _amount = 500;

    [HideInInspector]
    public Vector2 _poolPosition = new Vector2(-999, -999);

    private Stack<GameObject> _tilePool = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InstantiateObjectPool();
    }

    public void InstantiateObjectPool()
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject go = Instantiate(_tilePrefab);
            SendTileToPool(go);
        }
    }

    public void SendTileToPool(GameObject go)
    {
        _tilePool.Push(go);
        go.transform.position = _poolPosition;
        go.SetActive(false);
    }

    public GameObject GetTileFromPool()
    {
        if (_tilePool.Count == 100)
        {
            Debug.Log("pool almost empty, instantiating more object");
            InstantiateObjectPool();
        }

        GameObject go = _tilePool.Pop();
        go.SetActive(true);

        return go;
    }

}
