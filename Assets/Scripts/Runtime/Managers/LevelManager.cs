using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [FormerlySerializedAs("LevelSelectionSo")] [SerializeField]
    private LevelSelectionSO LevelSelectionSO;

    [SerializeField] TextAsset[] levelFiles;
    LevelData[] _levelData;
    LevelSaveData _levelSaveData;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] private float delayBeforeLoadMetaScene = 5f; // Gecikme süresi

    void Awake()
    {
        ReadLevels();
        Load();
        scoreManager = FindObjectOfType<ScoreManager>();
        LevelEvents.OnLevelSelected += LevelSelected;
        LevelEvents.OnLevelWin += Save_Callback;
        LevelEvents.OnLevelDataNeeded += LevelDataNeeded_Callback;
        GameEvents.OnWin += Win;
        GameEvents.OnFail += Fail;
    }

    void OnDestroy()
    {
        GameEvents.OnWin -= Win;
        GameEvents.OnFail -= Fail;
        LevelEvents.OnLevelSelected -= LevelSelected;
        LevelEvents.OnLevelWin -= Save_Callback;
        LevelEvents.OnLevelDataNeeded -= LevelDataNeeded_Callback;
    }

    public void QuickPlay()
    {
        LevelSelected(0); // İlk seviyeyi seç ve oyunu başlat
    }

    void LevelSelected(int index)
    {
        LevelSelectionSO.levelIndex = index;
        LevelSelectionSO.levelData = _levelData[index];
        LevelSelectionSO.score = _levelSaveData.Data[index].highScore;
        SceneEvents.OnLoadGameScene?.Invoke();
    }

    void ReadLevels()
    {
        _levelData = new LevelData[levelFiles.Length];
        Debug.Log($"Total files to read: {levelFiles.Length}");

        for (int i = 0; i < levelFiles.Length; i++)
        {
            _levelData[i] = JsonUtility.FromJson<LevelData>(levelFiles[i].text);
        }
    }

    void Load()
    {
        if (DataHandler.HasData(DataKeys.LevelScoreDataKey))
        {
            _levelSaveData = DataHandler.Load<LevelSaveData>(DataKeys.LevelScoreDataKey);
        }
        else
        {
            _levelSaveData = new LevelSaveData(new LevelScoresData[_levelData.Length]);

            for (int i = 0; i < _levelData.Length; i++)
            {
                _levelSaveData.Data[i].index = i;
                _levelSaveData.Data[i].title = _levelData[i].title;
                _levelSaveData.Data[i].highScore = 0;
                _levelSaveData.Data[i].isUnlocked = false;
            }

            _levelSaveData.Data[0].isUnlocked = true;
            _levelSaveData.Data[0].highScore = 0;

            DataHandler.Save(_levelSaveData, DataKeys.LevelScoreDataKey);
        }
    }

    void Save_Callback(CompleteData completeData)
    {
        _levelSaveData.Data[completeData.Index + 1].isUnlocked = true;
        _levelSaveData.Data[completeData.Index].highScore = completeData.Score;
        DataHandler.Save(_levelSaveData, DataKeys.LevelScoreDataKey);
    }

    void LevelDataNeeded_Callback()
    {
        LevelEvents.OnSpawnLevelSelectionButtons?.Invoke(_levelSaveData.Data);
    }

    private void Win()
    {
        Save_Callback(GetCompleteData());
        GameEvents.WinPanel?.Invoke();
        StartCoroutine(LoadMetaSceneWithDelay());
    }

    private void Fail()
    {
        StartCoroutine(LoadMetaSceneWithDelay());
    }

    private IEnumerator LoadMetaSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoadMetaScene);
        LoadMetaScene();
    }

    private CompleteData GetCompleteData()
    {
        if (LevelSelectionSO == null)
        {
            Debug.LogError("LevelSelectionSO is null in GetCompleteData.");
        }

        if (scoreManager == null)
        {
            Debug.LogError("scoreManager is null in GetCompleteData.");
        }

        return new CompleteData(LevelSelectionSO.levelIndex, scoreManager.score);
    }

    private void LoadMetaScene()
    {
        SceneEvents.OnLoadMetaScene?.Invoke();
    }
}