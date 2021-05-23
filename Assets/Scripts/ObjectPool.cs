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

    private Stack<GameObject> _objectPool = new Stack<GameObject>();
    private Vector2 _poolPosition = new Vector2(-999, -999);

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
            SendToPool(go);
        }
    }

    public void SendToPool(GameObject go)
    {
        _objectPool.Push(go);
        go.transform.position = _poolPosition;
        go.SetActive(false);
    }

    public GameObject GetFromPool()
    {
        if (_objectPool.Count == 100)
        {
            Debug.Log("pool almost empty, instantiating more object");
            InstantiateObjectPool();
        }

        GameObject go = _objectPool.Pop();
        go.SetActive(true);

        return go;
    }
}
