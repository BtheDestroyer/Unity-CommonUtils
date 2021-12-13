using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Safe and fast wrapper object around <see cref="GameObject.GetComponent{T}"/>
  /// <para>This <i>must</i> be manually initialized within the owning <see cref="MonoBehaviour"/>'s constructor.</para>
  /// </summary>
  /// <typeparam name="T"><see cref="Component"/> type to get from the owner</typeparam>
  public struct SafeComponent<T> where T : Component
  {
    /// <summary>
    /// Cached <see cref="Component"/>
    /// </summary>
    private T m_component;
    /// <summary>
    /// Owning script (ie: the class that contains this <see cref="SafeComponent{T}"/> variable)
    /// </summary>
    private MonoBehaviour m_owner;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="owner"><see cref="MonoBehaviour"/> to call <see cref="GameObject.GetComponent{T}"/> on</param>
    public SafeComponent(MonoBehaviour owner)
    {
      m_component = null;
      m_owner = owner;
    }

    /// <summary>
    /// Converter from a <see cref="SafeComponent{T}"/> to the actual <see cref="Component"/> it represents
    /// </summary>
    /// <param name="safeComponent"><see cref="SafeComponent{T}"/> to convert from</param>
    public static implicit operator T(SafeComponent<T> safeComponent)
    {
      return safeComponent.Get();
    }

    /// <summary>
    /// Retrieves specified <see cref="Component"/>
    /// </summary>
    /// <returns><see cref="Component"/> from the owner matching the type <typeparamref name="T"/></returns>
    public T Get()
    {
      if (m_component == null && m_owner != null)
      {
        m_component = m_owner.GetComponent<T>();
      }
      return m_component;
    }
  }

  /// <summary>
  /// Safe and fast wrapper object around <see cref="GameObject.GetComponents{T}"/>
  /// <para>This <i>must</i> be manually initialized within the owning <see cref="MonoBehaviour"/>'s constructor.</para>
  /// </summary>
  /// <typeparam name="T"><see cref="Component"/> type to get from the owner</typeparam>
  public struct SafeComponents<T> where T : Component
  {
    /// <summary>
    /// Cached <see cref="Component"/>s
    /// </summary>
    private T[] m_components;
    /// <summary>
    /// Owning script (class that contains this <see cref="SafeComponent{T}"/> variable)
    /// </summary>
    private MonoBehaviour m_owner;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="owner"><see cref="MonoBehaviour"/> to call <see cref="GameObject.GetComponents{T}"/> on</param>
    public SafeComponents(MonoBehaviour owner)
    {
      m_components = null;
      m_owner = owner;
    }

    /// <summary>
    /// Converter from a <see cref="SafeComponent{T}"/> to the actual <see cref="Component"/>s it represents
    /// </summary>
    /// <param name="safeComponent"><see cref="SafeComponent{T}"/> to convert from</param>
    public static implicit operator T[](SafeComponents<T> safeComponent)
    {
      return safeComponent.Get();
    }

    /// <summary>
    /// Subscript operator to interact with the cached <see cref="Component"/>s
    /// </summary>
    /// <param name="i">Index to subsript with</param>
    /// <returns>Component at <paramref name="i"/></returns>
    public T this[int i]
    {
      get => Get()[i];
      set => Get()[i] = value;
    }

    /// <summary>
    /// Retrieves specified <see cref="Component"/>s
    /// </summary>
    /// <returns><see cref="Component"/>s from the owner matching the type <typeparamref name="T"/></returns>
    public T[] Get()
    {
      if (m_components == null || m_components.Length == 0 && m_owner != null)
      {
        m_components = m_owner.GetComponents<T>();
      }
      return m_components;
    }
  }

  /// <summary>
  /// Safe and fast wrapper object around <see cref="Object.FindObjectOfType{T}"/>
  /// </summary>
  /// <typeparam name="T"><see cref="Object"/> type to find</typeparam>
  public struct SafeFindObject<T> where T : Object
  {
    /// <summary>
    /// Cached <see cref="Object"/>
    /// </summary>
    private T m_object;

    /// <summary>
    /// Converter from a <see cref="SafeFindObject{T}"/> to the actual <see cref="Object"/> it represents
    /// </summary>
    /// <param name="safeComponent"><see cref="SafeFindObject{T}"/> to convert from</param>
    public static implicit operator T(SafeFindObject<T> safeFinder) => safeFinder.Get();

    /// <summary>
    /// Retrieves specified <see cref="Object"/>
    /// </summary>
    /// <returns><see cref="Object"/> matching the type <typeparamref name="T"/></returns>
    public T Get()
    {
      if (m_object == null)
      {
        m_object = Object.FindObjectOfType<T>();
      }
      return m_object;
    }
  }

  /// <summary>
  /// Safe and fast wrapper object around <see cref="Object.FindObjectsOfType{T}"/>
  /// </summary>
  /// <typeparam name="T"><see cref="Object"/> type to find</typeparam>
  public struct SafeFindObjects<T> where T : Object
  {
    /// <summary>
    /// Cached <see cref="Object"/>
    /// </summary>
    private T[] m_object;

    /// <summary>
    /// Converter from a <see cref="SafeFindObjects{T}"/> to the actual <see cref="Object"/> it represents
    /// </summary>
    /// <param name="safeComponent"><see cref="SafeFindObjects{T}"/> to convert from</param>
    public static implicit operator T[](SafeFindObjects<T> safeFinder) => safeFinder.Get();

    /// <summary>
    /// Retrieves specified <see cref="Object"/>
    /// </summary>
    /// <returns><see cref="Object"/> matching the type <typeparamref name="T"/></returns>
    public T[] Get()
    {
      if (m_object == null || m_object.Length == 0)
      {
        m_object = Object.FindObjectsOfType<T>();
      }
      return m_object;
    }
  }
}
