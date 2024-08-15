using Runtime.SceneUtil;
using UnityEngine;

namespace SceneUtil
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelAsset", fileName = "NewLevelAsset")]
    public sealed class LevelAssetSO : ScriptableObject
    {
        public string ID;
        public SceneField Asset;
    }
}