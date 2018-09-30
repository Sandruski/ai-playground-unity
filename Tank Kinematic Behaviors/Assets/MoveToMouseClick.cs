using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveToMouseClick : MonoBehaviour
{
	void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit) == true)
                transform.position = hit.point;
        }
	}
}
