using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSelection", menuName = "ScriptableObjects/LevelSelection")]
public class LevelSelectionSO : ScriptableObject
{
    public int levelIndex;
    public int score;
    public LevelData levelData;
    public Material[] sphereMaterials;
}