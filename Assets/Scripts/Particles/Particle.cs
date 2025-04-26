using UnityEngine;

public abstract class Particle : MonoBehaviour
{
    [SerializeField] protected LayerMask EntityMask;
    [SerializeField] protected LayerMask TerrainMask;

    public AttackInfo AtkInfo;

    protected bool Activated;
    protected bool EnableCollider;

    private void Awake()
    {
        Activated = false;
        EnableCollider = true;

        OnAwake();
    }

    protected void Activate()
    {
        Activated = true;
    }

    protected void Update()
    {
        if(Activated)
        {
            OnActing();
        }
    }

    protected void OnDestroy()
    {
    }

    protected virtual void OnTriggerEnterEntity(Collider entityTrigger)
    {
    }
    protected virtual void OnAwake()
    {
    }
    protected abstract void OnActing();
}