using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UserDataHolder : MonoBehaviour
{
    public string EventType;
    public string SubType;
    public float Latitude;
    public float Longitude;
    public float Depth;
    public int Year;
    public int Month;
    public int Day;
    public int Hour;
    public int Minute;
    public string AudioFile;
    public string PlotFile;

    public void InfoBox()
    {
        string title;
        switch (SubType)
        {
            case "impact_artificial":
                title = $"Aritfical Impact || {EventType}";
                break;
            case "impact_natural":
                title = $"Natural Impact || {EventType}";
                break;
            case "shallow":
                title = $"Shallow Moonquake || {EventType}";
                break;
            case "deep":
                title = $"Deep Moonquake || {EventType}";
                break;
            case "landing":
                title = $"Rocket Landing || {EventType}";
                break;
            default:
                title = $"{SubType} || {EventType}";
                break;
        }
        GameObject.Find("InfoTitle").GetComponent<TMPro.TMP_Text>().text = title;


        DateTime time = new DateTime();

        time = time.AddYears(Year);
        time = time.AddMonths(Month);
        time = time.AddDays(Day);

        time = time.AddHours(Hour);
        time = time.AddMinutes(Minute);


        string FullInfoText = $"Location: {Latitude}ºN {Longitude}ºE \n";
        FullInfoText = FullInfoText + $"Date: {time.ToString("dd/MM/yyyy")} \n";
        FullInfoText = FullInfoText + $"Time: {time.ToString("H:mm:ss")} \n";
        FullInfoText = FullInfoText + $"Depth: {Depth}m";

        GameObject.Find("InfoText").GetComponent<TMPro.TMP_Text>().text = FullInfoText;

        //Transform UI = GameObject.Find("UI").transform;

        //UI.position = transform.position;
        //UI.rotation = transform.rotation;


        // Play sound if exists
        if (gameObject.TryGetComponent<AudioSource>(out AudioSource audio))
        {
            Debug.Log("Playing!");
            audio.Play();
        }

        // Create Plot if exists
        if (PlotFile != "" || PlotFile != null)
        {
            PlotFile.Replace(".png", "");
            Sprite plotSprite = Resources.Load<Sprite>(PlotFile);
            Debug.Log("Plotting!");

            RawImage img = GameObject.Find("Icon").GetComponent<RawImage>();
            img.texture = plotSprite.texture;
        }
    }
}