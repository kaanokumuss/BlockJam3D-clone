using UnityEngine;

namespace Runtime.SceneUtil
{
    [System.Serializable]
    public class SceneField : ISerializationCallbackReceiver
    {

#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;
#endif

#pragma warning disable 414
        [SerializeField, HideInInspector] string sceneName = "";
#pragma warning restore 414

        public static implicit operator string(SceneField sceneField)
        {
#if UNITY_EDITOR
            return System.IO.Path.GetFileNameWithoutExtension(UnityEditor.AssetDatabase.GetAssetPath(sceneField.sceneAsset));
#else
        return sceneField.sceneName;
#endif
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            sceneName = this;
#endif
        }
        public void OnAfterDeserialize() { }
    }
}