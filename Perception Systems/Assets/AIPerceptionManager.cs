using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptionEvent
{
	public enum senses { VISION, SOUND };
	public enum types { NEW, LOST };

	public GameObject go;
	public senses sense;
	public types type;
}

public class AIPerceptionManager : MonoBehaviour
{
	public GameObject Alert;

	void PerceptionEvent (PerceptionEvent ev) {

		if(ev.type == global::PerceptionEvent.types.NEW)
		{
			Debug.Log("Saw something NEW: " + ev.go.name);
			Alert.SetActive(true);
		}
		else
		{
			Debug.Log("LOST: " + ev.go.name);
			Alert.SetActive(false);
		}
	}
}