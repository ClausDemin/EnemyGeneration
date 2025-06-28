using Assets.Codebase;
using Assets.Codebase.Enemy.Interfaces;
using Assets.Codebase.ObjectPool.Interfaces;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(AnimationStatusProvider))]
public abstract class AbstractEnemy : MonoBehaviour, IPooledInstance, IMovable
{
    [SerializeField, Min(0)] protected float _attackRadius;

    protected Target _target;

    private CharacterController _character;

    private AnimationStatusProvider _statusProvider;

    private InterceptMovement<AbstractEnemy> _movementBehavior;

    public event Action<IPooledInstance> Released;
    public event Action<IPooledInstance> Disposed;

    public bool IsFree => !isActiveAndEnabled;
    public bool CanMove { get; private set; }

    [field: SerializeField, Min(1)] public float MovementSpeed { get; private set; }

    public Vector3 Velocity => _character.velocity;

    protected virtual void Awake()
    {
        _character = GetComponent<CharacterController>();
        _statusProvider = GetComponent<AnimationStatusProvider>();
    }

    protected virtual void OnEnable()
    {
        _statusProvider.AnimationCompleted += OnSpawnAnimationEnd;

        StartCoroutine(Move());
    }

    protected virtual void OnDisable()
    {
        _statusProvider.AnimationCompleted -= OnSpawnAnimationEnd;

        CanMove = false;

        Released?.Invoke(this);
    }

    protected virtual void OnDestroy()
    {
        Disposed?.Invoke(this);
    }

    private void OnSpawnAnimationEnd()
    {
        CanMove = true;
    }

    public virtual IEnumerator Move()
    {
        while (isActiveAndEnabled)
        {
            if (CanMove && _target != null)
            {
                Vector3 movement = _movementBehavior.GetInterceptMovement(_target.MovementSpeed);

                transform.forward = movement;

                _character.Move(transform.forward * MovementSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    public void SetTarget(Target target)
    {
        _target = target;

        _movementBehavior = new InterceptMovement<AbstractEnemy>(this, _target.transform);
    }

    private float GetTargetPositionPrediction(Vector3 targetPosition)
    {
        float distance = (targetPosition - transform.position).sqrMagnitude;

        return distance / (MovementSpeed);
    }
}