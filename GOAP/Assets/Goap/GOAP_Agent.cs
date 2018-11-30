using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GOAP_Agent : MonoBehaviour, IGoap
{
	public List<Condition> state { get; set; }
    public List<Goal> availableGoals { get; set; }
    public List<Action> availableActions { get; set; }

    public Transform Alarm = null;

    void Awake()
    {
        state = new List<Condition>();
        availableGoals = new List<Goal>();
        availableActions = new List<Action>();

        availableGoals.Add(ScriptableObject.CreateInstance<GOAP_Goal_AlertBase>());
        availableActions.Add(ScriptableObject.CreateInstance<GOAP_Action_GoTo>());
        availableActions.Add(ScriptableObject.CreateInstance<GOAP_Action_SoundAlarm>());

        foreach (Goal g in availableGoals)
            g.OnGoalInitialize(this);
    }
}
