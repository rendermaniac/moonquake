using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnumerateAll : MonoBehaviour
{
    public GameObject container;

    private UserDataHolder previous;

    public void BeginRound()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(2);
        UserDataHolder[] items = container.GetComponentsInChildren<UserDataHolder>();

        yield return new WaitForSeconds(1);
        foreach (UserDataHolder item in items)
        {
            if (previous)
            {
                previous.correctColour();
            }
            int returnType = item.InfoBox();

            if (returnType == 0)
            {
                yield return new WaitForSeconds(4);
            }

            Debug.Log(returnType);

            previous = item;
        }
        Debug.Log("Complete!"); 
    }

}
