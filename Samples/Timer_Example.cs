using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils.Examples
{
  public class Timer_Example : MonoBehaviour
  {
    void Update()
    {
      // Timers are an easy way of creating repeatable events without any initialization

      // This conditional will be true every 10 seconds
      if (Timer.Every(10.0f))
      {
        Debug.Log("10 seconds have passed!");
      }

      for (int i = 0; i < 4; ++i)
      {
        // However, Timers are identified by their stacktrace, so we need to provide 
        // an extra value so each for loop iteration can get a unique timer
        float time = 1.0f * i;
        if (Timer.Every(time, (ulong)i))
        {
          Debug.Log(time + " second(s) have passed!");
        }
      }
    }
  }
}
