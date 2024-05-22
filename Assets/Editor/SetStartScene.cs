#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace JokerGames
{
    [InitializeOnLoad]
    public class SetStartScene 
    {
        static SetStartScene()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/MainMenu.unity");
        }
    }
}

#endif
