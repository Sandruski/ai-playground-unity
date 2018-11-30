using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAP_Goal_SearchPlayer : Goal
{
    // Constructor
    public GOAP_Goal_SearchPlayer()
    {
        AddSucces("Patrolling", true);
        priority = 1;
    }

    public override void OnGoalInitialize(IGoap igoap)
    {
        this.igoap = igoap;
    }

    public override void OnGoalSetup()
    {

    }

    public override void OnGoalFinished()
    {

    }

    public override void OnGoalAborted()
    {

    }

    public override bool IsGoalRelevant()
    {
        return true;
    }
}
