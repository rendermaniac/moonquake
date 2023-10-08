using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnumerateAll : MonoBehaviour
{
    public GameObject container;
    public bool forever = true;
    public float waitTime = 4;

    private UserDataHolder previous;

    public void BeginRound()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        UserDataHolder[] items = container.GetComponentsInChildren<UserDataHolder>();

        while (forever)
        {
            foreach (UserDataHolder item in items)
            {
                if (previous)
                {
                    previous.correctColour();
                }
                int returnType = item.InfoBox();

                if (returnType == 0)
                {
                    yield return new WaitForSeconds(waitTime);
                }

                Debug.Log(returnType);

                previous = item;
            }
        }
        
        Debug.Log("Complete!"); 
    }

}
