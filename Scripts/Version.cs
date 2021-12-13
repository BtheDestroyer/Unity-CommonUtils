using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils
{
  /// <summary>
  /// Value type for storing versioning information
  /// <para><example><c>(a/b) X.Y.Z</c></example></para>
  /// </summary>
  [System.Serializable]
  public struct Version
  {
    /// <summary>
    /// Different types/eras of development/builds
    /// </summary>
    public enum ReleaseType
    {
      /// <summary>
      /// AKA: "Feature Complete"
      /// </summary>
      Alpha,
      /// <summary>
      /// AKA: "Content Complete"
      /// </summary>
      Beta,
      /// <summary>
      /// AKA: "Production"
      /// </summary>
      Release
    }

    /// <summary>
    /// Build type
    /// </summary>
    public ReleaseType type;
    /// <summary>
    /// Major version
    /// </summary>
    public int major;
    /// <summary>
    /// Minor version
    /// </summary>
    public int minor;
    /// <summary>
    /// Version's revision
    /// </summary>
    public int revision;

    /// <summary>
    /// Constructor: X.Y.Z
    /// <para>Assumes <see cref="ReleaseType.Release"/></para>
    /// </summary>
    /// <param name="major">X in X.Y.Z</param>
    /// <param name="minor">Y in X.Y.Z</param>
    /// <param name="revision">Z in X.Y.Z</param>
    public Version(int major, int minor = 0, int revision = 0) : this(ReleaseType.Release, major, minor, revision) { }

    /// <summary>
    /// Constructor: (a/b) X.Y.Z
    /// </summary>
    /// <param name="type">(a/b) in (a/b) X.Y.Z</param>
    /// <param name="major">X in (a/b) X.Y.Z</param>
    /// <param name="minor">Y in (a/b) X.Y.Z</param>
    /// <param name="revision">Z in (a/b) X.Y.Z</param>
    public Version(ReleaseType type, int major, int minor = 0, int revision = 0)
    {
      this.type = type;
      this.major = major;
      this.minor = minor;
      this.revision = revision;
    }

    /// <summary>
    /// Converts a <see cref="Version"/> into a human-readable string
    /// </summary>
    /// <returns>
    /// <c>(a/b) X.Y.Z</c> where
    /// <list type="number">
    /// <item>
    /// (a/b) =
    /// <list type="bullet">
    /// <item>a if <see cref="type"/>==<see cref="ReleaseType.Alpha"/>; or</item>
    /// <item>b if <see cref="type"/>==<see cref="ReleaseType.Beta"/>; or</item>
    /// <item>? if <see cref="type"/> is invalid</item>
    /// </list>
    /// </item>
    /// <item>X = <see cref="major"/></item>
    /// <item>Y = <see cref="minor"/></item>
    /// <item>Z = <see cref="revision"/></item>
    /// </list>
    /// </returns>
    public override string ToString()
    {
      string v = major.ToString() + "." + minor.ToString() + "." + revision.ToString();
      switch (type)
      {
        case ReleaseType.Alpha:
          return "a " + v;
        case ReleaseType.Beta:
          return "b " + v;
        case ReleaseType.Release:
          return v;
      }
      return "?" + v;
    }

    /// <summary>
    /// Heirarchical override of equal-to comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&gt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator ==(Version lhs, Version rhs)
    {
      return (lhs.major == rhs.major)
        && (lhs.minor == rhs.minor)
        && (lhs.revision == rhs.revision)
        && (lhs.type == rhs.type);
    }

    /// <summary>
    /// Heirarchical override of equal-to comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&gt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator !=(Version lhs, Version rhs)
    {
      return !(lhs == rhs);
    }

    /// <summary>
    /// Heirarchical override of greater-than comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&gt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator >(Version lhs, Version rhs)
    {
      if (lhs.major != rhs.major)
      {
        return lhs.major > rhs.major;
      }
      if (lhs.minor != rhs.minor)
      {
        return lhs.minor > rhs.minor;
      }
      if (lhs.revision != rhs.revision)
      {
        return lhs.revision > rhs.revision;
      }
      if (lhs.type != rhs.type)
      {
        return lhs.type > rhs.type;
      }
      return false;
    }

    /// <summary>
    /// Heirarchical override of less-than comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&lt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator <(Version lhs, Version rhs)
    {
      return rhs > lhs;
    }

    /// <summary>
    /// Heirarchical override of greater-or-equal comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&lt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator >=(Version lhs, Version rhs)
    {
      return lhs > rhs || lhs == rhs;
    }

    /// <summary>
    /// Heirarchical override of less-than or equal comparison
    /// </summary>
    /// <param name="lhs">Left parameter</param>
    /// <param name="rhs">Right parameter</param>
    /// <returns>
    /// <paramref name="lhs"/>&lt;<paramref name="rhs"/> by testing in the following order:
    /// <list type="number">
    /// <item><see cref="major"/></item>
    /// <item><see cref="minor"/></item>
    /// <item><see cref="revision"/></item>
    /// <item><see cref="type"/></item>
    /// </list>
    /// </returns>
    public static bool operator <=(Version lhs, Version rhs)
    {
      return lhs < rhs || lhs == rhs;
    }
  }
}
