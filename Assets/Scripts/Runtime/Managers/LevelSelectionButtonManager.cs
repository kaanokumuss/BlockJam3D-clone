using System;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelSelectionButtonManager : MonoBehaviour
    {
        [Header("Spawn object prefab"), SerializeField] LevelSelectionButton prefab;
        [Header("Where to spawn objects"), SerializeField] Transform spawnParent;
        LevelSelectionButton[] _buttons;

        void Awake()
        {
            LevelEvents.OnSpawnLevelSelectionButtons += Prepare;
            LevelEvents.OnLevelDataNeeded?.Invoke();
        }

        void OnDestroy()
        {
            LevelEvents.OnSpawnLevelSelectionButtons -= Prepare;
        }

        void Prepare(LevelScoresData[] data)
        {
            _buttons = new LevelSelectionButton[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                _buttons[i] = Instantiate(prefab, spawnParent);
                _buttons[i].Prepare(data[i]);
            }
        }
    }
}