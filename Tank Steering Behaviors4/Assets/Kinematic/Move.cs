using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_velocity = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

	[Header("-------- Read Only --------")]
	public Vector3 movement = Vector3.zero;
	public float rotation = 0.0f; // degrees

    Vector3[] movementVelocity = new Vector3[SteeringConf.numPriorities];
    float[] angularVelocity = new float[SteeringConf.numPriorities];

	// Methods for behaviours to set / add velocities
	public void SetMovementVelocity (Vector3 velocity) 
	{
		movement = velocity;
	}

	public void AccelerateMovement (Vector3 velocity, int priority) 
	{
        movementVelocity[priority] = velocity; 
	}

	public void SetRotationVelocity (float rotation_velocity) 
	{
		rotation = rotation_velocity;
	}

	public void AccelerateRotation (float rotation_acceleration, int priority) 
	{
        angularVelocity[priority] = rotation_acceleration;
	}


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < movementVelocity.Length; ++i)
        {
            if (!Mathf.Approximately(movementVelocity[i].magnitude, 0.0f))
            {
                movement = movementVelocity[i];
                break;
            }              
        }

        for (int i = 0; i < angularVelocity.Length; ++i)
        {
            if (!Mathf.Approximately(angularVelocity[i], 0.0f))
            {
                rotation = angularVelocity[i];
                break;
            }
        }

        // cap velocity
        if (movement.magnitude > max_mov_velocity)
        {
            movement.Normalize();
            movement *= max_mov_velocity;
        }

        // cap rotation
        Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

        // rotate the arrow
        float angle = Mathf.Atan2(movement.x, movement.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // strech it
        arrow.value = movement.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += movement * Time.deltaTime;

        for (int i = 0; i < SteeringConf.numPriorities; ++i)
        {
            movementVelocity[i] = Vector3.zero;
            angularVelocity[i] = 0.0f;
        }
    }
}
