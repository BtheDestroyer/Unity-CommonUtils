using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Wire Sphere")]
  public class Gizmo_WireSphere : Gizmo
  {
    protected override void DrawShape() => Gizmos.DrawWireSphere(Vector3.zero, 0.5f);
  }
}
