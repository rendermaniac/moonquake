using UnityEngine;
using UnityEditor;
using System.IO;

class CustomImportProcessor : AssetPostprocessor
{
    void OnPostprocessGameObjectWithUserProperties(GameObject go, string[] names, System.Object[] values)
    {
        ModelImporter importer = (ModelImporter)assetImporter;
        var asset_name = Path.GetFileName(importer.assetPath);
        Debug.LogFormat("OnPostprocessGameObjectWithUserProperties(go = {0}) asset = {1}", go.name, asset_name);

        string eventType = null;
        string subType = null;
        float latitude = 0f;
        float longitude = 0f;
        int depth = -1;
        int year = -1;
        int month = -1;
        int day = -1;
        int hour = -1;
        int minute = -1;

        for (int i = 0; i < names.Length; i++)
        {
            var name = names[i];
            var val = values[i];

            switch (name)
            {
                case "type":
                    eventType = (string)val;
                    break;
                case "subtype":
                    subType = (string)val;
                    break;
                case "latitude":
                    latitude = (float)val;
                    break;
                case "longitude":
                    longitude = (float)val;
                    break;
                case "depth":
                    depth = (int)val;
                    break;
                case "year":
                    year = (int)val;
                    break;
                case "month":
                    month = (int)val;
                    break;
                case "day":
                    day = (int)val;
                    break;
                case "minute":
                    minute = (int)val;
                    break;
                default:
                    Debug.LogFormat("Unknown Property : {0} : {1} : {2}", name, val.GetType().Name, val.ToString());
                    break;
            }
        }
        if (eventType != null)
        {
            var md = go.AddComponent<UserDataHolder>();

            if (eventType != null)
            {
                md.EventType = eventType;
            }
            if (subType != null)
            {
                md.SubType = subType;
            }
            if (latitude != 0f)
            {
                md.Latitude = latitude;
            }
            if (longitude != 0f)
            {
                md.Longitude = longitude;
            }
            if (depth != -1)
            {
                md.Depth = depth;
            }
            if (year != -1)
            {
                md.Year = year;
            }
            if (month != -1)
            {
                md.Month = month;
            }
            if (day != -1)
            {
                md.Day = day;
            }
            if (hour != -1)
            {
                md.Hour = hour;
            }
            if (minute != -1)
            {
                md.Minute = minute;
            }
        }
    }
}
