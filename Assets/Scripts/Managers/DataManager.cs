using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    public readonly string DataPath = "Data/";
    public readonly string EntityDefineFolder = "Entities";
    public readonly string CharacterDefineFolder = "CharactersDefine";
    public readonly string SkillDefineFolder = "Skills";

    public Dictionary<int, EntityData> EntitiesDefineDic;
    public Dictionary<int, CharacterDefine> CharactersDefineDic;
    public Dictionary<int, SkillDefine> SkillsDefineDic;

    protected override void OnAwake()
    {
        LoadAssets();
    }

    private void LoadAssets()
    {
        EntityData[] etts = Resources.LoadAll<EntityData>(Path.Combine(DataPath, EntityDefineFolder));
        CharacterDefine[] chars = Resources.LoadAll<CharacterDefine>(Path.Combine(DataPath, CharacterDefineFolder));
        SkillDefine[] skls = Resources.LoadAll<SkillDefine>(Path.Combine(DataPath, SkillDefineFolder));

        foreach (EntityData ett in etts)
        {
            EntitiesDefineDic ??= new();
            EntitiesDefineDic.Add(ett.EntityId, ett);
        }
        foreach (CharacterDefine cha in chars)
        {
            CharactersDefineDic ??= new();
            CharactersDefineDic.Add(cha.CharacterId, cha);
        }
        foreach (SkillDefine skl in skls)
        {
            SkillsDefineDic ??= new();
            SkillsDefineDic.Add(skl.SkillId, skl);
        }
    }
}
