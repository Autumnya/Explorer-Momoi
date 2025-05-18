using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldManager : SingletonMono<WorldManager>
{
    public Dictionary<Collider, Entity> MapEntities;
    public UnityAction OnActivateEvent;
    public float Gravity;

    public Vector3 PlayerSpawnPoint;

    protected override void OnAwake()
    {
        MapEntities = new();
    }

    private void Update()
    {
        OnActivateEvent?.Invoke();
    }
    public Character CreatePlayerCharacter()
    {
        Character c = CacheManager.Instance.GetPlayerCache();
        c.gameObject.transform.SetParent(gameObject.transform);
        c.gameObject.transform.position = PlayerSpawnPoint;
        RegisterEntity(c.gameObject.GetComponent<CapsuleCollider>(), c);
        return c;
    }
    public void CreateCharacter(int characterId)
    {
        Character c;
        c = EntityFactory.Instance.CreateCharacter(characterId);
        c.SetSkillAsDefault();
        c.gameObject.transform.SetParent(gameObject.transform);
        RegisterEntity(c.GetComponent<CapsuleCollider>(), c);
    }

    private void RegisterEntity(Collider collider, Entity entity)
    {
        MapEntities.Add(collider, entity);
    }

    private void UnregisterEntity(Collider collider)
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

public class EntityFactory : SingletonMono<EntityFactory>
{
    public Character CreateCharacter(int characterId)
    {
        CharacterDefine define = DataManager.Instance.CharactersDefineDic[characterId];
        return Instantiate(define.EntityData.Obj).GetComponent<Character>();
    }
}