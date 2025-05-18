using UnityEngine;

public abstract class Particle : MonoBehaviour
{
    [SerializeField] protected LayerMask EntityMask;
    [SerializeField] protected LayerMask TerrainMask;

    public AttackInfo AtkInfo;

    protected bool EnableCollider;

    private void Awake()
    {
        EnableCollider = true;

        OnAwake();
    }

    protected void Update()
    {
        OnActing();
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