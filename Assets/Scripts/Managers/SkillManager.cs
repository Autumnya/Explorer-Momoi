using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMono<SkillManager>
{
    private Dictionary<int, Type> _skillsDic = new();
    private Dictionary<int, SkillBase> _skillsObjDic = new();

    protected override void OnAwake()
    {
        RegisterAllSkills();
    }

    public void RegisterSkill<T>(int skillID) where T : SkillBase
    {
        _skillsDic[skillID] = typeof(T);
    }

    public SkillBase GetSkill(int skillID)
    {
        if (_skillsDic.TryGetValue(skillID, out Type skillType))
        {
            GameObject obj = new(skillType.Name);
            SkillBase skill = obj.AddComponent(skillType) as SkillBase;
            skill.Define = DataManager.Instance.SkillsDefineDic[skillID];
            return skill;
        }
        throw new KeyNotFoundException($"Skill {skillID} not registered");
    }

    public void RegisterAllSkills()
    {
        RegisterSkill<EmptySkill>(0);
        RegisterSkill<MomoiNormalAttackSkill>(1);
        RegisterSkill<MomoiExSkill>(2);
        RegisterSkill<MomoiBasicSkill>(3);
        RegisterSkill<MomoiEnhancedSkill>(4);
        RegisterSkill<MomoiSubSkill>(5);
    }
}