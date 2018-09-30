using UnityEngine;
using System.Collections;

public class KinematicSeek : MonoBehaviour
{
	Move move;

	void Start()
    {
		move = GetComponent<Move>();
	}
	
	void Update() 
	{
		Vector3 diff = move.target.transform.position - transform.position;
		diff.Normalize ();
		diff *= move.max_mov_velocity;

		move.SetMovementVelocity(diff);
	}
}
