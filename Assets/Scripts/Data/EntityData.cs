using UnityEngine;

[CreateAssetMenu(fileName = "NewEntity", menuName = "Data/Entity Data")]
public class EntityData : ScriptableObject
{
    public int EntityId = 0;
    public float MaxHealth = 100f;
    public float Speed = 10f;
    public float AttackPower = 10f;
    public float AttackRange = 1f;
    public float AttackSpeed = 1f;
    public float Defend = 0f;
    public float KnockbackResistance = 0f;
    public float ShieldHealth = 0f;
    public float SkillCooldown = 0f;
    public int MaxAmmo = 20;
    public GameObject Obj;
}