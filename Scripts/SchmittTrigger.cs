using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Threshhold based float to boolean converter based on the <see href="https://en.wikipedia.org/wiki/Schmitt_trigger">Schmitt trigger</see> electronic component
  /// </summary>
  [System.Serializable]
  public class SchmittTrigger
  {
    /// <summary>
    /// Upper bound an input must exceed to set the trigger to true
    /// </summary>
    [SerializeField]
    private float m_upperBound;
    /// <summary>
    /// Lower bound an input must exceed to set the trigger to true
    /// </summary>
    [SerializeField]
    private float m_lowerBound;
    /// <summary>
    /// Current boolean state
    /// </summary>
    private bool m_value;
    /// <summary>
    /// Determines if the output should be inverted
    /// <param>Caused by the provided upper bound being less than the lower bound</param>
    /// </summary>
    private bool m_invert;

    /// <summary>
    /// Constructor
    /// <para>If provided upper bound is less than the lower bound, they will be swapped and the boolean output will be inverted</para>
    /// </summary>
    /// <param name="upperBound">Upper bound of the trigger</param>
    /// <param name="lowerBound">Lower bound of the trigger</param>
    public SchmittTrigger(float upperBound, float lowerBound)
    {
      m_invert = upperBound < lowerBound;
      m_upperBound = m_invert ? lowerBound : upperBound;
      m_lowerBound = m_invert ? upperBound : lowerBound;
    }

    /// <summary>
    /// Gets current state of the trigger as a boolean
    /// </summary>
    /// <param name="schmittTrigger">Trigger to retrieve teh state of</param>
    public static implicit operator bool(SchmittTrigger schmittTrigger)
    {
      return schmittTrigger.m_value;
    }

    /// <summary>
    /// Updates the internal boolean state of the trigger if the <paramref name="newValue"/> exceeds its upper or lower bounds
    /// </summary>
    /// <param name="newValue">Value to test against the trigger's upper and lower bounds</param>
    /// <returns><see cref="SchmittTrigger"/> instance the method was called on to enable <see href="https://en.wikipedia.org/wiki/Method_chaining">Method Chaining</see></returns>
    public SchmittTrigger Update(float newValue)
    {
      if (newValue > m_upperBound)
      {
        m_value = !m_invert;
      }
      else if (newValue < m_lowerBound)
      {
        m_value = m_invert;
      }
      return this;
    }

    /// <summary>
    /// Gets <see cref="m_upperBound"/>
    /// </summary>
    /// <returns>Upper bound of the trigger</returns>
    public float GetUpperBound()
    {
      return m_upperBound;
    }

    /// <summary>
    /// Gets <see cref="m_lowerBound"/>
    /// </summary>
    /// <returns>Lower bound of the trigger</returns>
    public float GetLowerBound()
    {
      return m_lowerBound;
    }
  }
}