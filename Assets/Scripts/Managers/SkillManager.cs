using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMono<SkillManager>
{
    private Dictionary<int, Type> _skillsDic = new();
    private Dictionary<int, SkillBase> _skillsObjDic = new();
    private int _curId = 0;

    protected override void OnAwake()
    {
        RegisterAllSkills();
    }

    public void RegisterSkill<T>(int skillID) where T : SkillBase
    {
        _skillsDic[skillID] = typeof(T);
    }

    public SkillBase CreateSkill(int skillID)
    {
        if (_skillsDic.TryGetValue(skillID, out Type skillType) && DataManager.Instance.SkillsDefineDic.TryGetValue(skillID, out SkillAttributes define))
        {
            GameObject obj = new(skillType.Name);
            SkillBase skill = obj.AddComponent(skillType) as SkillBase;
            skill.ObjId = _curId;
            _skillsObjDic.Add(_curId, skill);
            _curId++;
            skill.Define = define;
            return skill;
        }
        throw new KeyNotFoundException($"Skill {skillID} not registered or attributes not found.");
    }

    public void RemoveSkill(SkillBase skill)
    {
        _skillsObjDic.Remove(skill.ObjId);
    }

    public void RegisterAllSkills()
    {
        RegisterSkill<EmptySkill>(0);
        RegisterSkill<MomoiNormalAttackSkill>(1);
        RegisterSkill<MomoiExSkill>(2);
        RegisterSkill<MomoiBasicSkill>(3);
        RegisterSkill<MomoiSubSkill>(4);
    }
}