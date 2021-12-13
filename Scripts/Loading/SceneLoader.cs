using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Component for loading other scenes
  /// </summary>
  [AddComponentMenu("CommonUtils/Scenes/Scene Loader")]
  public class SceneLoader : MonoBehaviour
  {
    /// <summary>
    /// Scene to load when <see cref="LoadScene()"/> is called
    /// </summary>
    [NaughtyAttributes.HideIf("IsLoadingScene")]
    [NaughtyAttributes.Scene]
    public string m_sceneName;

#if UNITY_EDITOR
    [NaughtyAttributes.Button("Edit Loading Screen Settings")]
    private void SelectLoadingSettings()
    {
      UnityEditor.Selection.activeObject = LoadingSettings.GetSettings();
    }
#endif

    /// <summary>
    /// Destination scene to load the next time we're in the loading scene (<see cref="LoadingSettings.loadingScene"/>)
    /// </summary>
    [NaughtyAttributes.ShowIf("IsLoadingScene")]
    [NaughtyAttributes.ShowNonSerializedField]
    [NaughtyAttributes.Scene]
    private static string m_destinationScene = "";

    /// <summary>
    /// Current progress when loading a new scene in the range [0, 1]
    /// </summary>
    public static float m_loadProgress { private set; get; }

    private void Start()
    {
      if (IsLoadingScene())
      {
        StartCoroutine(LoadSceneAsync(m_destinationScene));
      }
    }

    /// <summary>
    /// Asyncronously loads a scene in the background
    /// </summary>
    /// <param name="scene">Scene to load</param>
    /// <returns>Coroutine status</returns>
    private static IEnumerator LoadSceneAsync(string scene)
    {
      AsyncOperation asyncLoad;
      m_loadProgress = 0f;
      if (IsLoadingScene(m_destinationScene) || !Scene.SceneExists(m_destinationScene))
      {
        Debug.LogError("Attempted to load scene that doesn't exist (\"" + m_destinationScene + "\"). Falling back on failsafe.");
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadingSettings.GetSettings().failsafeScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
      }
      else
      {
        asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_destinationScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
      }
      // Wait until the asynchronous scene fully loads
      while(!asyncLoad.isDone)
      {
        m_loadProgress = asyncLoad.progress;
        yield return null;
      }
      m_loadProgress = 1f;
      UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(LoadingSettings.GetSettings().loadingScene);
    }

    /// <summary>
    /// Loads the scene delegated by <see cref="m_sceneName"/>
    /// </summary>
    public void LoadScene()
    {
      LoadScene(m_sceneName);
    }

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="scene">Scene name to load</param>
    public static void LoadScene(string scene)
    {
      m_destinationScene = scene;
      UnityEngine.SceneManagement.SceneManager.LoadScene(LoadingSettings.GetSettings().loadingScene);
    }

    /// <summary>
    /// Determines if the current scene is the loading scene (<see cref="LoadingSettings.loadingScene"/>)
    /// </summary>
    /// <returns><see langword="true"/> if the current scene is the loading scene, <see langword="false"/> otherwise</returns>
    public bool IsLoadingScene()
    {
      return IsLoadingScene(Scene.GetCurrentScene().name);
    }

    /// <summary>
    /// Determines if a given scene name is the loading scene's name (<see cref="LoadingSettings.loadingScene"/>)
    /// </summary>
    /// <param name="scene">Scene name to check</param>
    /// <returns><see langword="true"/> if the given <paramref name="scene"/> is the loading scene's name, <see langword="false"/> otherwise</returns>
    public static bool IsLoadingScene(string scene)
    {
      return LoadingSettings.GetSettings().loadingScene == scene;
    }
  }
}
