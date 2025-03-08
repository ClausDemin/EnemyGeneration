using Assets.Codebase;
using Assets.Codebase.ObjectPool.Interfaces;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AnimationStatusProvider))]
public class Enemy : MonoBehaviour, IPooledInstance
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _lifeTime;

    private Rigidbody _rigidbody;

    private AnimationStatusProvider _callbackReceiver;

    public event Action<IPooledInstance> Released;
    public event Action<IPooledInstance> Disposed;

    public bool IsFree => isActiveAndEnabled;
    public bool CanMove { get; private set; }

    public Vector3 Velocity => _rigidbody.velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _callbackReceiver = GetComponent<AnimationStatusProvider>();
    }

    private void OnEnable()
    {
        _callbackReceiver.AnimationCompleted += OnSpawnAnimationEnd;

        StartCoroutine(DieOverTime());
    }

    private void OnDisable()
    {
        _callbackReceiver.AnimationCompleted -= OnSpawnAnimationEnd;

        CanMove = false;
    }

    private void OnDestroy()
    {
        Disposed?.Invoke(this);
    }

    public void MoveDirection(Vector3 direction)
    {
        LookAt(direction);
        StartCoroutine(Move());
    }

    private void OnSpawnAnimationEnd()
    {
        CanMove = true;
    }

    private IEnumerator Move()
    {
        while (isActiveAndEnabled)
        {
            while (CanMove)
            {
                _rigidbody.velocity = transform.forward.normalized * _movementSpeed;

                yield return null;
            }

            yield return null;
        }
    }

    private void LookAt(Vector3 destination)
    {
        transform.forward = destination;
    }

    private IEnumerator DieOverTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        gameObject.SetActive(false);

        Released?.Invoke(this);
    }
}