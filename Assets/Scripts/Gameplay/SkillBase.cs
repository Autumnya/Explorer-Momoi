using System.Collections.Generic;
using System;
using UnityEngine;

public enum TargetType
{
    Undefined = 0,

    //锁定Entity
    Entity = 1,
    //锁定位置
    Position = 2,
}

public class SkillFactory : Singleton<SkillFactory>
{
    private Dictionary<int, Type> _skillsDic;

    public void RegisterSkill<T>(int skillID) where T : SkillBase
    {
        _skillsDic[skillID] = typeof(T);
    }

    public SkillBase CreateSkill(int skillID)
    {
        if (_skillsDic.TryGetValue(skillID, out Type type))
        {
            SkillBase skill = (SkillBase)Activator.CreateInstance(type);
            skill.Define = DataManager.Instance.SkillsDefineDic[skillID];
            return skill;
        }
        throw new KeyNotFoundException($"Skill {skillID} not registered");
    }

    public void RegisterAllSkills()
    {
        RegisterSkill<MomoiNormalAttackSkill>(1);
        RegisterSkill<MomoiExSkill>(2);
        RegisterSkill<MomoiBasicSkill>(3);
        RegisterSkill<MomoiEnhancedSkill>(4);
        RegisterSkill<MomoiSubSkill>(5);
    }
}

public abstract class SkillBase : MonoBehaviour
{
    public SkillAttributes Define;

    public abstract void Activate(Character user, Entity targetEntity = null, Vector3 targetPos = new Vector3()); // 激活技能
    public virtual void Upgrade(int level)
    {
    }
}