using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Frustum")]
  public class Gizmo_Frustum : Gizmo
  {
    public float m_fov = 45f;
    public float m_maxRange = 100f;
    public float m_minRange = 0.1f;
    [InspectorName("Aspect Ratio")]
    public Vector2 m_aspectRatioDimensions = new Vector2(16f, 9f);
    public float m_aspectRatio { get { return m_aspectRatioDimensions.x / m_aspectRatioDimensions.y; } }

    protected override void DrawShape() => Gizmos.DrawFrustum(Vector3.zero, m_fov, m_maxRange, m_minRange, m_aspectRatio);
  }
}
