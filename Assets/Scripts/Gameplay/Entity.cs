using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EntityState
{
    Idle,
    Moving,
    Attacking,
    Dead
}
public enum EntityType
{
    Player,
    Teammate,
    Enemy,
    Environment
}
public class Entity : MonoBehaviour
{
    public EntityAttribute MaxHealth { get; private set; }
    public EntityAttribute Speed { get; private set; }
    public EntityAttribute AttackPower { get; private set; }
    public EntityAttribute AttackRange { get; private set; }
    public EntityAttribute AttackSpeed { get; private set; }
    public EntityAttribute Defend { get; private set; }
    public EntityAttribute KnockbackResistance { get; private set; }
    public EntityAttribute ShieldHealth { get; private set; }
    public EntityAttribute SkillCooldown { get; private set; }
    public EntityAttribute SkillDamageMultiplier { get; private set; }
    public int MaxAmmo { get; private set; }

    private float Health { get; set; }

    public EntityState State { get; private set; }
    public EntityType Type { get; private set; }

    public UnityAction<Entity> OnBeingAttackedEvent;
    public UnityAction OnDeadEvent;
    public UnityAction<Entity> OnUpdateEvent;
    public List<Buff> Buffs;
    public EntityData data;

    public Animator animator;
    [SerializeField] protected Collider coll;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected LayerMask terrainLayer;

    public Vector3 Velocity { get; protected set; }

    protected bool _useExternalForce;
    protected bool _attackable;
    protected bool _isStaticModel;

    public bool OnGround { get; private set; }

    public bool UseGravity { get; set; }

    public bool IsAttackable
    {
        get
        {
            return _attackable &&
                !(State == EntityState.Dead);
        }
    }

    protected virtual void OnAwake()
    {
    }
    protected virtual void OnStart()
    {
    }
    protected virtual void OnUpdate()
    {
    }

    protected void Awake()
    {
        _useExternalForce = true;
        _attackable = false;
        _isStaticModel = false;
        OnGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f, terrainLayer);
        if (OnGround)
            transform.position = hit.point;

        if (data != null)
        {
            LoadEntityData(data);
        }
        OnAwake();
    }

    protected void Start()
    {
        OnStart();
    }

    protected void Update()
    {
        OnUpdateEvent?.Invoke(this);

        OnGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f, terrainLayer);
        if (OnGround)
            transform.position = hit.point;
#if UNITY_EDITOR
        if (OnGround)
            Debug.DrawLine(transform.position, hit.point, Color.red);
#endif
        OnUpdate();
    }

    private float CalculateDamage(AttackInfo attackInfo)
    {
        // 防御力减伤后的实际伤害百分比 = 5000 / (3 * DEF + 5000)；
        float curDef = (Defend.CalculateValue() - attackInfo.IgnoreDefend) * (1 - attackInfo.IgnoreDefend);
        float defResistDmgFactor = 5000 / (3 * curDef + 5000);

        // 实际伤害 = 伤害值 * 实际伤害百分比；
        float ehp = attackInfo.Damage * defResistDmgFactor;

        return ehp;
    }

    private void Die()
    {
        State = EntityState.Dead;
        OnDeadEvent?.Invoke();
    }

    private void LoadEntityData(EntityData data)
    {
        MaxHealth = new EntityAttribute(data.MaxHealth);
        Speed = new EntityAttribute(data.Speed);
        AttackPower = new EntityAttribute(data.AttackPower);
        AttackRange = new EntityAttribute(data.AttackRange);
        AttackSpeed = new EntityAttribute(data.AttackSpeed);
        Defend = new EntityAttribute(data.Defend);
        KnockbackResistance = new EntityAttribute(data.KnockbackResistance);
        ShieldHealth = new EntityAttribute(data.ShieldHealth);
        SkillCooldown = new EntityAttribute(data.SkillCooldown);
        MaxAmmo = data.MaxAmmo;
    }

    public void ReceiveDamage(AttackInfo attackInfo)
    {
        float ehp = CalculateDamage(attackInfo);

        float shieldHealth = ShieldHealth.CalculateValue();
        if (shieldHealth > 0)
        {
            if (shieldHealth >= ehp)
            {
                ShieldHealth.IncreaseValue(-ehp);
                return;
            }
            else
            {
                ehp -= shieldHealth;
                ShieldHealth.IncreaseValue(-shieldHealth - 1f);
            }
        }
        Health -= ehp;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public void Heal(float value)
    {
        Mathf.Clamp(Health += value, 0, MaxHealth.CalculateValue());
    }

    public void RefreshAttributes()
    {

    }
}
