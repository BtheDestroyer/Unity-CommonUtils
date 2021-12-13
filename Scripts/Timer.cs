using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Class used for in-place timing operations.
  /// </summary>
  public class Timer
  {
    /// <summary>
    /// Private constructor to prevent improper usage.
    /// </summary>
    private Timer() {}

    private struct ID
    {
      public ulong val;

      public static ID FromBytes(byte[] bytes)
      {
        ID id = new ID();
        for (int i = 0; i < bytes.Length; ++i)
        {
          id.val ^= ((ulong)bytes[i]) << (8 * (i % 8));
        }
        return id;
      }
    }

    /// <summary>
    /// Static list of active timers.
    /// </summary>
    private static Dictionary<ID, Timer> s_timers = new Dictionary<ID, Timer>();

    /// <summary>
    /// Tracks when a timer was started.
    /// </summary>
    private float m_startTime;

    /// <summary>
    /// Used to pass a conditional repeatedly after a delay.
    /// </summary>
    /// <param name="seconds">How long between every pass of the conditional.</param>
    /// <returns><see langword="true"/> once every given <paramref name="seconds"/>, <see langword="false"/> all other times.</returns>
    public static bool Every(float seconds, ulong idSalt = 0)
    {
      return Every<MD5>(seconds, idSalt);
    }

    /// <summary>
    /// <inheritdoc cref="Every(float)" path="/summary"/>
    /// </summary>
    /// <typeparam name="Algorithm"><see cref="HashAlgorithm"/> to use to determine the <see cref="Timer"/> ID from the stacktrace.</typeparam>
    /// <param name="seconds">
    /// <inheritdoc cref="Every(float)" path="/param"/>
    /// </param>
    /// <returns>
    /// <inheritdoc cref="Every(float)" path="/returns"/>
    /// </returns>
    public static bool Every<Algorithm>(float seconds, ulong idSalt = 0) where Algorithm : HashAlgorithm
    {
      return Every(seconds, HashAlgorithm.Create(typeof(Algorithm).ToString()), idSalt);
    }

    /// <summary>
    /// <inheritdoc cref="Every(float)" path="/summary"/>
    /// </summary>
    /// <param name="seconds">
    /// <inheritdoc cref="Every(float)" path="/param"/>
    /// </param>
    /// <param name="algorithm">
    /// <inheritdoc cref="Every{Algorithm}(float)" path="/typeparam"/>
    /// </param>
    /// <returns>
    /// <inheritdoc cref="Every(float)" path="/returns"/>
    /// </returns>
    public static bool Every(float seconds, HashAlgorithm algorithm, ulong idSalt = 0)
    {
      string stack = System.Environment.StackTrace;
      byte[] hash = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(stack));
      ID id = ID.FromBytes(hash);
      id.val ^= idSalt;
      Timer timer;
      if (!s_timers.TryGetValue(id, out timer))
      {
        timer = new Timer();
        timer.m_startTime = Time.realtimeSinceStartup;
        s_timers.Add(id, timer);
      }
      float difference = (timer.m_startTime + seconds) - Time.realtimeSinceStartup;
      if (difference <= 0.0f)
      {
        if (-difference > seconds)
        {
          timer.m_startTime = Time.realtimeSinceStartup;
        }
        else
        {
          timer.m_startTime = Time.realtimeSinceStartup + difference;
        }
        return true;
      }
      return false;
    }
  }
}
