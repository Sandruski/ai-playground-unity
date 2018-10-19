using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public Transform target;

    private NavMeshAgent agent;
    private Animator anim;

    int movement = 0;
    int velX = 0;
    int velY = 0;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        movement = Animator.StringToHash("movement");
        velX = Animator.StringToHash("velX");
        velY = Animator.StringToHash("velY");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        if (agent.remainingDistance > 0.0f)
            anim.SetBool(movement, true);
        else
            anim.SetBool(movement, false);

        Vector3 vel = transform.InverseTransformVector(agent.velocity.normalized);
        anim.SetFloat(velX, vel.x);
        anim.SetFloat(velY, vel.z);
    }
}