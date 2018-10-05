using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour
{
    public BGCcMath path;

    #region PRIVATE_VARIABLES
    Move move;
	SteeringSeek seek;

    Vector3 closestPoint;
    float accumulatedDistanceRatio = 0.0f;

    float minDistance = 0.05f;
    float distanceRatio = 0.05f;
    #endregion

    void Start()
    {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
        float distance;
        closestPoint = path.CalcPositionByClosestPoint(transform.position, out distance);
      
        accumulatedDistanceRatio = distance / path.GetDistance();
    }
	
	void Update() 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path
        Vector3 target = closestPoint - transform.position;
        float targetDistance = target.magnitude;

        if (targetDistance < minDistance)
        {
            Debug.Log("New point!");

            accumulatedDistanceRatio += distanceRatio;

            if (accumulatedDistanceRatio > 1.0f)
                accumulatedDistanceRatio = 0.0f;

            closestPoint = path.CalcPositionByDistanceRatio(accumulatedDistanceRatio);
        }
        else
            seek.Steer(closestPoint);
	}

	void OnDrawGizmosSelected() 
	{

        if (isActiveAndEnabled)
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.green;
            // Useful if you draw a sphere were on the closest point to the path
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(closestPoint, 1.0f);
    }
}
