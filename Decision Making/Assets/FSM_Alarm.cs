﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class FSM_Alarm : MonoBehaviour {
    private bool player_detected = false;
    private bool in_alarm = false;
    private Vector3 patrol_pos;

    public GameObject alarm;
    public BansheeGz.BGSpline.Curve.BGCurve path;

    private bool perform = true;
    private NavMeshAgent agent;

    private SteeringArrive arrive;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == alarm)
            in_alarm = true;
    }

    // Update is called once per frame
    void PerceptionEvent(PerceptionEvent ev)
    {
        if (ev.type == global::PerceptionEvent.types.NEW)
        {
            player_detected = true;
        }
        else
            player_detected = false;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        arrive = GetComponent<SteeringArrive>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Patrol");
    }

    // TODO 1: Create a coroutine that executes 20 times per second
    // and goes forever. Make sure to trigger it from Start()
    IEnumerator Patrol()
    {
        while (!player_detected)
        {
            Debug.Log("Starting patrol...");
            yield return new WaitForSeconds(1.0f / 20.0f);
        }

        patrol_pos = transform.position;
        path.gameObject.SetActive(false);
        agent.SetDestination(alarm.transform.position);

        yield return StartCoroutine("ReachAlarm");
    }

    // TODO 2: If player is spotted, jump to another coroutine that should
    // execute 20 times per second waiting for the player to reach the alarm
    IEnumerator ReachAlarm()
    {
        while (!in_alarm)
        {
            yield return new WaitForSeconds(1.0f / 20.0f);
        }

        in_alarm = false;
        agent.SetDestination(patrol_pos);

        yield return StartCoroutine("BackToPatrol");
    }

    // TODO 3: Create the last coroutine to have the tank waiting to reach
    // the point where he left the path, and trigger again the patrol
    IEnumerator BackToPatrol()
    {
        Vector3 direction = patrol_pos - transform.position;
        while (direction.magnitude > 1.0f)
        {
            yield return new WaitForSeconds(1.0f / 20.0f);
        }

        path.gameObject.SetActive(true);

        yield return StartCoroutine("Patrol");
    }
}