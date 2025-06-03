using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private float _speed;
    [SerializeField] private float _force;
    [SerializeField] private float _rotationSpeed;

    private float _rotationX = 0;
    private float _rotationY = 0;

    private Rigidbody2D _rigidbody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _maxRotation = Quaternion.Euler(_rotationX, _rotationY, _maxRotationZ);
        _minRotation = Quaternion.Euler(_rotationX, _rotationY, _minRotationZ);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_speed, _force);
        transform.rotation = _maxRotation;
    }
}
