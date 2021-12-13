using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Line")]
  public class Gizmo_Line : Gizmo
  {
    public List<Vector3> m_points = new List<Vector3>(new Vector3[2] { Vector3.zero, new Vector3(1f, 0f, 0f) });
    public bool m_completeLoop = false;
    public bool m_drawPoints = false;

    protected override void DrawShape()
    {
      for (int i = 1; i < m_points.Count; ++i)
      {
        Gizmos.DrawLine(m_points[i - 1], m_points[i]);
      }
      if (m_completeLoop && m_points.Count > 2)
      {
        Gizmos.DrawLine(m_points[m_points.Count - 1], m_points[0]);
      }
      if (m_drawPoints)
      {
        for (int i = 0; i < m_points.Count; ++i)
        {
          Gizmos.DrawSphere(m_points[i], 0.1f);
        }
      }
    }
  }
}
