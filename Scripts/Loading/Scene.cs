using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Helper methods for interacting with scenes
  /// </summary>
  public static class Scene
  {
    /// <summary>
    /// Gets the current scene
    /// </summary>
    /// <returns>Currently running scene</returns>
    public static UnityEngine.SceneManagement.Scene GetCurrentScene() => UnityEngine.SceneManagement.SceneManager.GetActiveScene();

    /// <summary>
    /// Determines if a scene with the given name exists in the build
    /// </summary>
    /// <param name="scene">Scene name to test</param>
    /// <returns><see langword="true"/> if the given <paramref name="scene"/> exists in the build, <see langword="false"/> otherwise</returns>
    public static bool SceneExists(string scene)
    {
      for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; ++i)
      {
        string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
        int lastSlash = scenePath.LastIndexOf('/');
        if (scenePath.Substring(lastSlash + 1) == scene + ".unity")
        {
          return true;
        }
      }
      return false;
    }
  }
}
