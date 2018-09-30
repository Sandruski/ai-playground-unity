using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : MonoBehaviour
{
	public float time_to_target = 0.25f;

	Move move;
	Move target_move;

	void Start()
    {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	void Update() 
	{
        if (target_move)
        {
            // TODO 5: First come up with your ideal velocity
            // then accelerate to it.

            Vector3 targetVel = target_move.movement;
            Vector3 newAcceleration = targetVel - move.movement;

            newAcceleration /= time_to_target;

            if (newAcceleration.magnitude > move.max_mov_acceleration)
            {
                newAcceleration.Normalize();
                newAcceleration *= move.max_mov_acceleration;
            }

            move.AccelerateMovement(newAcceleration);
        }
	}
}
