using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BryceDixon.CommonUtils.Examples
{
  // Using RequireComponent can guarantee the owning GameObject has the component we're looking for
  // However, this is not required
  [RequireComponent(typeof(Rigidbody))]
  [AddComponentMenu("CommonUtils/Examples/SafeComponent")]
  public class SafeComponent_Example : MonoBehaviour
  {
    // We can use SafeComponent<Component> as a wrapper around GetComponent<Component> for a few benefits:
    //   - It caches the component after it's been retrieved, making all uses after the first much faster
    //   - If the component is destroyed, it will automatically start looking for a replacement
    //   - It will wait until it's used to call GetComponent for the first time,
    //       so the desired Component doesn't have to exist at Start like in typical uses of GetComponent
    // Other variants also exist:
    //   - SafeComponents<Component> gets an array of all Components on the object instead of just one
    //   - SafeFindObject<Object> wraps around FindObjectOfType instead of GetComponent
    //   - SafeFindObjects<Object> wraps around FindObjectsOfType instead of GetComponent
    private SafeComponent<Rigidbody> m_rigidbody;

    [SerializeField]
    [Min(0.1f)]
    private float m_time = 4.0f;
    [SerializeField]
    [Min(0.0f)]
    private float m_velocity = 2.0f;
    [SerializeField]
    private bool m_log = true;

    private void Start()
    {
      // SafeComponents cannot be constructed in the class definition as the constructor requires a reference to `this`
      m_rigidbody = new SafeComponent<Rigidbody>(this);
    }

    private void Update()
    {
      if (Timer.Every(m_time))
      {
        if (m_log)
        {
          Debug.Log("Adding force through " + m_rigidbody.GetType().ToString());
        }
        // The first way to get the Component from a SafeComponent is using the Get() method
        if (m_rigidbody.Get() != null)
        {
          // The second method is by casting it to its wrapped Component type
          Rigidbody rb = m_rigidbody;
          rb.velocity = Vector3.up * m_velocity;
        }
      }
    }
  }
}
