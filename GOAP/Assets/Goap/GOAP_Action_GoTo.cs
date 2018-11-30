using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAP_Action_GoTo : Action
{
    private GOAP_Agent entity = null;
    private Transform target = null;

    // Constructor
    public GOAP_Action_GoTo()
    {
        cost = 1;
    }

    public override void OnActionSetup(IGoap igoap, List<Condition> state)
    {
        entity = (GOAP_Agent)igoap;
        target = null;

        foreach (Condition c in state)
        {
            if (c.identifier == "CloseTo")
            {
                target = (Transform)c.value;
                break;
            }
        }

        UpdateEffect("CloseTo", target);
        isViable = (target != null);
    }

    public override void OnActionStart()
    {

    }

    public override void OnActionPerform()
    {

    }

    public override void OnActionFinished()
    {

    }

    public override void OnActionAborted()
    {

    }
}
