using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SteeringConf
{
    public const int numPriorities = 5;
}

abstract public class SteeringAbstract : MonoBehaviour
{
    [Range(0, SteeringConf.numPriorities)]
    public int priority = 0;
}
