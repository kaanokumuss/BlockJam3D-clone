using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SceneBootstrapper 
{
    private const string PreviousSceneKey = "PreviousScene";
    private const string ShouldLoadBootstrapKey = "ShouldLoadBootstrap";
    private const string LoadBootstrapMenu = "GD101/Load Boot Scene On Play";
    private const string DontLoadBootstrapMenu = "GD101/Don't Load Boot Scene On Play";
    private static string BootstrapScene => EditorBuildSettings.scenes[0].path;

    private static string PreviousScene
    {
        get => EditorPrefs.GetString(PreviousSceneKey);
        set => EditorPrefs.SetString(PreviousSceneKey, value);
    }

    private static bool ShouldLoadBootstrapScene
    {
        get => EditorPrefs.GetBool(ShouldLoadBootstrapKey, true);
        set => EditorPrefs.SetBool(ShouldLoadBootstrapKey, value);
    }

    static SceneBootstrapper()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange playModeState)
    {
        if (!ShouldLoadBootstrapScene)
        {
            return;
        }

        switch (playModeState)
        {
            case PlayModeStateChange.ExitingEditMode:
                PreviousScene = SceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() 
                    && IsSceneInBuildSettings(BootstrapScene))
                {
                    EditorSceneManager.OpenScene(BootstrapScene);
                }
                break;
            
            case PlayModeStateChange.EnteredEditMode:
                if (!string.IsNullOrEmpty(PreviousScene))
                {
                    EditorSceneManager.OpenScene(PreviousScene);
                }
                break;
        }
    }

    static bool IsSceneInBuildSettings(string scenePath)
    {
        return !string.IsNullOrEmpty(scenePath)
               && EditorBuildSettings.scenes.Any(scene => scene.path == scenePath);
    }
    
    [MenuItem(LoadBootstrapMenu)]
    static void EnableBootstrapper()
    {
        ShouldLoadBootstrapScene = true;
    }
    
    [MenuItem(DontLoadBootstrapMenu)]
    static void DisableBootstrapper()
    {
        ShouldLoadBootstrapScene = false;
    }
    
    [MenuItem(LoadBootstrapMenu, true)]
    static bool ValidateEnableBootstrapper()
    {
        return !ShouldLoadBootstrapScene;
    }
    
    [MenuItem(DontLoadBootstrapMenu, true)]
    static bool ValidateDisableBootstrapper()
    {
        return ShouldLoadBootstrapScene;
    }
}