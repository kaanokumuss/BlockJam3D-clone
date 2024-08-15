using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using SceneUtil;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Assets")]
    [SerializeField] private LevelAssetSO metaSceneAsset;
    [SerializeField] private LevelAssetSO gameSceneAsset;

    private AsyncOperation _asyncOperation = new();

    private void Awake()
    {
        HandleFirstLoad();
    }

    void HandleFirstLoad()
    {
        StartCoroutine(LoadSceneAdditive(metaSceneAsset.Asset));
    }
    [Button]
    void LoadGameScene()
    {
        StartCoroutine(UnloadActiveSceneThenLoadScene(gameSceneAsset.Asset));
    }
    [Button]
    void LoadMetaScene()
    {
        StartCoroutine(UnloadActiveSceneThenLoadScene(metaSceneAsset.Asset));
    }
    
    IEnumerator UnloadActiveSceneThenLoadScene(string sceneName)
    {
        yield return UnloadSceneAdditive(SceneManager.GetActiveScene().name);
        yield return LoadSceneAdditive(sceneName);
    }
    
    IEnumerator LoadSceneAdditive(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _asyncOperation.allowSceneActivation = true;

        yield return new WaitUntil(()=> _asyncOperation.isDone);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    IEnumerator UnloadSceneAdditive(string sceneName)
    {
        _asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        yield return new WaitUntil(()=>_asyncOperation.isDone);
    }
}
