using Assets.Codebase.Enemy.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement<T>
    where T: MonoBehaviour, IMovable
{
    private List<Vector3> _path;
    private T _actor;

    public WaypointMovement(T actor, List<Vector3> path) 
    { 
        _actor = actor;
        _path = path;
    }

    private IEnumerable<Vector3> GetNextPoint()
    {
        if (_path.Count > 0)
        {
            int currentPointIndex = 0;

            while (_actor.CanMove)
            {
                yield return _path[currentPointIndex % (_path.Count)];

                currentPointIndex++;
            }
        }
        else 
        { 
            yield return _actor.transform.position;
        }

    }

    public IEnumerable<Vector3> GetMovement()
    {
        foreach (Vector3 waypoint in GetNextPoint())
        {
            while (IsTargetReached(waypoint) == false)
            {
                Vector3 actorPlanarPosition = GetPlanarPosition(_actor.transform);

                yield return waypoint - actorPlanarPosition;
            }
        }
    }

    private Vector3 GetPlanarPosition(Transform transform)
    {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

    private bool IsTargetReached(Vector3 target) 
    {
        float epsilon = 0.5f;

        Vector2 planarPosition = new Vector2(_actor.transform.position.x, _actor.transform.position.z);
        Vector2 planarTarget = new Vector2(target.x, target.z);

        return (planarPosition - planarTarget).sqrMagnitude <= epsilon;
    }
}
