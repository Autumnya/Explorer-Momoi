using UnityEngine;

public enum TargetType
{
    Undefined = 0,

    //锁定Entity
    Entity = 1,
    //锁定位置
    Position = 2,
}

public abstract class SkillBase : MonoBehaviour
{
    public SkillAttributes Define;

    public abstract void Activate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3()); // 激活技能
    public virtual void Upgrade(int level)
    {
    }
}