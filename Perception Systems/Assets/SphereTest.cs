using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask rayMask;

    private float distance = 1.0f;
    private Camera camera;

    private List<GameObject> detectedEnemies = new List<GameObject>();

    void Start()
    {
        camera = GetComponent<Camera>();
        distance = camera.nearClipPlane + camera.farClipPlane;
    }

    void Update()
    {
        List<GameObject> newDetectedEnemies = new List<GameObject>();

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
                        newDetectedEnemies.Add(hitInfo.collider.gameObject);
                }
            }
        }

        for (int i = detectedEnemies.Count - 1; i >= 0; --i)
        {
            if (!newDetectedEnemies.Contains(detectedEnemies[i]))
            {
                PerceptionEvent perceptionEvent = new PerceptionEvent();
                perceptionEvent.type = PerceptionEvent.types.LOST;
                perceptionEvent.sense = PerceptionEvent.senses.VISION;
                perceptionEvent.go = detectedEnemies[i];
                gameObject.SendMessage("PerceptionEvent", perceptionEvent);

                // Lost enemy
                detectedEnemies.RemoveAt(i);
            }
        }

        foreach (GameObject newDetectedEnemy in newDetectedEnemies)
        {
            if (!detectedEnemies.Contains(newDetectedEnemy))
            {
                PerceptionEvent perceptionEvent = new PerceptionEvent();
                perceptionEvent.type = PerceptionEvent.types.NEW;
                perceptionEvent.sense = PerceptionEvent.senses.VISION;
                perceptionEvent.go = newDetectedEnemy;
                gameObject.SendMessage("PerceptionEvent", perceptionEvent);

                // New enemy
                detectedEnemies.Add(newDetectedEnemy);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
