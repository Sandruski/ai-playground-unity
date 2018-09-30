using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorsManager : MonoBehaviour
{
    public enum Behavior { steeringPursue, steeringEvade, steeringWander };
    public Behavior currBehavior = Behavior.steeringPursue;

    private SteeringEvade steeringEvade;
    private SteeringWander steeringWander;
    private SteeringPursue steeringPursue;

    private KinematicFaceMovement kinematicFaceMovement;

    void Awake()
    {
        steeringEvade = GetComponent<SteeringEvade>();
        steeringWander = GetComponent<SteeringWander>();
        steeringPursue = GetComponent<SteeringPursue>();

        kinematicFaceMovement = GetComponent<KinematicFaceMovement>();
    }

    void Start()
    {
        ChangeBehavior(currBehavior);
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (currBehavior != Behavior.steeringEvade)
                ChangeBehavior(Behavior.steeringEvade);
        }
        else if (Input.GetKeyDown("w"))
        {
            if (currBehavior != Behavior.steeringWander)
                ChangeBehavior(Behavior.steeringWander);
        }
        else if (Input.GetKeyDown("p"))
        {
            if (currBehavior != Behavior.steeringPursue)
                ChangeBehavior(Behavior.steeringPursue);
        }
    }

    void ChangeBehavior(Behavior newBehavior)
    {
        currBehavior = newBehavior;

        switch (currBehavior)
        {
            case Behavior.steeringEvade:
                steeringWander.enabled = false;
                steeringPursue.enabled = false;
                kinematicFaceMovement.enabled = false;

                steeringEvade.enabled = true;
                break;

            case Behavior.steeringWander:
                steeringEvade.enabled = false;
                steeringPursue.enabled = false;

                kinematicFaceMovement.enabled = true;
                steeringWander.enabled = true;
                break;

            case Behavior.steeringPursue:
                steeringEvade.enabled = false;
                steeringWander.enabled = false;
                kinematicFaceMovement.enabled = false;

                steeringPursue.enabled = true;
                break;

            default:
                break;
        }
    }
}
