using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent agent;

	// Use this for initialization
	void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update()
    {
        agent.SetDestination(target.position);
	}
}
