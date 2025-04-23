using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterDefine", menuName = "Data/Character Define")]
public class CharacterDefine : ScriptableObject
{
    public int CharacterId;
    public string CharacterName;
    public string CharacterName_CN;
    public string WeaponType;
    public Sprite Avator;
    public EntityData EntityData;
}
