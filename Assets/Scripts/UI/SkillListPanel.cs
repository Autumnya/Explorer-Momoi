using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillListPanel : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private SkillSelectSlot Skill1Slot;
    [SerializeField] private SkillSelectSlot Skill2Slot;
    [SerializeField] private SkillSelectSlot Skill3Slot;
    [SerializeField] private SkillSelectSlot SpecialSkillSlot;

    [SerializeField] private Transform SkillListContent;
    [SerializeField] private SkillSelectItem SkillItemPrefab;

    public SkillSlot CurSlot;
    private Dictionary<SkillSlot, SkillSelectSlot> _skillSlotDic;
    //private Dictionary<int, SkillBase> _skillListDic;
    private bool _addedSkill;

    public void OnPointerExit(PointerEventData e)
    {
        gameObject.SetActive(false);
    }

    public void RefreshSkillList()
    {
        /*
        _skillListDic.Clear();
        List<SkillBase> skills = SkillManager.Instance.GenerateAllSkills();
        foreach(SkillBase skl in skills)
        {
            _skillListDic[skl.Define.SkillId] = skl;
            SkillSelectItem skillItem = Instantiate(SkillItemPrefab, SkillListContent);
            skillItem.SetSkill(skl);
            skillItem.OnSkillItemClickedEvent += () => SelectSkill(skl);
        }
        */
        foreach(var kv in DataManager.Instance.SkillsDefineDic)
        {
            SkillSelectItem skillItem = Instantiate(SkillItemPrefab, SkillListContent);
            skillItem.SetSkill(kv.Value);
            skillItem.OnSkillItemClickedEvent += () => SelectSkill(kv.Value);
        }

        _addedSkill = true;
    }

    private void Awake()
    {
        //_skillListDic = new();
        _addedSkill = false;
        Skill1Slot.SetSkill(null);
        Skill2Slot.SetSkill(null);
        Skill3Slot.SetSkill(null);
        SpecialSkillSlot.SetSkill(null);

        Skill1Slot.OnSlotClickedEvent += () => StartSelect(SkillSlot.Skill1);
        Skill2Slot.OnSlotClickedEvent += () => StartSelect(SkillSlot.Skill2);
        Skill3Slot.OnSlotClickedEvent += () => StartSelect(SkillSlot.Skill3);
        SpecialSkillSlot.OnSlotClickedEvent += () => StartSelect(SkillSlot.Special);

        _skillSlotDic = new()
        {
            [SkillSlot.Skill1] = Skill1Slot,
            [SkillSlot.Skill2] = Skill2Slot,
            [SkillSlot.Skill3] = Skill3Slot,
            [SkillSlot.Special] = SpecialSkillSlot
        };

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(!_addedSkill)
        {
            /*
            foreach(var kv in _skillListDic)
            {
                SkillManager.Instance.RemoveSkill(kv.Value);
            }
            */
            RefreshSkillList();
        }
    }

    private void StartSelect(SkillSlot slot)
    {
        gameObject.SetActive(true);
        CurSlot = slot;
    }
    private void SelectSkill(SkillAttributes skl)
    {
        CacheManager.Instance.SetCacheSkill(CurSlot, skl.SkillId);
        _skillSlotDic[CurSlot].SetSkill(skl);
        gameObject.SetActive(false);
    }
}
