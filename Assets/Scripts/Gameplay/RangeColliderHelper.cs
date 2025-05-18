using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeColliderHelper : MonoBehaviour
{
    [SerializeField] private SphereCollider coll;
    private float _checkSphereRedius;

    public UnityAction<Collider> OnTriggerEnterEvent;
    public UnityAction<Collider> OnTriggerEnterExit;

    public void SetRangeRedius(float r)
    {
        _checkSphereRedius = r;
        coll.radius = r;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        OnTriggerEnterEvent?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;
        OnTriggerEnterExit?.Invoke(other);
    }
}
