using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//管理缓存、跨场景数据、持久化数据等
public class CacheManager : Singleton<CacheManager>
{
    //Player
    private int _charId = -1;
    private Dictionary<SkillSlot, int> _sklDic = new();
    private bool _inited = false;

    public Dictionary<int, GameObject> Objs;

    public void DataInit()
    {
        //_sklDic[SkillSlot.NormalAttack] = 0;
        _sklDic[SkillSlot.Special] = 0;
        _sklDic[SkillSlot.Skill1] = 0;
        _sklDic[SkillSlot.Skill2] = 0;
        _sklDic[SkillSlot.Skill3] = 0;
        _inited = true;
    }

    public void SetPlayerChar(int charId)
    {
        _charId = charId;
    }
    public void SetCacheSkill(SkillSlot slot, int sklId)
    {
        if (!_inited)
            return;
        _sklDic[slot] = sklId;
    }
    private bool CheckPlayerCharacter()
    {
        if (_charId == -1)
            return false;
        return true;
    }

    public Character GetPlayerCache()
    {
        if (!CheckPlayerCharacter())
            return null;
        Character c = EntityFactory.Instance.CreateCharacter(_charId);
        //c.SetSkill(SkillSlot.NormalAttack, _sklDic[SkillSlot.NormalAttack]);
        c.SetSkill(SkillSlot.NormalAttack, c.Define.DefaultNormalAttackSkillId);
        c.SetSkill(SkillSlot.Skill1, _sklDic[SkillSlot.Skill1]);
        c.SetSkill(SkillSlot.Skill2, _sklDic[SkillSlot.Skill2]);
        c.SetSkill(SkillSlot.Skill3, _sklDic[SkillSlot.Skill3]);
        c.SetSkill(SkillSlot.Special, _sklDic[SkillSlot.Special]);
        c.SetPlayerControl();
        return c;
    }
}
