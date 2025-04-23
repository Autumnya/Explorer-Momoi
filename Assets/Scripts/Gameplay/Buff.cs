using UnityEngine;

public enum BuffType
{
    Buff,
    Debuff
}
public abstract class Buff : ScriptableObject
{
    protected float Duration;
    protected BuffType Type;

    public Sprite Icon;

    public virtual void Apply(Entity target)
    {
        target.Buffs.Add(this);
        target.OnUpdateEvent += OnUpdate;
    }
    protected virtual void OnUpdate(Entity target)
    {
        Duration -= Time.deltaTime;
        if (Duration <= 0)
        {
            target.Buffs.Remove(this);
            OnRemove(target);
        }
    }
    protected virtual void OnRemove(Entity target)
    {
        target.OnUpdateEvent -= OnUpdate;
        target.Buffs.Remove(this);
    }
}

//提升固定数值
public class AttackUpValueBuff : Buff
{
    private float AtkUpValue;

    public AttackUpValueBuff(float atkUpValue)
    {
        AtkUpValue = atkUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackPower.IncreaseValue(AtkUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackPower.IncreaseValue(-AtkUpValue);
    }
}
public class DefendUpValueBuff : Buff
{
    private float DefUpValue;

    public DefendUpValueBuff(float defUpValue)
    {
        DefUpValue = defUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Defend.IncreaseValue(DefUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Defend.IncreaseValue(-DefUpValue);
    }
}
public class SpeedUpValueBuff : Buff
{
    private float SpdUpValue;

    public SpeedUpValueBuff(float spdUpValue)
    {
        SpdUpValue = spdUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Speed.IncreaseValue(SpdUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Speed.IncreaseValue(-SpdUpValue);
    }
}
public class AttackSpeedUpValueBuff : Buff
{
    private float AtkSpdUpValue;

    public AttackSpeedUpValueBuff(float atkspdUpValue)
    {
        AtkSpdUpValue = atkspdUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackSpeed.IncreaseValue(AtkSpdUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackSpeed.IncreaseValue(-AtkSpdUpValue);
    }
}
public class AttackRangeUpValueBuff : Buff
{
    private float AtkRagValue;

    public AttackRangeUpValueBuff(float atkragValue)
    {
        AtkRagValue = atkragValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackRange.IncreaseValue(AtkRagValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackRange.IncreaseValue(-AtkRagValue);
    }
}
public class SkillCooldownUpValueBuff : Buff
{
    private float SklCdUpValue;

    public SkillCooldownUpValueBuff(float sklCdUpValue)
    {
        SklCdUpValue = sklCdUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillCooldown.IncreaseValue(SklCdUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillCooldown.IncreaseValue(-SklCdUpValue);
    }
}
public class SkillDamageMultiplierUpValueBuff : Buff
{
    private float SklMulUpValue;

    public SkillDamageMultiplierUpValueBuff(float skilMulUpValue)
    {
        SklMulUpValue = skilMulUpValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillDamageMultiplier.IncreaseValue(SklMulUpValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillDamageMultiplier.IncreaseValue(-SklMulUpValue);
    }
}

//提升百分比
public class AttackUpPercentBuff : Buff
{
    private float AtkUpPercent;

    public AttackUpPercentBuff(float atkUpPercent)
    {
        AtkUpPercent = atkUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackPower.IncreasePercent(AtkUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackPower.IncreasePercent(-AtkUpPercent);
    }
}
public class DefendUpPercentBuff : Buff
{
    private float DefUpPercent;

    public DefendUpPercentBuff(float defUpPercent)
    {
        DefUpPercent = defUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Defend.IncreasePercent(DefUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Defend.IncreasePercent(-DefUpPercent);
    }
}
public class SpeedUpPercentBuff : Buff
{
    private float SpdUpPercent;

    public SpeedUpPercentBuff(float spdUpPercent)
    {
        SpdUpPercent = spdUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Speed.IncreasePercent(SpdUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Speed.IncreasePercent(-SpdUpPercent);
    }
}
public class AttackSpeedUpPercentBuff : Buff
{
    private float AtkSpdUpPercent;

    public AttackSpeedUpPercentBuff(float atkspdUpPercent)
    {
        AtkSpdUpPercent = atkspdUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackSpeed.IncreasePercent(AtkSpdUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackSpeed.IncreasePercent(-AtkSpdUpPercent);
    }
}
public class AttackRangeUpPercentBuff : Buff
{
    private float AtkRagPercent;

    public AttackRangeUpPercentBuff(float atkragPercent)
    {
        AtkRagPercent = atkragPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackRange.IncreasePercent(AtkRagPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackRange.IncreasePercent(-AtkRagPercent);
    }
}
public class SkillCooldownUpPercentBuff : Buff
{
    private float SklCdUpPercent;

    public SkillCooldownUpPercentBuff(float sklCdUpPercent)
    {
        SklCdUpPercent = sklCdUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillCooldown.IncreasePercent(SklCdUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillCooldown.IncreasePercent(-SklCdUpPercent);
    }
}
public class SkillDamageMultiplierUpPercentBuff : Buff
{
    private float SklMulUpPercent;

    public SkillDamageMultiplierUpPercentBuff(float skilMulUpPercent)
    {
        SklMulUpPercent = skilMulUpPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillDamageMultiplier.IncreasePercent(SklMulUpPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillDamageMultiplier.IncreasePercent(-SklMulUpPercent);
    }
}

//降低固定数值
public class AttackDownValueBuff : Buff
{
    private float AtkDownValue;

    public AttackDownValueBuff(float atkDownValue)
    {
        AtkDownValue = atkDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackPower.IncreaseValue(-AtkDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackPower.IncreaseValue(AtkDownValue);
    }
}
public class DefendDownValueBuff : Buff
{
    private float DefDownValue;

    public DefendDownValueBuff(float defDownValue)
    {
        DefDownValue = defDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Defend.IncreaseValue(-DefDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Defend.IncreaseValue(DefDownValue);
    }
}
public class SpeedDownValueBuff : Buff
{
    private float SpdDownValue;

    public SpeedDownValueBuff(float spdDownValue)
    {
        SpdDownValue = spdDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Speed.IncreaseValue(-SpdDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Speed.IncreaseValue(SpdDownValue);
    }
}
public class AttackSpeedDownValueBuff : Buff
{
    private float AtkSpdDownValue;

    public AttackSpeedDownValueBuff(float atkspdDownValue)
    {
        AtkSpdDownValue = atkspdDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackSpeed.IncreaseValue(-AtkSpdDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackSpeed.IncreaseValue(AtkSpdDownValue);
    }
}
public class AttackRangeDownValueBuff : Buff
{
    private float AtkRagDownValue;

    public AttackRangeDownValueBuff(float atkragDownValue)
    {
        AtkRagDownValue = atkragDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackRange.IncreaseValue(-AtkRagDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackRange.IncreaseValue(AtkRagDownValue);
    }
}
public class SkillCooldownDownValueBuff : Buff
{
    private float SklCdDownValue;

    public SkillCooldownDownValueBuff(float sklCdDownValue)
    {
        SklCdDownValue = sklCdDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillCooldown.IncreaseValue(-SklCdDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillCooldown.IncreaseValue(SklCdDownValue);
    }
}
public class SkillDamageMultiplierDownValueBuff : Buff
{
    private float SklMulDownValue;

    public SkillDamageMultiplierDownValueBuff(float skilMulDownValue)
    {
        SklMulDownValue = skilMulDownValue;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillDamageMultiplier.IncreaseValue(-SklMulDownValue);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillDamageMultiplier.IncreaseValue(SklMulDownValue);
    }
}

//降低百分比
public class AttackDownPercentBuff : Buff
{
    private float AtkDownPercent;

    public AttackDownPercentBuff(float atkDownPercent)
    {
        AtkDownPercent = atkDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackPower.IncreasePercent(-AtkDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackPower.IncreasePercent(AtkDownPercent);
    }
}
public class DefendDownPercentBuff : Buff
{
    private float DefDownPercent;

    public DefendDownPercentBuff(float defDownPercent)
    {
        DefDownPercent = defDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Defend.IncreasePercent(-DefDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Defend.IncreasePercent(DefDownPercent);
    }
}
public class SpeedDownPercentBuff : Buff
{
    private float SpdDownPercent;

    public SpeedDownPercentBuff(float spdDownPercent)
    {
        SpdDownPercent = spdDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.Speed.IncreasePercent(-SpdDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.Speed.IncreasePercent(SpdDownPercent);
    }
}
public class AttackSpeedDownPercentBuff : Buff
{
    private float AtkSpdDownPercent;

    public AttackSpeedDownPercentBuff(float atkspdDownPercent)
    {
        AtkSpdDownPercent = atkspdDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackSpeed.IncreasePercent(-AtkSpdDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackSpeed.IncreasePercent(AtkSpdDownPercent);
    }
}
public class AttackRangeDownPercentBuff : Buff
{
    private float AtkRagDownPercent;

    public AttackRangeDownPercentBuff(float atkragDownPercent)
    {
        AtkRagDownPercent = atkragDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.AttackRange.IncreasePercent(-AtkRagDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.AttackRange.IncreasePercent(AtkRagDownPercent);
    }
}
public class SkillCooldownDownPercentBuff : Buff
{
    private float SklCdDownPercent;

    public SkillCooldownDownPercentBuff(float sklCdDownPercent)
    {
        SklCdDownPercent = sklCdDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillCooldown.IncreasePercent(-SklCdDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillCooldown.IncreasePercent(SklCdDownPercent);
    }
}
public class SkillDamageMultiplierDownPercentBuff : Buff
{
    private float SklMulDownPercent;

    public SkillDamageMultiplierDownPercentBuff(float skilMulDownPercent)
    {
        SklMulDownPercent = skilMulDownPercent;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.SkillDamageMultiplier.IncreasePercent(-SklMulDownPercent);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.SkillDamageMultiplier.IncreasePercent(SklMulDownPercent);
    }
}

public class ShieldBuff : Buff
{
    private float ShieldHealth;

    public ShieldBuff(float shieldHealth)
    {
        ShieldHealth = shieldHealth;
    }
    public override void Apply(Entity target)
    {
        base.Apply(target);
        target.ShieldHealth.IncreaseValue(-ShieldHealth);
    }
    protected override void OnRemove(Entity target)
    {
        base.OnRemove(target);
        target.ShieldHealth.IncreaseValue(ShieldHealth);
    }
}
public class PoisonBuff : Buff
{
    private static readonly float Interval = 1000;
    //累计时间
    private float _curInterval = 0;
    private float _poisonDamage;

    public PoisonBuff(float poisonDamage)
    {
        _poisonDamage = poisonDamage;
    }

    protected override void OnUpdate(Entity target)
    {
        base.OnUpdate(target);
        _curInterval += Time.deltaTime;
        while (_curInterval > Interval)
        {
            target.ReceiveDamage(new AttackInfo()
            {
                Damage = _poisonDamage,
                IgnoreDefendPercent = 1f
            });
            _curInterval -= Interval;
        }
    }
}
public class BurnBuff : Buff
{
    private float _burnDamage;
    private float _curInterval = 0;
    private float Interval = 200f;

    public BurnBuff(float burnDamage)
    {
        _burnDamage = burnDamage;
    }

    protected override void OnUpdate(Entity target)
    {
        base.OnUpdate(target);
        _curInterval += Time.deltaTime;
        while (_curInterval > Interval)
        {
            target.ReceiveDamage(new AttackInfo()
            {
                Damage = _burnDamage,
                IgnoreDefendPercent = 1f
            });
            _curInterval -= Interval;
        }
    }
}