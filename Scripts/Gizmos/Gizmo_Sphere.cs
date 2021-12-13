using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Sphere")]
  public class Gizmo_Sphere : Gizmo
  {
    protected override void DrawShape() => Gizmos.DrawSphere(Vector3.zero, 0.5f);
  }
}
