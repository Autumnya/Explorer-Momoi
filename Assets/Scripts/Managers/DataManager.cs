using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    private readonly string DataPath = "Data/";
    private readonly string EntityDefineFolder = "Entities";
    private readonly string CharacterDefineFolder = "CharactersDefine";
    private readonly string SkillDefineFolder = "Skills";

    public Dictionary<int, EntityData> EntitiesDefineDic;
    public Dictionary<int, CharacterDefine> CharactersDefineDic;
    public Dictionary<int, SkillAttributes> SkillsDefineDic;
    public Dictionary<string, Particle> ParticlePrefabsData;
    public Dictionary<DamageType, Color> DamageColorDic;

    protected override void OnAwake()
    {
        EntitiesDefineDic = new();
        CharactersDefineDic = new();
        SkillsDefineDic = new();
        ParticlePrefabsData = new();

        LoadAssets();
        LoadParticlePrefabs();
        InitDamageColorDic();
        CacheManager.Instance.DataInit();
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

    private void InitDamageColorDic()
    {
        DamageColorDic = new()
        {
            [DamageType.Undefined] = new(0.2f, 0.2f, 0.2f, 0.2f),
            [DamageType.Normal] = new(0.2f, 0.2f, 0.2f, 1f),
            [DamageType.Red] = new(0.5911949f, 0.09579335f, 0f, 1f),
            [DamageType.Yellow] = new(0.9056604f, 0.5948873f, 0f, 1f),
            [DamageType.Blue] = new(0.02982464f, 0.4074356f, 0.7295597f, 1f),
            [DamageType.Purple] = new(0.5386199f, 0.05561471f, 0.9308176f, 1f)
        };
    }

    [SerializeField] private Bullet ARNormalBullet;
    [SerializeField] private Bullet SMGNormalBullet;
}
