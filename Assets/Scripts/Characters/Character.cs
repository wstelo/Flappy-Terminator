using System;
using UnityEngine;

[RequireComponent(typeof(CharacterCollisionDetector))]

public abstract class Character : MonoBehaviour
{
    private CharacterCollisionDetector _collisionHandler;

    public event Action <Character> Destroyed;

    public virtual void Awake()
    {
        _collisionHandler = GetComponent<CharacterCollisionDetector>();
    }

    public virtual void OnEnable()
    {
        _collisionHandler.CollisionDetected += DisableObject;
    }

    public virtual void OnDisable()
    {
        _collisionHandler.CollisionDetected -= DisableObject;
    }

    public void DisableObject()
    {
        Destroyed?.Invoke(this);
    }
}
