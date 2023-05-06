#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor
{
    private const string SCENE_MERGED = "ScriptTesting";
    private const string SCENE_CAR = "Car";
    private const string SCENE_ENVIRONMENT = "EnvironmentAssets";
    private const string SCENE_MAINMENU = "MainMenu";
    
        


    [MenuItem("Open Scene/Script Testing #1")]
    public static void OpenMainScene()
    {
        OpenScene(SCENE_MERGED);
    }

    [MenuItem("Open Scene/Car #3")]
    public static void OpenCarScene()
    {
        OpenScene(SCENE_CAR);
    }
    
     [MenuItem("Open Scene/Environment Assets #2")]
    public static void OpenEnvironmentAssetsScene()
    {
        OpenScene(SCENE_ENVIRONMENT);
    }
    
    [MenuItem("Open Scene/Main Menu #4")]
    public static void OpenMainMenuScene()
    {
        OpenScene(SCENE_MAINMENU);
    }
    
    private static void OpenScene(string sceneName)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Project/Scenes/" + sceneName + ".unity");
        }
    }
}
#endif