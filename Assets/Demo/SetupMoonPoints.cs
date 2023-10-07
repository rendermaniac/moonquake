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

            string path;
            if (item.AudioFile != "" || item.AudioFile != null)
            {
                // Create sound
                path = Application.persistentDataPath + item.AudioFile;
                info.gameObject.AddComponent<AudioSource>().clip = AudioClip.Create(path, 672000, 1, 224000, false);
            }
        }
        Debug.Log("Starting!");
        transform.SendMessage("BeginRound");
    }

}
