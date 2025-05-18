using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillAttributes", menuName = "Data/Skill Attributes")]
public class SkillAttributes : ScriptableObject
{
    //������ID������ƥ���ӦID�ļ��ܣ�ʵ�ʼ���IDȡ���ڼ��ܹ�����ע��
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
