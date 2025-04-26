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

    public bool OnHitEntity(Collider target, AttackInfo attackInfo)
    {
        if (MapEntities.TryGetValue(target, out Entity entity))
        {
            Character attacker = attackInfo.Attacker;
            EntityType attackerType = attacker.Type;
            EntityType receiverType = entity.Type;


            if (entity.State == EntityState.Dead)
            {
                MapEntities.Remove(target);
                return true;
            }
            if (entity.IsAttackable)
                entity.ReceiveDamage(attackInfo);
        }
        return false;
    }

    private bool IsEffectiveAttack(EntityType attackerType, EntityType receiverType)
    {
        if (attackerType == receiverType)
            return false;
        if (receiverType == EntityType.Undefined)
            return true;
        if(attackerType == EntityType.Player || attackerType == EntityType.Teammate)
        {
            if(receiverType == EntityType.Enemy)
                return true;
            return false;
        }
        if(attackerType == EntityType.Enemy)
        {
            if(receiverType == EntityType.Player || receiverType == EntityType.Teammate)
                return true;
            return false;
        }
        return false;
    }
}