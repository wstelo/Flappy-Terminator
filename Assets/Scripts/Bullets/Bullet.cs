using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BulletCollisionDetector), typeof(BulletMover))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3;

    private BulletCollisionDetector _detector;
    private BulletMover _bulletMover;
    private Coroutine _coroutine;

    public event Action <Bullet> EndedLifeTime;

    private void Awake()
    {
        _detector = GetComponent<BulletCollisionDetector>();
        _bulletMover = GetComponent<BulletMover>();
    }

    private void OnEnable()
    {
        _detector.CollisionDetected += DisableObject;
        _coroutine = StartCoroutine(StartCountingTime());
    }

    private void OnDisable()
    {
        _detector.CollisionDetected -= DisableObject;
        StopCoroutine(_coroutine);
    }

    private void DisableObject()
    {
        EndedLifeTime?.Invoke(this);
    }

    private IEnumerator StartCountingTime()
    {
        var wait = new WaitForSeconds(_lifetime);

        yield return wait;

        EndedLifeTime?.Invoke(this);
    }

    public void InitializeDirection(float characterPosition, float spawnPointPosition)
    {
        _bulletMover.SetDirection(spawnPointPosition - characterPosition);
    }
}
