using Assets.Codebase.Enemy.Interfaces;
using UnityEngine;

public class InterceptMovement<T>
    where T : MonoBehaviour, IMovable
{
    private T _actor;
    private Transform _targetTransform;

    public InterceptMovement(T actor, Transform targetTransform)
    {
        _actor = actor;
        _targetTransform = targetTransform;
    }

    public Vector3 GetInterceptMovement(float targetSpeed)
    {
        Vector3 targetPlanarPosition = GetPlanarPosition(_targetTransform);
        Vector3 actorPlanarPosition = GetPlanarPosition(_actor.transform);

        float predictionFactor = CalculatePredictionFactor(actorPlanarPosition, targetPlanarPosition, _actor.MovementSpeed);

        Vector3 targetPredictedPosition = targetPlanarPosition + _targetTransform.forward * targetSpeed * predictionFactor * Time.deltaTime;

        return targetPredictedPosition - actorPlanarPosition;
    }

    private Vector3 GetPlanarPosition(Transform transform)
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

    private float CalculatePredictionFactor(Vector3 actorPosition, Vector3 targetPosition, float movementSpeed)
    {
        float distance = (targetPosition - actorPosition).sqrMagnitude;

        return distance / (movementSpeed);
    }
}
