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

    public int InfoBox()
    {
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
        } else
        {
            return 1;
        }

        // Create Plot if exists
        if (PlotFile != "" || PlotFile != null)
        {
            PlotFile = PlotFile.Replace(".png", "");
            Sprite plotSprite = Resources.Load<Sprite>(PlotFile);

            if (plotSprite != null)
            {
                Debug.Log("Plotting!");

                RawImage img = GameObject.Find("Icon").GetComponent<RawImage>();

                Texture2D plotTexture = textureFromSprite(plotSprite);
                if (plotTexture != null)
                {
                    img.texture = plotTexture;
                } else
                {
                    return 1;
                }
            } else
            {
                return 1;
            }
        } else
        {
            return 1;
        }

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
}