using UnityEngine;
using UnityEngine.Events;

public enum TargetType
{
    Undefined = 0,
    //锁定Entity
    Enemy = 1,
    Ally = 2,
    //锁定位置
    Position = 3,
    //锁定自己
    Self = 4
}
public enum DamageType
{
    Undefined = 0,
    Normal = 1,
    Red = 2,
    Yellow = 3,
    Blue = 4,
    Purple = 5
}

public abstract class SkillBase : MonoBehaviour
{
    public int ObjId;
    public SkillAttributes Define;

    public UnityAction OnSkillActivateEvent;
    public UnityAction OnSkillEndCoolDownEvent;

    //达到0时结束冷却
    public float CoolDownTime { get; protected set; } = 0;
    private bool _isCooling = false;

    private void Awake()
    {
        OnAwake();
    }
    private void Update()
    {
        if (_isCooling)
        {
            CoolDownTime -= Time.deltaTime;
            if (CoolDownTime < 0)
            {
                CoolDownTime = 0;
                _isCooling = false;
                OnSkillEndCoolDownEvent?.Invoke();
            }
        }
    }
    public void Activate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
        if (!_isCooling)
        {
            OnSkillActivateEvent?.Invoke();
            OnActivate(user, targetEntity, targetPos);
            if (Define.Cooldown > 0f)
            {
                CoolDownTime = Define.Cooldown;
                _isCooling = true;
            }
        }
    }
    protected virtual void OnActivate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3())
    {
    }
    protected virtual void Upgrade(int level)
    {
    }
    protected virtual void OnAwake()
    {
    }
}