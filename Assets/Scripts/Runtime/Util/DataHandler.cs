using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandler 
{
    public static void Save<T>(T saveData, string key)
    {
        var data = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(key,data);
    }

    public static T Load<T>(string key) where T : new()
    {
        var data = PlayerPrefs.GetString(key);
        var loadedData = JsonUtility.FromJson<T> (data) ?? new T();
        return loadedData;
    }

    public static void Delete(string key)
    {
        if (HasData(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static bool HasData(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
