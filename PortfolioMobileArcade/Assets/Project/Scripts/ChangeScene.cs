#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor
{
    private const string SCENE_GAME = "Game";
    private const string SCENE_CAR = "Car";


    [MenuItem("Open Scene/Game #1")]
    public static void OpenMainScene()
    {
        OpenScene(SCENE_GAME);
    }

    [MenuItem("Open Scene/Car #3")]
    public static void OpenCarScene()
    {
        OpenScene(SCENE_CAR);
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