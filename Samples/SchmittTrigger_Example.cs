using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils.Examples
{
  public class SchmittTrigger_Example : MonoBehaviour
  {
    // SchmittTriggers can be useful for preventing rapid on-off cycling when conditions are near their change threshhold
    // For example, if we check an analogue trigger against an exact value like this:
    //   if (Input.GetAxis("Horizontal") > 0.5f)
    // That could cause a rapid switching between true and false if the player were to hold the stick near 0.5f
    // Instead, we can make a SchmittTrigger to provide some buffered range to the tested input
    // This can also be useful for testing object positions, distances, etc.
    // For example, we could make an AI that becomes agressive when the player is within 5 units,
    //   but only becomes passive again once they're more than 10 units away.
    private SchmittTrigger m_active = new SchmittTrigger(0.75f, 0.25f);

    void Update()
    {
      // This will evaluate as true when m_active first crosses its upper threshold
      // In order to evaluate as true again, it will have to pass its lower threshold
      if (!m_active && m_active.Update(Input.GetAxis("Horizontal")))
      {
        Debug.Log("Input became active!");
      }
    }
  }
}
