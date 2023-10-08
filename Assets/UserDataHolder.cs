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
    public string IconFile;

    private Vector3 defaultScale = Vector3.one * 0.04f;

    private void SetDefaultScale()
    {
        defaultScale = transform.GetChild(0).localScale;
    }

    public int InfoBox()
    {
        SetDefaultScale();
        string title;
        switch (SubType)
        {
            case "impact_artificial":
                title = $"Aritfical Impact";
                break;
            case "impact_natural":
                title = $"Natural Impact";
                break;
            case "shallow":
                title = $"Shallow Moonquake";
                break;
            case "deep":
                title = $"Deep Moonquake";
                break;
            case "landing":
                title = $"Rocket Landing";
                break;
            default:
                return 1;
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
        if (SubType == "landing" || SubType == "impact_artificial")
        {
            FullInfoText = FullInfoText + $"Mission: {EventType}";
        } else
        {
            FullInfoText = FullInfoText + $"Depth: {Depth}m";
        }

        GameObject.Find("InfoText").GetComponent<TMPro.TMP_Text>().text = FullInfoText;

        //Transform UI = GameObject.Find("UI").transform;

        //UI.position = transform.position;
        //UI.rotation = transform.rotation;


        // Create Plot if exists
        if (PlotFile != "" || PlotFile != null)
        {
            PlotFile = PlotFile.Replace(".png", "");
            Sprite plotSprite = Resources.Load<Sprite>(PlotFile);

            RawImage img = GameObject.Find("WaveForm").GetComponent<RawImage>();

            if (plotSprite != null)
            {
                Debug.Log("Plotting!");

                Texture2D plotTexture = textureFromSprite(plotSprite);
                if (plotTexture != null)
                {
                    img.texture = plotTexture;
                } else
                {
                    img.texture = textureFromSprite( Resources.Load<Sprite>("Default") );
                    return 3;
                }
            } else
            {
                img.texture = textureFromSprite(Resources.Load<Sprite>("Default"));
                return 2;
            }
        } else
        {
            GameObject.Find("WaveForm").GetComponent<RawImage>().texture = textureFromSprite(Resources.Load<Sprite>("Default"));
            return 1;
        }


        // Create icon if exists
        if (IconFile != "" || IconFile != null)
        {
            Sprite iconSprite = Resources.Load<Sprite>(IconFile.Replace(".jpg", ""));

            if (iconSprite != null)
            {
                Debug.Log("adding icon!");

                RawImage img = GameObject.Find("Icon").GetComponent<RawImage>();

                Texture2D plotTexture = textureFromSprite(iconSprite);
                if (plotTexture != null)
                {
                    img.texture = plotTexture;
                }
                else
                {
                    return 6;
                }
            }
            else
            {
                return 5;
            }
        }
        else
        {
            return 4;
        }

        // Play sound if exists
        if (gameObject.TryGetComponent<AudioSource>(out AudioSource audio))
        {
            Debug.Log("Playing!");
            audio.Play();
        }
        else
        {
            return 9;
        }

        Transform identifier = gameObject.transform.GetChild(0);
        identifier.GetComponent<Renderer>().material.color = Color.green;
        identifier.localScale = Vector3.one * 0.1f;

        return 0;
    }

    // https://discussions.unity.com/t/convert-sprite-image-to-texture/97618/4
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }


    public void correctColour()
    {
        GameObject identifier = transform.GetChild(0).gameObject;
        identifier.transform.localScale = defaultScale;
        // colour identifier
        switch (SubType)
        {
            case "impact_artificial":
                identifier.GetComponent<Renderer>().material.color = Color.red;
                break;
            case "impact_natural":
                identifier.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            case "shallow":
                identifier.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case "deep":
                identifier.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case "landing":
                identifier.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            default:
                break;
        }
    }
}