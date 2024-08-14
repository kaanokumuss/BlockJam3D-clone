using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] levelFiles;
    private LevelData[] _levelData;
    private LevelSaveData _levelSaveData;

    private void Awake()
    {
        ReadLevels();
        Load();
        LevelEvents.OnLevelWin += Save_Callback;
        LevelEvents.OnLevelDataNeeded += LevelDataNeeded_Callback; //leveldata lazim olunca 
        //LevelEvents.OnLevelSelected += LevelDataNeeded_Callback(); //leveldata lazim olunca 
        
    }

    private void OnDestroy()
    {
        LevelEvents.OnLevelWin -= Save_Callback;
        LevelEvents.OnLevelDataNeeded -= LevelDataNeeded_Callback;
    }

    void ReadLevels()
    {
        _levelData = new LevelData[levelFiles.Length];
        for (int i = 0; i < levelFiles.Length; i++)
        {
            _levelData[i] = JsonUtility.FromJson<LevelData>(levelFiles[i].text);
        }
    }

    void Load()
    {
        // todo: save edilen data var mı diye bak ,  level save datasını çek . 
        if (DataHandler.HasData(DataKeys.LevelScoreDataKey)) // burada bir data var mi diye checkliyoruz. key tutmak gerekiyor bunun icin yeni sc actik 
        { 
            //yukaridaki level save datayi data handler daki load la json a aktariyoruz . içine LevelSaveData class ını veriyoruz. çünkü ordaki şeyleri kullanacağız . 
            _levelSaveData = DataHandler.Load<LevelSaveData>(DataKeys.LevelScoreDataKey);
        }
        else // Oyuna ilk başladığımızda ya da data silindiğinde
        {
            _levelSaveData = new LevelSaveData(new LevelScoresData[_levelData.Length]); // yeni oluşturulan dataları setle
            for (int i = 0; i<_levelData.Length; i++)
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
        //bir sonraki leveli aç 
        _levelSaveData.Data[completeData.Index + 1].isUnlocked = true;
        
        //şuanki levellin yüksek skorunu güncelle
        _levelSaveData.Data[completeData.Index].highScore = completeData.Score;
        DataHandler.Save(_levelSaveData,DataKeys.LevelScoreDataKey);
    }

    void LevelDataNeeded_Callback()
    {
        LevelEvents.OnSpawnLevelSelectionButton?.Invoke(_levelSaveData.Data);
    }

}
