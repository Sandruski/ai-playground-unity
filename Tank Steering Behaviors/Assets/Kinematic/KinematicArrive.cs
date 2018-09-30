using UnityEngine;
using System.Collections;

public class KinematicArrive : MonoBehaviour
{
	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;

	Move move;

	void Start()
    {
		move = GetComponent<Move>();
	}

	void Update() 
	{
        /// *Modified from original
		Vector3 diff = move.target.transform.position - transform.position;

        if (diff.magnitude < min_distance)
            move.SetMovementVelocity(Vector3.zero);
        else
        {
            diff /= time_to_target;
            move.SetMovementVelocity(diff);
        }
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
