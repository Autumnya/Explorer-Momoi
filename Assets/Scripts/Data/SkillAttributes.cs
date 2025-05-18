using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillAttributes", menuName = "Data/Skill Attributes")]
public class SkillAttributes : ScriptableObject
{
    //此属性ID仅用于匹配对应ID的技能，实际技能ID取决于技能工厂的注册
    public int SkillId;
    public string SkillName;
    public string Description;
    public Sprite Icon;
    public TargetType TarType;
    public DamageType DmgType;
    public int MaxLevel;
    public float Cooldown;
    public GameObject Indicator;
    public float RangeRedius;
}
