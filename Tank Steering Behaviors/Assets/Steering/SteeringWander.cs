using UnityEngine;
using System.Collections;

public class SteeringWander : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public float min_distance = 0.1f; // ?
	public float time_to_target = 0.25f; // ?

    public float distanceToCircle = 4.0f;
    public float circleRadius = 1.0f;

    public float wanderRate = 0.1f;
    #endregion

    #region PRIVATE_VARIABLES
    private Move move;
    private SteeringSeek seek;

    private Vector3 target = Vector3.zero;

    private float timer = 0.0f;
    #endregion

    void Awake()
    {
		move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
	}

    void Start()
    {
        timer = wanderRate;
    }

    void Update() 
	{
        /*
		Vector3 diff = move.target.transform.position - transform.position;

        if (diff.magnitude < min_distance)
            return;

		diff /= time_to_target;

        move.AccelerateMovement(diff);
        */

        if (timer >= wanderRate)
        {
            // Update the target
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            randomDirection.Normalize();

            Vector3 circlePosition = transform.position + transform.forward * distanceToCircle;
            target = circlePosition + randomDirection * circleRadius;

            timer = 0.0f;
        }

        timer += Time.deltaTime;

        seek.Steer(target);
    }

    void OnDrawGizmos()
    {
        // Debug wander behavior
        Gizmos.color = Color.white;

        Vector3 circlePosition = transform.position + transform.forward * distanceToCircle;
        Gizmos.DrawWireSphere(circlePosition, circleRadius);
        Gizmos.DrawLine(transform.position, circlePosition);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target, 1.0f);
    }

    void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
    }
}
