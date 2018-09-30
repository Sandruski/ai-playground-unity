using UnityEngine;
using System.Collections;

public class KinematicFaceMovement : MonoBehaviour
{
	public float min_angle = 1.0f;

	Move move;

	void Start()
    {
		move = GetComponent<Move>();
	}
	
	void Update() 
	{
        /// *Modified from original
        float target_degrees = Mathf.Atan2(move.movement.x, move.movement.z) * Mathf.Rad2Deg;
		float current_degrees = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
		float delta = Mathf.DeltaAngle(current_degrees, target_degrees);

        if (Mathf.Abs(delta) < min_angle)
            move.SetRotationVelocity(0.0f);
        else
            move.SetRotationVelocity(delta);
	}
}
