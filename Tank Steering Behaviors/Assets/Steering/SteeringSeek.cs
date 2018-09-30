using UnityEngine;
using System.Collections;

public class SteeringSeek : MonoBehaviour
{
	Move move;

	void Start()
    {
		move = GetComponent<Move>();
	}
	
	void Update() 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
        if (!move)
            move = GetComponent<Move>();

        // TODO 1: accelerate towards our target at max_acceleration
        // use move.AccelerateMovement()

        Vector3 acceleratedDir = target - transform.position;
        acceleratedDir.Normalize();
        acceleratedDir *= move.max_mov_acceleration;

        move.AccelerateMovement(acceleratedDir);
    }
}
