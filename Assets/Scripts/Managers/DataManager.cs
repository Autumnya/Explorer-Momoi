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
    public Dictionary<int, SkillAttributes> SkillsDefineDic;
    public Dictionary<string, Particle> ParticlePrefabsData;

    protected override void OnAwake()
    {
        LoadAssets();
        LoadParticlePrefabs();
    }

    private void LoadAssets()
    {
        EntityData[] etts = Resources.LoadAll<EntityData>(Path.Combine(DataPath, EntityDefineFolder));
        CharacterDefine[] chars = Resources.LoadAll<CharacterDefine>(Path.Combine(DataPath, CharacterDefineFolder));
        SkillAttributes[] skls = Resources.LoadAll<SkillAttributes>(Path.Combine(DataPath, SkillDefineFolder));

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
        foreach (SkillAttributes skl in skls)
        {
            SkillsDefineDic ??= new();
            SkillsDefineDic.Add(skl.SkillId, skl);
        }
    }

    private void LoadParticlePrefabs()
    {
        ParticlePrefabsData["ARNormalBullet"] = ARNormalBullet;
        ParticlePrefabsData["SMGNormalBullet"] = SMGNormalBullet;
    }

    [SerializeField] private Bullet ARNormalBullet;
    [SerializeField] private Bullet SMGNormalBullet;
}
