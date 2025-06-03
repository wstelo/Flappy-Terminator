using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _offsetFromPlayerX = 20;
    [SerializeField] private float _minSpawnPointY = -5;
    [SerializeField] private float _maxSpawnPointY = 9;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private float _spawnTime = 3;
    [SerializeField] private Player _player;
    [SerializeField] private Character _prefab;

    private Quaternion _rotation = Quaternion.Euler(0, 180, 0);
    private ObjectPool<Character> _pool;
    private int _poolMaxSize = 15;

    public event Action EnemyReleased;

    public virtual void Awake()
    {
        _pool = new ObjectPool<Character>(
            createFunc: () => CreateObject(),
            actionOnGet: (enemy) => Initialize(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (enemy) => DestroyObject(enemy),
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(EnableObject());
    }

    public IEnumerator EnableObject()
    {
        while (enabled)
        {
            var wait = new WaitForSeconds(_spawnTime);

            yield return wait;

            _pool.Get();
        }
    }

    public virtual void Initialize(Character enemy)
    {
        enemy.transform.position = GetRandomSpawnPoint();
        enemy.gameObject.SetActive(true);
    }

    public virtual void ReleasedObject(Character enemy )
    {
        EnemyReleased?.Invoke();
        _pool.Release(enemy);
    }

    private Character CreateObject()
    {
        Character enemy = Instantiate(_prefab, GetRandomSpawnPoint(), _rotation);
        enemy.Destroyed += ReleasedObject;

        return enemy;
    }

    private void DestroyObject(Character enemy)
    {
        enemy.Destroyed -= ReleasedObject;
        Destroy(enemy.gameObject);
    }

    private Vector2 GetRandomSpawnPoint()
    {
        Vector2 spawnPoint = new Vector2(_player.transform.position.x + _offsetFromPlayerX, UnityEngine.Random.Range(_minSpawnPointY, _maxSpawnPointY));

        return spawnPoint;
    }
}
