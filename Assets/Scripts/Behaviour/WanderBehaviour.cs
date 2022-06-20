using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behaviour/Wander")]
public class WanderBehaviour : FilteredFlockBehaviour
{
    private Path _path;
    private int _currentWaypoint = 0;

    //private Vector2 _waypointDirection = Vector2.zero;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (_path == null)
        {
            FindPath(agent, context);
        }

        return FollowPath(agent);
    }
    private Vector2 FollowPath(FlockAgent agent)
    {
        if (_path == null) return Vector2.zero;

        Vector3 waypointDirection;

        if (WaypointInRadius(agent, _currentWaypoint, out waypointDirection))
        {
            _currentWaypoint++;
            if (_currentWaypoint >= _path.waypoints.Count)
            {
                _currentWaypoint = 0;
            }
            return Vector2.zero;
        }
        return waypointDirection.normalized;
    }

    public bool WaypointInRadius(FlockAgent agent, int currentWaypoint, out Vector3 waypointDirection)
    {
        waypointDirection = (Vector2)(_path.waypoints[currentWaypoint].position - agent.transform.position);

        if (waypointDirection.magnitude < _path.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FindPath(FlockAgent agent, List<Transform> context)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return;
        }

        int randomPath = Random.Range(0, filteredContext.Count);
        _path = filteredContext[randomPath].GetComponentInParent<Path>();

        
    }
}
