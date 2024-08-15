using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
   // [SerializeField] LevelSelectionSO levelSelectionSo;
   [SerializeField] TextAsset[] levelFiles;
   LevelData[] _levelData; 
   LevelSaveData _levelSaveData;

   void Awake()
   {
      ReadLevels();
      Load();

      // LevelEvents.OnLevelSelected += LevelSelected;
      LevelEvents.OnLevelWin += Save_Callback;
      LevelEvents.OnLevelDataNeeded += LevelDataNeeded_Callback;
      // GameEvents.OnCompleted += OnCompleteCallback;
   }

   void OnDestroy()
   {
     // LevelEvents.OnLevelSelected -= LevelSelected;
      LevelEvents.OnLevelWin -= Save_Callback;
      LevelEvents.OnLevelDataNeeded -= LevelDataNeeded_Callback;
      // GameEvents.OnCompleted -= OnCompleteCallback;
   }
   
   // void LevelSelected(int index)
   // {
   //    levelSelectionSo.levelIndex = index;
   //    levelSelectionSo.levelData = _levelData[index];
   //    levelSelectionSo.score = _levelSaveData.Data[index].highScore;
   //    SceneEvents.OnLoadGameScene?.Invoke();
   // }

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
   
}
