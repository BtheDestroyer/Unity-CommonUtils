using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils.Extensions
{
  /// <summary>
  /// Extension helper methods for <see cref="Animator"/>
  /// </summary>
  public static class AnimatorExtensions
  {
    /// <summary>
    /// Safely attempts to set a float <paramref name="value"/> in the <paramref name="animator"/>
    /// </summary>
    /// <param name="animator"><see cref="Animator"/> to set a parameter of</param>
    /// <param name="name">Name of the parameter within <paramref name="animator"/></param>
    /// <param name="value">Value to store in the parameter</param>
    public static void TrySetFloat(this Animator animator, string name, float value)
    {
      foreach (AnimatorControllerParameter parameter in animator.parameters)
      {
        if (parameter.name.GetHashCode() == name.GetHashCode())
        {
          animator.SetFloat(name, value);
          return;
        }
      }
    }

    /// <summary>
    /// <inheritdoc cref="TrySetFloat" path="/summary"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetFloat" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetFloat" path="/param[@name='name']"/></param>
    /// <param name="value"><inheritdoc cref="TrySetFloat" path="/param[@name='value']"/></param>
    public static void TrySetParameter(this Animator animator, string name, float value)
      => TrySetFloat(animator, name, value);

    /// <summary>
    /// Safely attempts to set a bool <paramref name="value"/> in the <paramref name="animator"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetFloat" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetFloat" path="/param[@name='name']"/></param>
    /// <param name="value"><inheritdoc cref="TrySetFloat" path="/param[@name='value']"/></param>
    public static void TrySetBool(this Animator animator, string name, bool value)
    {
      foreach (AnimatorControllerParameter parameter in animator.parameters)
      {
        if (parameter.name.GetHashCode() == name.GetHashCode())
        {
          animator.SetBool(name, value);
          return;
        }
      }
    }

    /// <summary>
    /// <inheritdoc cref="TrySetBool" path="/summary"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetBool" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetBool" path="/param[@name='name']"/></param>
    /// <param name="value"><inheritdoc cref="TrySetBool" path="/param[@name='value']"/></param>
    public static void TrySetParameter(this Animator animator, string name, bool value)
      => TrySetBool(animator, name, value);

    /// <summary>
    /// Safely attempts to set a trigger in the <paramref name="animator"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetFloat" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetFloat" path="/param[@name='name']"/></param>
    public static void TrySetTrigger(this Animator animator, string name)
    {
      foreach (AnimatorControllerParameter parameter in animator.parameters)
      {
        if (parameter.name.GetHashCode() == name.GetHashCode())
        {
          animator.SetTrigger(name);
          return;
        }
      }
    }

    /// <summary>
    /// Safely attempts to reset a trigger in the <paramref name="animator"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetFloat" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetFloat" path="/param[@name='name']"/></param>
    public static void TryResetTrigger(this Animator animator, string name)
    {
      foreach (AnimatorControllerParameter parameter in animator.parameters)
      {
        if (parameter.name.GetHashCode() == name.GetHashCode())
        {
          animator.ResetTrigger(name);
          return;
        }
      }
    }

    /// <summary>
    /// <inheritdoc cref="TrySetFloat" path="/summary"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetFloat" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetFloat" path="/param[@name='name']"/></param>
    /// <param name="value"><inheritdoc cref="TrySetFloat" path="/param[@name='value']"/></param>
    public static void TrySetInteger(this Animator animator, string name, int value)
    {
      foreach (AnimatorControllerParameter parameter in animator.parameters)
      {
        if (parameter.name.GetHashCode() == name.GetHashCode())
        {
          animator.SetInteger(name, value);
          return;
        }
      }
    }

    /// <summary>
    /// <inheritdoc cref="TrySetInteger" path="/summary"/>
    /// </summary>
    /// <param name="animator"><inheritdoc cref="TrySetInteger" path="/param[@name='animator']"/></param>
    /// <param name="name"><inheritdoc cref="TrySetInteger" path="/param[@name='name']"/></param>
    /// <param name="value"><inheritdoc cref="TrySetInteger" path="/param[@name='value']"/></param>
    public static void TrySetParameter(this Animator animator, string name, int value)
      => TrySetInteger(animator, name, value);
  }
}
