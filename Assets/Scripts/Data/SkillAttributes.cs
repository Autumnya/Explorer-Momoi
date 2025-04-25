using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillAttributes", menuName = "Data/Skill Attributes")]
public class SkillAttributes : ScriptableObject
{
    //������ID������ƥ���ӦID�ļ��ܣ�ʵ�ʼ���IDȡ���ڼ��ܹ�����ע��
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public string Description { get; set; }
    public Sprite Icon { get; set; }
    public TargetType Type { get; set; }
    public int MaxLevel { get; set; }
    public float Cooldown { get; set; }
}
