using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMoonPoints : MonoBehaviour
{

    public GameObject container;
    public GameObject identifier;

    void Start()
    {
        UserDataHolder[] items = container.GetComponentsInChildren<UserDataHolder>();

        foreach (UserDataHolder item in items)
        {
            // create identifier
            Transform info = item.transform;
            GameObject newIdentifier = Instantiate(identifier, info.position, info.rotation);
            newIdentifier.transform.parent = info;

            // colour identifier
            switch (item.SubType)
            {
                case "impact_artificial":
                    newIdentifier.GetComponent<Renderer>().material.color = Color.red;
                    break;
                case "impact_natural":
                    newIdentifier.GetComponent<Renderer>().material.color = Color.magenta;
                    break;
                case "shallow":
                    newIdentifier.GetComponent<Renderer>().material.color = Color.cyan;
                    break;
                case "deep":
                    newIdentifier.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "landing":
                    newIdentifier.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                default:
                    break;
            }

            // Create Audio if exists
            if (item.AudioFile != "" || item.AudioFile != null)
            {
                item.AudioFile = item.AudioFile.Replace(".wav", "");
                AudioClip audioComponent = Resources.Load<AudioClip>(item.AudioFile);

                AudioSource moonquake = info.gameObject.AddComponent<AudioSource>();
                moonquake.clip = audioComponent;
                moonquake.rolloffMode = AudioRolloffMode.Linear;
                moonquake.playOnAwake = false;
            }
        }
        Debug.Log("Starting!");
        transform.SendMessage("BeginRound");
    }

}
