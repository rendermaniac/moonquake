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
        Debug.Log(items.Length);

        yield return new WaitForSeconds(1);
        while (true)
        {
            foreach (UserDataHolder item in items)
            {
                item.InfoBox();
                yield return new WaitForSeconds(2);
            }
        }
    }

}
