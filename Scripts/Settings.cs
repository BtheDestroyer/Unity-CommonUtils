using System;
using System.Reflection;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// <see cref="Attribute"/> for determining the file path of a <see langword="class"/> deriving from <see cref="Settings{T}"/>
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class SettingsPathAttribute : Attribute
  {
    /// <summary>
    /// Determines the path of a <see cref="Settings{T}"/> file
    /// </summary>
    /// <param name="path">Path of the <see cref="Settings{T}"/> file</param>
    public SettingsPathAttribute(string path)
    {
      m_path = path;
    }

    public string m_path { get; private set; }
  }

  /// <summary>
  /// Internal usage only to be inherited directly by <see cref="Settings{T}"/>; use that instead.
  /// </summary>
  public class Settings : ScriptableObject
  {
#if UNITY_EDITOR
    /// <summary>
    /// Editor-only method to create a <see cref="Settings{T}"/> file for every derived <see langword="class"/> with a <see cref="SettingsPathAttribute"/>
    /// </summary>
    [UnityEditor.MenuItem("CommonUtils/Settings/Create All", priority = -10)]
    private static void CreateAllSettings()
    {
      foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        foreach (Type type in assembly.GetTypes())
        {
          if (type.IsSubclassOf(typeof(Settings)) && type.BaseType != typeof(Settings))
          {
            Type settingsType = type;
            while (settingsType.BaseType != typeof(Settings))
            {
              settingsType = settingsType.BaseType;
            }
            MethodInfo method = settingsType.GetMethod("CreateSettings");
            Debug.Log("Creating settings file for " + type.FullName);
            method.Invoke(null, null);
          }
        }
      }
    }
#endif
  }

  /// <summary>
  /// <see href="https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern">CRTP</see> Settings Class
  /// <para>Derived <see langword="class"/> <i>must</i> have a <see cref="SettingsPathAttribute"/> attribute to function properly</para>
  /// <example>
  /// <code>
  /// [SettingsPath("Settings/MySettings")]
  /// public class MySettings : BryceDixon.CommonUtils.Settings&lt;MySettings&gt; { /* ... */ }
  /// </code>
  /// </example>
  /// 
  /// <para>When inheriting, be sure to implement a way to create your <see langword="class"/>'s <see cref="Settings{T}"/> file by creating
  /// a <see langword="static"/> method with the <see cref="UnityEditor.MenuItem"/> attribute.</para>
  /// <example>
  /// <code>
  ///  #if UNITY_EDITOR
  ///    [UnityEditor.MenuItem("CommonUtils/Settings/Create My Settings", priority = 1)]
  ///    private static new void CreateSettings() => BryceDixon.CommonUtils.Settings&lt;MySettings&gt;.CreateSettings();
  ///  #endif
  /// </code>
  /// </example>
  /// </summary>
  /// <typeparam name="T">
  /// Derived Class
  /// <example>
  /// <code>
  /// public class ExampleSettings : Settings&lt;ExampleSettings&gt;
  /// </code>
  /// </example>
  /// </typeparam>
  public class Settings<T> : Settings where T : Settings<T>
  {
    /// <summary>
    /// <see cref="Settings{T}"/> related <see cref="Exception"/>
    /// </summary>
    public class SettingsException : Exception
    {
      public SettingsException(string comment) : base(comment) { }
    }

    /// <summary>
    /// Can be overloaded by derived classes to initialize data when they're loaded
    /// </summary>
    protected virtual void OnLoad() { }

    /// <summary>
    /// Gets the file path of a <see cref="Settings{T}"/> derived <see langword="class"/> as described in its <see cref="SettingsPathAttribute"/> attribute.
    /// </summary>
    /// <exception cref="SettingsException">Thrown if <typeparamref name="T"/> does not have a <see cref="SettingsPathAttribute"/> attribute</exception>
    /// <returns>File path of <typeparamref name="T"/></returns>
    public static string GetPath()
    {
      SettingsPathAttribute[] attributes = typeof(T).GetCustomAttributes(typeof(SettingsPathAttribute), true) as SettingsPathAttribute[];
      if (attributes.Length == 0)
      {
        throw new SettingsException("Attempted to get settings path from " + typeof(T).ToString() + " when it doesn't have a SettingsPath attribute.\nFix this by adding [SettingsPath(PATH)] above the class definition.");
      }
      return attributes[0].m_path;
    }

    /// <summary>
    /// Editor only method. Will create the provided folder (and any required parents) if it does not currently exist.
    /// </summary>
    /// <exception cref="SettingsException">Thrown if not in an editor environment</exception>
    /// <param name="folderPath">Folder of which to ensure the existence</param>
    private static void EnsureFolderExists(string folderPath)
    {
#if UNITY_EDITOR
      if (!UnityEditor.AssetDatabase.IsValidFolder(folderPath))
      {
        string parentFolderPath = folderPath.Substring(0, folderPath.LastIndexOf('/'));
        EnsureFolderExists(parentFolderPath);
        folderPath = folderPath.Substring(folderPath.LastIndexOf('/') + 1);
        UnityEditor.AssetDatabase.CreateFolder(parentFolderPath, folderPath);
      }
#else
      throw new SettingsException("Attempted to create a Settings file outside of the editor");
#endif
    }

    /// <summary>
    /// Editor only method. Creates <see cref="Settings{T}"/> file for <typeparamref name="T"/> if it does not exist.
    /// </summary>
    /// <exception cref="SettingsException">Thrown if not called from the main thread</exception>
    /// <exception cref="SettingsException">Thrown if not in an editor environment</exception>
    public static void CreateSettings()
    {
#if UNITY_EDITOR
      string path = GetPath();
      s_settings = Resources.Load<T>(path);
      if (s_settings == null)
      {
        s_settings = CreateInstance<T>();
        string fullPath = "Assets/Resources/" + path;
        string folderPath = fullPath.Substring(0, fullPath.LastIndexOf('/'));
        EnsureFolderExists(folderPath);
        UnityEditor.AssetDatabase.CreateAsset(s_settings, fullPath + ".asset");
        UnityEditor.AssetDatabase.SaveAssets();
      }
#else
      throw new SettingsException("Attempted to create a Settings file outside of the editor");
#endif
    }

    /// <summary>
    /// Gets a reference to the <see href="https://en.wikipedia.org/wiki/Singleton_pattern">singleton</see> of <typeparamref name="T"/>
    /// <para>This will automatically load the data from the relevant path (see <see cref="GetPath"/>) on its first call</para>
    /// <para>If in an editor context, this will call <see cref="CreateSettings"/> automatically if the relevant file does not exist</para>
    /// </summary>
    /// <returns><see href="https://en.wikipedia.org/wiki/Singleton_pattern">Singleton</see> of <typeparamref name="T"/></returns>
    public static T GetSettings()
    {
      if (s_settings == null)
      {
        s_settings = Resources.Load<T>(GetPath());
#if UNITY_EDITOR
        if (s_settings == null)
        {
          CreateSettings();
        }
#endif
        s_settings.OnLoad();
      }
      return s_settings;
    }

    static T s_settings;
  }
}