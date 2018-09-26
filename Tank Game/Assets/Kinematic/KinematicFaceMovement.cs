using UnityEngine;
using System.Collections;

public class KinematicFaceMovement : MonoBehaviour
{
    Move move;

	// Use this for initialization
	void Start()
    {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update()
    {
        // TODO 7: rotate the whole tank to look in the movement direction
        // Extremnely similar to TODO 2
        float angle = Mathf.Atan2(move.mov_velocity.normalized.x, move.mov_velocity.normalized.z);
        Quaternion new_rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
        transform.rotation = new_rotation;
    }
}
