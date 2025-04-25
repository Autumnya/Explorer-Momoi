using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillAttributes", menuName = "Data/Skill Attributes")]
public class SkillAttributes : ScriptableObject
{
    //此属性ID仅用于匹配对应ID的技能，实际技能ID取决于技能工厂的注册
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }
    public TargetType Type { get; set; }
    public int MaxLevel { get; set; }
    public float Cooldown { get; set; }
}
