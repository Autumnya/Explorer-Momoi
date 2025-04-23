using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : SingletonMono<WorldManager>
{
    private static Dictionary<Collider, Entity> MapEntities;
    public static UnityAction OnActivateEvent;
    public static float Gravity;

    public Vector3 PlayerSpawnPoint;

    protected override void OnAwake()
    {
        MapEntities = new();
    }

    private void Update()
    {
        OnActivateEvent?.Invoke();
    }

    public void RegisterEntity(Collider collider, Entity entity)
    {
        MapEntities.Add(collider, entity);
    }

    public void UnregisterEntity(Collider collider)
    {
        MapEntities.Remove(collider);
    }

    public void OnHitEntity(Collider target, AttackInfo attackInfo)
    {
        if (MapEntities.TryGetValue(target, out Entity entity))
        {
            if (entity.State == EntityState.Dead)
            {
                MapEntities.Remove(target);
                return;
            }
            if (entity.IsAttackable)
                entity.ReceiveDamage(attackInfo);
        }
    }
}