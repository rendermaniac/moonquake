using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnMoonPoints : MonoBehaviour
{
    private UserDataHolder previous;
    private RaycastHit previousRay = new RaycastHit();

    void Update()
    {
        if (Input.GetMouseButton(0))
        { // if left button pressed...

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                if (hit.transform.position != previousRay.transform.position)
                {
                    UserDataHolder point;
                    Debug.Log("I hit: " + hit.transform.parent.name);
                    if (hit.transform.gameObject.TryGetComponent<UserDataHolder>(out point))
                    {
                        Debug.Log(hit.transform.name);
                        if (point != previous)
                        {

                            previous.correctColour();
                            point.InfoBox();
                        }
                    }

                    previousRay = hit;
                }
            }
        }
    }
}
