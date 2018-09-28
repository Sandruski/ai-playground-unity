using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : MonoBehaviour {

	public float time_to_target = 0.25f;

	Move move;
	Move target_move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (target_move)
        {
            // TODO 5: First come up with your ideal velocity
            // then accelerate to it.

            Vector3 targetVel = target_move.movement;
            Vector3 newAcceleration = targetVel - move.movement;

            float distanceToTarget = Vector3.Distance(transform.position, target_move.transform.position);
            newAcceleration *= distanceToTarget * time_to_target;

            /*
            if (distanceToTarget < slow_distance)
            {
                Vector3 idealVel = currVel.normalized * distanceToTarget * time_to_target;

                if (distanceToTarget < min_distance)
                    idealVel = Vector3.zero;

                newAcceleration = idealVel - move.movement;
            }
            */

            move.AccelerateMovement(newAcceleration);
        }
	}
}
