using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Line (Transforms)")]
  public class Gizmo_TransformLine : Gizmo
  {
    public List<Transform> m_objects = new List<Transform>();
    public bool m_completeLoop = false;

    protected override void DrawShape()
    {
      Gizmos.matrix = Matrix4x4.identity;
      for (int i = 1; i < m_objects.Count; ++i)
      {
        Gizmos.DrawLine(m_objects[i - 1].position, m_objects[i].position);
      }
      if (m_completeLoop && m_objects.Count > 2)
      {
        Gizmos.DrawLine(m_objects[m_objects.Count - 1].position, m_objects[0].position);
      }
    }
  }
}
