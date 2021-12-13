using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Wire Cube")]
  public class Gizmo_WireCube : Gizmo
  {
    protected override void DrawShape() => Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
  }
}
