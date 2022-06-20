using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    [SerializeField] private Vector2 _center;
    [SerializeField] private float _radius = 60f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Direction to centre
        Vector2 centerOffset = _center - (Vector2)(agent.transform.position);

        float t = centerOffset.magnitude / _radius;

        if(t < 0.9f) //If we are between the centre and 90% of the radius
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
