using UnityEngine;
using System.Collections;

public class SteeringFlee : MonoBehaviour
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

        // TODO 2: Same as Steering seek but opposite direction

        Vector3 acceleratedDir = transform.position - target;
        acceleratedDir.Normalize();
        acceleratedDir *= move.max_mov_acceleration;

        move.AccelerateMovement(acceleratedDir);
    }
}
