using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		// TODO 4: As with arrive, we first construct our ideal rotation
		// then accelerate to it. Use Mathf.DeltaAngle() to wrap around PI
		// Is the same as arrive but with angular velocities

	}
}
