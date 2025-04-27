using UnityEngine;
using UnityEngine.Events;

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
    public int ObjId;
    public SkillAttributes Define;

    public UnityAction OnSkillActivateEvent;
    public UnityAction OnSkillEndCoolDownEvent;

    //达到0时结束冷却
    public float CoolDownTime = 0;

    private void Update()
    {
        if(CoolDownTime > 0)
        {
            CoolDownTime -= Time.deltaTime;
            if(CoolDownTime < 0 )
            {
                CoolDownTime = 0;
                OnSkillEndCoolDownEvent?.Invoke();
            }
        }
    }
    public void Activate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
        if(CoolDownTime > 0)
        {
            OnSkillActivateEvent?.Invoke();
            OnActivate(user, targetEntity, targetPos);
        }
    }
    public virtual void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
    }
    public virtual void Upgrade(int level)
    {
    }
}