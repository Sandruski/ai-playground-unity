using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorsManager : MonoBehaviour
{
    public enum KinematicBehavior { seek, flee, wander };
    public KinematicBehavior currBehavior = KinematicBehavior.seek;

    KinematicSeek seek;
    KinematicFlee flee;
    KinematicArrive arrive;
    KinematicWander wander;

	void Awake()
    {
        seek = GetComponent<KinematicSeek>();
        flee = GetComponent<KinematicFlee>();
        arrive = GetComponent<KinematicArrive>();
        wander = GetComponent<KinematicWander>();
    }

    void Start()
    {
        ChangeBehavior(currBehavior);
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            if (currBehavior != KinematicBehavior.seek)
                ChangeBehavior(KinematicBehavior.seek);
        }
        else if (Input.GetKeyDown("f"))
        {
            if (currBehavior != KinematicBehavior.flee)
                ChangeBehavior(KinematicBehavior.flee);
        }
        else if (Input.GetKeyDown("w"))
        {
            if (currBehavior != KinematicBehavior.wander)
                ChangeBehavior(KinematicBehavior.wander);
        }
    }

    void ChangeBehavior(KinematicBehavior newBehavior)
    {
        currBehavior = newBehavior;

        switch (currBehavior)
        {
            case KinematicBehavior.seek:
                flee.enabled = false;
                wander.enabled = false;

                seek.enabled = true;
                arrive.enabled = true;
                break;

            case KinematicBehavior.flee:
                seek.enabled = false;
                arrive.enabled = false;
                wander.enabled = false;

                flee.enabled = true;
                break;

            case KinematicBehavior.wander:
                seek.enabled = false;
                arrive.enabled = false;
                flee.enabled = false;

                wander.enabled = true;
                break;

            default:
                break;
        }
    }
}
