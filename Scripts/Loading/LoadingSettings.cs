using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Settings for loading screens
  /// </summary>
  [SettingsPath("Settings/Loading")]
  public class LoadingSettings : Settings<LoadingSettings>
  {
    [SerializeField]
    [NaughtyAttributes.Scene]
    private string m_loadingScene;
    /// <summary>
    /// Name of the loading scene
    /// </summary>
    public string loadingScene { get => m_loadingScene; }

    [NaughtyAttributes.InfoBox("Scene to fall back on if someone tries to load a scene that doesn't exist.")]
    [SerializeField]
    [NaughtyAttributes.Scene]
    private string m_failsafeScene;
    /// <summary>
    /// Name of a failsafe scene to use if we try to load a scene that doesn't exist
    /// </summary>
    public string failsafeScene { get => m_failsafeScene; }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("CommonUtils/Settings/Create Loading Screen Settings", priority = 1)]
    private static new void CreateSettings()
    {
      Settings<LoadingSettings>.CreateSettings();
    }
#endif
  }
}
