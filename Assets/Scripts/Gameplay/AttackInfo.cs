using UnityEngine;

public enum AttackType
{
    Undefined = 0
}

public class AttackInfo
{
    public AttackType AttackType = AttackType.Undefined;
    public float Damage = 0f;
    public float Knockback = 0f;
    public float IgnoreDefend = 0f;
    public float IgnoreDefendPercent = 0f;
    public Vector3 HitPosition = Vector3.zero;
    public Character Attacker = null;
    public IEffect[] Effects = null;
}
