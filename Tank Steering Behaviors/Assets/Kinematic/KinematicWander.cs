using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour
{
	public float max_angle = 0.5f;
    public float wanderRate = 0.1f;

	Move move;

    private float timer = 0.0f;

    void Start()
    {
		move = GetComponent<Move>();

        timer = wanderRate;
	}

	// Number [-1,1] values around 0 more likely
	float RandomBinominal()
	{
		return Random.value * Random.value;
	}
	
	void Update() 
	{
        /// *Modified from original
        timer += Time.deltaTime;

        if (timer >= wanderRate)
        {
            // Random rotation
            float angle = RandomBinominal() * max_angle;
            Vector3 velocity = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up) * Vector3.forward;
            velocity *= move.max_mov_velocity;

            move.SetMovementVelocity(velocity);

            timer = 0.0f;
        }
	}
}
