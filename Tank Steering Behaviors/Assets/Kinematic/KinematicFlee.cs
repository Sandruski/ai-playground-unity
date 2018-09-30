using UnityEngine;
using System.Collections;

public class KinematicFlee : MonoBehaviour
{
	Move move;

	void Start()
    {
		move = GetComponent<Move>();
	}
	
	void Update() 
	{
		Vector3 diff = move.transform.position - move.target.transform.position;
		diff.Normalize ();
		diff *= move.max_mov_velocity;

		move.SetMovementVelocity(diff);
	}
}
