using Assets.Codebase;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Animator), typeof(AnimationStatusProvider))]
public class EnemyView : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    private AnimationStatusProvider _statusProvider;

    private string _speedXName = "SpeedX";
    private string _speedZName = "SpeedZ";
    private string _screamTriggerName = "Scream";

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _statusProvider = GetComponent<AnimationStatusProvider>();
    }

    private void OnEnable()
    {
        _statusProvider.AnimationCompleted += OnSpawnAnimationEnd;
    }

    private void OnDisable()
    {
        _statusProvider.AnimationCompleted -= OnSpawnAnimationEnd;
    }

    private void Update()
    {
        Vector3 velocity = _enemy
            .transform.InverseTransformDirection(_enemy.Velocity)
            .normalized;

        _animator.SetFloat(_speedXName, velocity.x);
        _animator.SetFloat(_speedZName, velocity.z);
    }

    private void OnSpawnAnimationEnd() 
    {
        _animator.SetTrigger(_screamTriggerName);
    }
}
