using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public float max_angle = 0.5f;
    public float secondsChangeDirection = 1.0f;
    #endregion

    #region PRIVATE_VARIABLES
    Move move;

    private Vector3 randomVelocity = Vector3.zero;
    private float timer = 0.0f;
    #endregion

    // Use this for initialization
    void Start()
    {
		move = GetComponent<Move>();
        timer = secondsChangeDirection;
	}

	// number [-1,1] values around 0 more likely
	float RandomBinominal()
	{
		return Random.value * Random.value;
	}
	
	void Update() 
	{
        // TODO 9: Generate a velocity vector in a random rotation (use RandomBinominal) and some attenuation factor
        timer += Time.deltaTime;

        if (timer >= secondsChangeDirection)
        {
            float angle = RandomBinominal() * max_angle;       

            randomVelocity = new Vector3(-Mathf.Sin(angle), 0.0f, Mathf.Cos(angle));
            randomVelocity.Normalize();
            randomVelocity *= move.max_mov_velocity;

            timer = 0.0f;
        }

        move.SetMovementVelocity(randomVelocity);
    }
}
