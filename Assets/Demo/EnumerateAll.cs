using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnumerateAll : MonoBehaviour
{
    public GameObject container;

    public void BeginRound()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(2);
        UserDataHolder[] items = container.GetComponentsInChildren<UserDataHolder>();

        yield return new WaitForSeconds(1);
        while (true)
        {
            foreach (UserDataHolder item in items)
            {
                int returnType = item.InfoBox();

                if (returnType == 0)
                {
                    yield return new WaitForSeconds(4);
                }
            }
        }
    }

}
