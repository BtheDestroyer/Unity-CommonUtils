using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// <see href="https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern">CRTP</see> <see href="https://en.wikipedia.org/wiki/Singleton_pattern">Singleton</see> Script
  /// <para>A <see langword="class"/> that derives from <see cref="Singleton{T}"/> can only have one instance in the game
  /// and will automatically be created the first time it's used in external code</para>
  /// </summary>
  /// <typeparam name="T">
  /// Derived Class
  /// <example>
  /// <code>
  /// public class ExampleSingleton : Singleton&lt;ExampleSingleton&gt;
  /// </code>
  /// </example>
  /// </typeparam>
  public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
  {
    /// <summary>
    /// Used to send a message for the "OnSceneChange" method
    /// </summary>
    private string m_currentScene;

    protected void Start()
    {
      if (s_singleton == null || s_singleton == this)
      {
        s_singleton = this as T;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Debug.LogError("Tried to create a " + typeof(T).ToString() + " when one already exists");
        Destroy(this);
      }
    }

    protected void FixedUpdate()
    {
      string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
      if (m_currentScene != currentScene)
      {
        m_currentScene = currentScene;
        SendMessage("OnSceneChange", SendMessageOptions.DontRequireReceiver);
      }
    }

    /// <summary>
    /// Gets the <see href="https://en.wikipedia.org/wiki/Singleton_pattern">singleton</see> reference of <typeparamref name="T"/>
    /// <para>This will create an instance of <typeparamref name="T"/> on a new, empty <see cref="GameObject"/> named "<typeparamref name="T"/>" if it does not currently exist</para>
    /// </summary>
    /// <returns><see href="https://en.wikipedia.org/wiki/Singleton_pattern">Singleton</see> of <typeparamref name="T"/></returns>
    public static T Get()
    {
      if (s_singleton == null)
      {
        try
        {
          s_singleton = FindObjectOfType<T>();
          if (s_singleton == null)
          {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
              s_singleton = new GameObject(typeof(T).ToString()).AddComponent<T>();
            }
#else
            s_singleton = new GameObject(typeof(T).ToString()).AddComponent<T>();
#endif
          }
        }
        catch
        {
          // Accept defeat
        }
      }
      return s_singleton;
    }

    public static bool Exists()
    {
      return s_singleton != null;
    }

    static T s_singleton;
  }
}
