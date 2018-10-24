using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask rayMask;

    private float distance = 1.0f;
    private Camera camera;

	void Start()
    {
        camera = GetComponent<Camera>();
        distance = camera.nearClipPlane + camera.farClipPlane;

        List<GameObject> enemies = new List<GameObject>();
    }

    void Update()
    {


        // 1
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, mask);
        foreach (Collider col in colliders)
        {
            if (col.gameObject == gameObject)
                continue;

            // 2
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                // 3
                RaycastHit hitInfo;
                Ray ray = new Ray(transform.position + transform.forward * camera.nearClipPlane, col.gameObject.transform.position - transform.position);
                if (Physics.Raycast(ray, out hitInfo, distance, rayMask))
                {
                    Debug.DrawRay(ray.origin, col.transform.position - transform.position);

                    if (hitInfo.collider.gameObject.CompareTag("Visual Emitter"))
                    {
                        Debug.Log("Player!!!");
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
