using System.Collections;
using UnityEngine;

public class Ghost : AbstractEnemy
{
    [SerializeField, Min(0)] protected float _lifeTime;

    public bool IsMoving { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (isActiveAndEnabled)
        {
            if (CanAttack())
            {
                yield return StartCoroutine(DieOverTime(_lifeTime));
            }

            yield return null;
        }
    }

    private IEnumerator DieOverTime(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);

        gameObject.SetActive(false);
    }

    private bool CanAttack()
    {
        return _target != null && (_target.transform.position - transform.position).sqrMagnitude < _attackRadius;
    }
}
