using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillSelectItem : MonoBehaviour
{
    private SkillAttributes _skill;

    [SerializeField] private Image Bg;
    [SerializeField] private Image Icon;
    [SerializeField] private Button SkillItemButton;

    public UnityAction OnSkillItemClickedEvent;

    private void Awake()
    {
        SkillItemButton.onClick.AddListener(OnSkillItemClicked);
    }

    public void SetSkill(SkillAttributes skl)
    {
        _skill = skl;
        Bg.color = DataManager.Instance.DamageColorDic[_skill.DmgType];
        Icon.sprite = _skill.Icon;
    }

    private void OnSkillItemClicked()
    {
        OnSkillItemClickedEvent?.Invoke();
    }
}
