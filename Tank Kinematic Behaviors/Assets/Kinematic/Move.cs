using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public GameObject target;
	public GameObject aim;
	public Slider arrow;

    public Vector3 mov_velocity = Vector3.zero;
    public float max_mov_velocity = 5.0f;
    #endregion

    public void SetMovementVelocity(Vector3 vel)
    {
		mov_velocity = vel;
	}

	// Update is called once per frame
	void Update() 
	{
        // TODO 1: Make sure mov_velocity is never bigger that max_mov_velocity
        if (mov_velocity.magnitude > max_mov_velocity)
            SetMovementVelocity(mov_velocity.normalized * max_mov_velocity);

        // TODO 2: rotate the arrow to point to mov_velocity direction. First find out the angle
        // then create a Quaternion with that expressed that rotation and apply it to aim.transform
        float angle = Mathf.Atan2(mov_velocity.x, mov_velocity.z);
        Quaternion new_rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
        aim.transform.rotation = new_rotation;

        // TODO 3: stretch it the arrow (arrow.value) to show how fast the tank is getting push in
        // that direction. Adjust with some factor so the arrow is visible.
        float factor = 4.0f;
        arrow.value = mov_velocity.magnitude * factor;

        // TODO 4: update tank position based on final mov_velocity and deltatime
        transform.position += mov_velocity * Time.deltaTime;

        // Reset movement to 0 to simplify things ...
        mov_velocity = Vector3.zero;
	}
}
