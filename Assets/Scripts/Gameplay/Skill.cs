using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkill : SkillBase
{
    protected override void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
        return;
    }
}
//Éä³öÒ»¿Å×Óµ¯
public class MomoiNormalAttackSkill : SkillBase
{
    public static Bullet bulletPrefab = (Bullet)DataManager.Instance.ParticlePrefabsData["ARNormalBullet"];

    private static readonly Vector3 _bulletPositionOffset = new Vector3(0.3f, 0.6f, 0.75f);
    private static readonly float _damageMultiplier = 0.6f;
    private static readonly float _bulletSpeed = 100f;

    protected override void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
        Bullet bullet = Instantiate(bulletPrefab, user.transform.position + _bulletPositionOffset, user.transform.rotation);
        bullet.AtkInfo = new()
        {
            Attacker = user,
            Damage = user.AttackPower.CalculateValue() * _damageMultiplier,
            Knockback = 0,
        };
    }
}
public class MomoiExSkill : SkillBase
{
    public GameObject projectilePrefab;

    protected override void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {

    }
}
public class MomoiBasicSkill : SkillBase
{
    public GameObject projectilePrefab;

    protected override void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {

    }
}
public class MomoiSubSkill : SkillBase
{
    public GameObject projectilePrefab;

    protected override void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {

    }
}