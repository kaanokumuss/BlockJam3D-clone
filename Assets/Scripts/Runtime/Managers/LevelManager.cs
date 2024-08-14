using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] levelFiles;
    private LevelData[] _levelData;

    private void Awake()
    {
        ReadLevels();
    }

    void ReadLevels()
    {
        _levelData = new LevelData[levelFiles.Length];
        for (int i = 0; i < levelFiles.Length; i++)
        {
            _levelData[i] = JsonUtility.FromJson<LevelData>(levelFiles[i].text);
        }
    }

}
