using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMono<SkillManager>
{
    private Dictionary<int, Type> _skillsDic;
    private Dictionary<int, SkillBase> _skillsObjDic;
    private int _curId = 0;

    public int ActivedSkillCount => _skillsObjDic.Count;

    protected override void OnAwake()
    {
        _skillsDic = new();
        _skillsObjDic = new();
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
            obj.transform.SetParent(gameObject.transform);
            return skill;
        }
        throw new KeyNotFoundException($"Skill {skillID} not registered or attributes not found.");
    }

    public List<SkillBase> GenerateAllSkills()
    {
        List<SkillBase> skills = new();
        foreach(var kv in _skillsDic)
        {
            skills.Add(CreateSkill(kv.Key));
        }
        return skills;
    }

    public void RemoveSkill(SkillBase skill)
    {
        if(skill != null)
        {
            _skillsObjDic.Remove(skill.ObjId);
            Destroy(skill.gameObject);
        }
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