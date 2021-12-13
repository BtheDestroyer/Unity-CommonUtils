using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  [AddComponentMenu("CommonUtils/Gizmos/Cube")]
  public class Gizmo_Cube : Gizmo
  {
    protected override void DrawShape() => Gizmos.DrawCube(Vector3.zero, Vector3.one);
  }
}
