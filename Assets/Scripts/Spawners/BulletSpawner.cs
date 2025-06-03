using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Attacker _character;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _poolCapacity = 5;

    private ObjectPool<Bullet> _pool;
    private int _poolMaxSize = 100;

    private void OnEnable()
    {
        _character.Attacked += EnableBullet;
    }

    private void OnDisable()
    {
        _character.Attacked -= EnableBullet;
    }

    public virtual void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => CreateObject(),
            actionOnGet: (bullet) => Initialize(bullet),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            actionOnDestroy: (bullet) => DestroyObject(bullet),
            maxSize: _poolMaxSize);
    }

    public void EnableBullet()
    {
        _pool.Get();
    }

    public virtual void Initialize(Bullet bullet)
    {
        bullet.transform.position = _spawnPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.InitializeDirection(_character.transform.position.x, _spawnPoint.transform.position.x);
        bullet.gameObject.SetActive(true);
    }

    public virtual void ReleasedObject(Bullet bullet)
    {
        _pool.Release(bullet);
    }

    private Bullet CreateObject()
    {
        Bullet bullet = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
        bullet.EndedLifeTime += ReleasedObject;

        return bullet;
    }

    private void DestroyObject(Bullet bullet)
    {
        bullet.EndedLifeTime -= ReleasedObject;
        Destroy(bullet.gameObject);
    }
}
