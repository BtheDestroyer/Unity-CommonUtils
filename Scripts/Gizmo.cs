using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Base class for attaching simple <see cref="Gizmos"/> to <see cref="GameObject"/>s
  /// </summary>
  public abstract class Gizmo : MonoBehaviour
  {
    /// <summary>
    /// Used to determine when a <see cref="Gizmo"/> should be drawn
    /// </summary>
    public enum DrawMode
    {
      Always,
      OnlyWhenSelected
    }

    public DrawMode m_drawMode = DrawMode.Always;
    public Color m_color = Color.white;

    /// <summary>
    /// Method implemented by derived classes which draws the proper <see cref="Gizmos"/>
    /// </summary>
    protected abstract void DrawShape();

    private void OnDrawGizmos()
    {
      if (m_drawMode == DrawMode.Always)
      {
        Gizmos.color = m_color;
        Gizmos.matrix = transform.localToWorldMatrix;
        DrawShape();
        Gizmos.matrix = Matrix4x4.identity;
      }
    }

    private void OnDrawGizmosSelected()
    {
      if (m_drawMode == DrawMode.OnlyWhenSelected)
      {
        Gizmos.color = m_color;
        Gizmos.matrix = transform.localToWorldMatrix;
        DrawShape();
        Gizmos.matrix = Matrix4x4.identity;
      }
    }
  }
}
