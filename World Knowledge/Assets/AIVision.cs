using UnityEngine;
using System.Collections.Generic;

public class PerceptionEvent
{
	public enum senses { VISION, SOUND };
	public enum types { NEW, LOST };

	public GameObject go;
	public senses sense;
	public types type;
}

public class AIVision : MonoBehaviour
{
    public float hearRadius = 1.0f;

	public Camera frustum;
	public LayerMask ray_mask;
	public LayerMask vision_mask;
    public LayerMask sound_mask;

	private List<GameObject> detected;
	private List<GameObject> detected_now;
	private Ray ray;

	// Use this for initialization
	void Start () {
		detected = new List<GameObject>();
		detected_now = new List<GameObject>();
		ray = new Ray();
	}
	
	// Update is called once per frame
	void Update ()
    {
		Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, vision_mask);
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

		detected_now.Clear();

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                // Vision
                RaycastHit hit;
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, ray_mask))
                {
                    if (hit.collider.gameObject.CompareTag("Visual Emitter"))
                        detected_now.Add(col.gameObject);
                }
            }
        }

        /*
        colliders = Physics.OverlapSphere(transform.position, hearRadius, sound_mask);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                // Sound
                if (col.gameObject.CompareTag("Sound Emitter"))
                    detected_now.Add(col.gameObject);
            }
        }
        */

        // Compare detected with detected_now -------------------------------------
        foreach (GameObject go in detected_now)
        {
            if (detected.Contains(go) == false)
            {
                PerceptionEvent p_event = new PerceptionEvent();
                p_event.go = go;
                p_event.type = PerceptionEvent.types.NEW;
                p_event.sense = PerceptionEvent.senses.VISION;

                SendMessage("PerceptionEvent", p_event);
            }
        }

        foreach (GameObject go in detected)
        {
            if (detected_now.Contains(go) == false)
            {
                PerceptionEvent p_event = new PerceptionEvent();
                p_event.go = go;
                p_event.type = PerceptionEvent.types.LOST;
                p_event.sense = PerceptionEvent.senses.VISION;

                SendMessage("PerceptionEvent", p_event);
            }
        }

		detected.Clear();
		detected.AddRange(detected_now);
	}

    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, hearRadius);
        */
    }
}
