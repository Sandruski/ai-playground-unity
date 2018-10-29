using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmitSound : MonoBehaviour
{
    public SphereCollider sound = null;

    private NavMeshAgent agent = null;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (agent.velocity.magnitude != 0.0f)
        {
            sound.gameObject.SetActive(true);
            sound.transform.position = transform.position;
        }
        else
            sound.gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if (sound.gameObject.activeSelf)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(sound.transform.position, sound.radius);
        }
    }
}
