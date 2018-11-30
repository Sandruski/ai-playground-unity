using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAP_Action_SoundAlarm : Action
{
    private GOAP_Agent entity = null;

    // Constructor
    public GOAP_Action_SoundAlarm()
    {
        AddPrecondition("CloseTo", null);
        AddEffect("AlarmTriggered", true);
        cost = 1;
    }

    public override void OnActionSetup(IGoap igoap, List<Condition> state)
    {
        entity = (GOAP_Agent)igoap;
        Transform alarm_point = entity.Alarm;

        UpdatePrecondition("CloseTo", alarm_point);
        isViable = (alarm_point != null);
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
