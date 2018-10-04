﻿using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

	public float min_distance = 0.1f;
	public float slow_distance = 5.0f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () { 
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
		if(!move)
			move = GetComponent<Move>();

		// Velocity we are trying to match
		float ideal_velocity = 0.0f;
		Vector3 diff = target - transform.position;

		if(diff.magnitude < min_distance)
			move.SetMovementVelocity(Vector3.zero);

		// Decide wich would be our ideal velocity
		if(diff.magnitude > slow_distance)
			ideal_velocity = move.max_mov_velocity;
		else
			ideal_velocity = move.max_mov_velocity * diff.magnitude / slow_distance;

		// Create a vector that describes the ideal velocity
		Vector3 ideal_movement = diff.normalized * ideal_velocity;

		// Calculate acceleration needed to match that velocity
		Vector3 acceleration = ideal_movement - move.movement;
		acceleration /= time_to_target;

		// Cap acceleration
		if(acceleration.magnitude > move.max_mov_acceleration)
		{
			acceleration.Normalize();
			acceleration *= move.max_mov_acceleration;
		}

		move.AccelerateMovement(acceleration);
	}
    
    /// My own implementation of Steer
    /*
    public void Steer(Vector3 target)
    {
        if (!move)
            move = GetComponent<Move>();

        // TODO 3: Create a vector to calculate our ideal velocity
        // then calculate the acceleration needed to match that velocity
        // before sending it to move.AccelerateMovement() clamp it to 
        // move.max_mov_acceleration

        Vector3 currVel = target - transform.position;
        float distanceToTarget = currVel.magnitude;

        currVel.Normalize();
        currVel *= move.max_mov_acceleration;

        Vector3 newAcceleration = currVel;

        if (distanceToTarget < slow_distance)
        {
            Vector3 idealVel = currVel.normalized * distanceToTarget / time_to_target;

            if (distanceToTarget < min_distance)
                idealVel = Vector3.zero;

            newAcceleration = idealVel - move.movement;
        }

        move.AccelerateMovement(newAcceleration);
    }
    */

    void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
