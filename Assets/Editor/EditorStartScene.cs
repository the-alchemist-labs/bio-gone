using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class EditorStartScene
{
    private const string StartScene = "Assets/Scenes/LoadingScene.unity";
    private const string PreviousSceneKey = "EditorStartScene_PreviousScene";

    static EditorStartScene()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            string currentScene = EditorSceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PreviousSceneKey, currentScene);

            if (currentScene != StartScene)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(StartScene);
                }
                else
                {
                    EditorApplication.isPlaying = false;
                }
            }
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            string previousScene = EditorPrefs.GetString(PreviousSceneKey, string.Empty);
            if (!string.IsNullOrEmpty(previousScene) && previousScene != StartScene)
            {
                EditorApplication.delayCall += () => EditorSceneManager.OpenScene(previousScene);
            }
        }
    }
}