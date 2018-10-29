using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// TODO 1: Create a simple class to contain one entry in the blackboard
// should at least contain the gameobject, position, timestamp and a bool
// to know if it is in the past memory
public class BlackboardEntry
{
    public GameObject gameObject;
    public Vector3 position;
    public float timestamp;
    public bool inMemory;
    public GameObject from;
}

public class AIMemory : MonoBehaviour
{
    public float forgetTime = 30.0f;
    public GameObject Cube;
    public Text Output;
    public List<AIMemory> friends = new List<AIMemory>();

    // TODO 2: Declare and allocate a dictionary with a string as a key and
    // your previous class as value
    public Dictionary<string, BlackboardEntry> blackboard;

    // TODO 3: Capture perception events and add an entry if the player is detected
    // if the player stop from being seen, the entry should be "in the past memory"

    // Use this for initialization
    void Start()
    {
        blackboard = new Dictionary<string, BlackboardEntry>();     
    }

    // Update is called once per frame
    void Update()
    {
        List<BlackboardEntry> entriesToRemove = new List<BlackboardEntry>();

        foreach (BlackboardEntry entry in blackboard.Values)
        {
            Cube.transform.position = entry.position;

            // We see the thing
            if (!entry.inMemory)
            {
                // Keep updating its position
                entry.position = entry.gameObject.transform.position;
                entry.timestamp = 0.0f;
            }
            else if (entry.timestamp >= forgetTime)
            {
                // Forget the thing
                entriesToRemove.Add(entry);
            }
            else
            {
                entry.timestamp += Time.deltaTime;
            }
        }

        for (int i = entriesToRemove.Count - 1; i >= 0; --i)
        {
            blackboard.Remove(entriesToRemove[i].gameObject.name);
        }
        entriesToRemove.Clear();

        string result = null;
        if (blackboard.Count > 0)
        {
            foreach (BlackboardEntry entry in blackboard.Values)
            {
                // TODO 4: Add text output to the bottom-left panel with the information
                // of the elements in the Knowledge base
                result = string.Format("{0} {1:0.0} {2:0.0} {3}", entry.gameObject.name, entry.position, entry.timestamp, entry.inMemory);
            }
        }
        else
        {
            result = "Nothing...";
        }
        Output.text = result;
    }

    void PerceptionEvent(PerceptionEvent ev)
    {
        if (ev.type == global::PerceptionEvent.types.NEW)
        {
            // Something new
            if (blackboard.ContainsKey(ev.go.name))
            {
                // Already in memory
                blackboard[ev.go.name].timestamp = 0.0f;
                blackboard[ev.go.name].position = ev.go.transform.position;
                blackboard[ev.go.name].inMemory = false;
            }
            else
            {
                // Something new
                BlackboardEntry entry = new BlackboardEntry();
                entry.gameObject = ev.go;
                entry.position = ev.go.transform.position;
                entry.timestamp = 0.0f;
                entry.inMemory = false;
                entry.from = gameObject;

                blackboard.Add(entry.gameObject.name, entry);

                if (ev.sense == global::PerceptionEvent.senses.VISION)
                {
                    foreach (AIMemory friend in friends)
                    {
                        if (!friend.blackboard.ContainsKey(ev.go.name))
                        {
                            friend.blackboard.Add(entry.gameObject.name, entry);
                        }
                    }
                }
            }
        }
        else
        {
            // Lost something
            if (blackboard.ContainsKey(ev.go.name))
            {
                // Already in memory
                blackboard[ev.go.name].position = ev.go.transform.position;
                blackboard[ev.go.name].inMemory = true;
            }
        }
    }
}
