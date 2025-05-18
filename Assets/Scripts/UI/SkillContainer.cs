using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillContainer : MonoBehaviour
{
    private SkillBase _targetSkill;

    [SerializeField] private Image Thumbnail;
    [SerializeField] private Image CoolDownMask;
    [SerializeField] private TMP_Text BindKey;
    [SerializeField] private Image PanelBg;

    private float _width;
    private float _height;
    private bool _isCooling;

    public void SetSkill(SkillBase skl)
    {
        if (skl == null)
            return;
        _targetSkill = skl;
        skl.OnSkillActivateEvent += StartCoolDown;
        skl.OnSkillEndCoolDownEvent += EndCoolDown;

        PanelBg.color = DataManager.Instance.DamageColorDic[_targetSkill.Define.DmgType];
        Thumbnail.sprite = _targetSkill.Define.Icon;
    }
    public void ResetSkill()
    {
        _targetSkill.OnSkillActivateEvent -= StartCoolDown;
        _targetSkill.OnSkillEndCoolDownEvent -= EndCoolDown;
        _targetSkill = null;
    }

    private void Awake()
    {
        _width = CoolDownMask.rectTransform.rect.width;
        _height = CoolDownMask.rectTransform.rect.height;
        CoolDownMask.gameObject.SetActive(false);
        _isCooling = false;
    }
    private void Update()
    {
        if (_targetSkill != null && _isCooling)
        {
            UpdateCoolDownMask();
        }
    }

    private void StartCoolDown()
    {
        CoolDownMask.gameObject.SetActive(true);
        CoolDownMask.rectTransform.localScale = new Vector3(1, 1, 1);
    }
    private void UpdateCoolDownMask()
    {
        float percent = _targetSkill.CoolDownTime / _targetSkill.Define.Cooldown;
        CoolDownMask.rectTransform.localScale = new Vector3(1, percent, 1);
    }
    private void EndCoolDown()
    {
        CoolDownMask.gameObject.SetActive(false);
    }
}
