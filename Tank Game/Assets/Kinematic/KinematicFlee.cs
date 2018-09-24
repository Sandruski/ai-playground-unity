using UnityEngine;
using System.Collections;

public class KinematicFlee : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 6: To create flee just switch the direction to go
        Vector3 targetDirection = -(move.target.transform.position - transform.position);
        targetDirection.y = 0.0f;
        targetDirection.Normalize();

        move.SetMovementVelocity(targetDirection * move.max_mov_velocity);
    }
}
