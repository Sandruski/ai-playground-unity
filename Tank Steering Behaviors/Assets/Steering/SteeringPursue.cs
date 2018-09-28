using UnityEngine;
using System.Collections;

public class SteeringPursue : MonoBehaviour {

	public float max_prediction;

	Move move;
	SteeringArrive arrive;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		arrive = GetComponent<SteeringArrive>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position, move.target.GetComponent<Move>().movement);
	}

	public void Steer(Vector3 target, Vector3 velocity)
	{
		// TODO 6: Create a fake position to represent
		// enemies predicted movement. Then call Steer()
		// on our Steering Arrive
	}
}
