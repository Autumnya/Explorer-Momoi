using UnityEngine;
using UnityEngine.Events;

public abstract class Particle : MonoBehaviour
{
    AttackInfo attackInfo;

    protected bool Activated;

    public void Activate()
    {
        Activated = true;
        WorldManager.OnActivateEvent += OnActive;
    }

    protected void OnDestroy()
    {
        WorldManager.OnActivateEvent -= OnActive;
    }

    protected abstract void OnActive();
}