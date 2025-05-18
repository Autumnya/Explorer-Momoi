using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillSelectSlot : MonoBehaviour
{
    private SkillAttributes _curSkill;

    [SerializeField] private Image DamageTypeBg;
    [SerializeField] private Button SelectButton;
    [SerializeField] private Image SkillIcon;

    public UnityAction OnSlotClickedEvent;

    private void Awake()
    {
        SelectButton.onClick.AddListener(OnClickSkillSlot);
    }

    public void SetSkill(SkillAttributes skill)
    {
        _curSkill = skill;
        if (_curSkill == null)
        {
            DamageTypeBg.color = DataManager.Instance.DamageColorDic[DamageType.Undefined];
            SkillIcon.sprite = null;
            return;
        }
        DamageTypeBg.color = DataManager.Instance.DamageColorDic[_curSkill.DmgType];
        SkillIcon.sprite = _curSkill.Icon;
    }
    private void OnClickSkillSlot()
    {
        OnSlotClickedEvent?.Invoke();
    }
}
