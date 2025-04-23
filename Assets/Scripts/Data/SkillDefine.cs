using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillDefine", menuName = "Data/Skill Define")]
public class SkillDefine : ScriptableObject
{
    public int SkillId;
    public string SkillName;
    public string Description;
    public Sprite Icon;
}
