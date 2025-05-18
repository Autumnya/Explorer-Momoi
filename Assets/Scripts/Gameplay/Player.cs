using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : SingletonMono<Player>
{
    private Character _targetCharacter;

    [SerializeField] private TMP_Text _ammoLeftText;
    [SerializeField] private TMP_Text _ammoMiddleText;
    [SerializeField] private TMP_Text _ammoRightText;
    [SerializeField] private SkillContainer _specialSkillContainer;
    [SerializeField] private SkillContainer _skill1Container;
    [SerializeField] private SkillContainer _skill2Container;
    [SerializeField] private SkillContainer _skill3Container;
    [SerializeField] private Transform _buffPanel;

    public UnityAction<Character> OnSetPlayerEvent;

    public UnityAction OnJumpEvent;
    public UnityAction<Vector2> OnMoveStartEvent;
    public UnityAction<Vector2> OnMovePerformEvent;
    public UnityAction OnMoveEndEvent;
    public UnityAction OnTogglePauseEvent;
    public UnityAction OnKneelEvent;
    public UnityAction OnAimEvent;
    public UnityAction OnCancelAimEvent;
    public UnityAction OnAttackEvent;
    public UnityAction<SkillSlot> OnSkillEvent;
    public UnityAction OnCancelSkillEvent;
    public UnityAction<Vector2> OnLookEvent;

    private PlayerControls _controls;
    private SkillAttributes _curSkillAttr;
    [SerializeField] private LayerMask _terrainLayer;

    public UnityAction<Entity, Vector3> OnGetTargetEvent;

    private void Awake()
    {
        _curSkillAttr = null;
        InitInputSystem();
    }
    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    private void InitInputSystem()
    {
        _controls = new PlayerControls();

        _controls.GamePlay.Jump.started += ctx => OnJumpEvent?.Invoke();
        _controls.GamePlay.Move.started += ctx => OnMoveStartEvent?.Invoke(ctx.ReadValue<Vector2>());
        _controls.GamePlay.Move.performed += ctx => OnMovePerformEvent?.Invoke(ctx.ReadValue<Vector2>());
        _controls.GamePlay.Move.canceled += ctx => OnMoveEndEvent?.Invoke();
        _controls.GamePlay.Pause.started += ctx => OnTogglePauseEvent?.Invoke();
        _controls.GamePlay.Kneel.started += ctx => OnKneelEvent?.Invoke();
        _controls.GamePlay.Aim.started += ctx => OnAimEvent?.Invoke();
        _controls.GamePlay.Aim.canceled += ctx => OnCancelAimEvent?.Invoke();
        _controls.GamePlay.Attack.started += ctx => OnAttackEvent?.Invoke();
        _controls.GamePlay.Skill1.started += ctx => OnSkillButtonDown(SkillSlot.Skill1);
        _controls.GamePlay.Skill2.started += ctx => OnSkillButtonDown(SkillSlot.Skill2);
        _controls.GamePlay.Skill3.started += ctx => OnSkillButtonDown(SkillSlot.Skill3);
        _controls.GamePlay.Special.started += ctx => OnSkillButtonDown(SkillSlot.Special);
        _controls.GamePlay.Look.performed += ctx => OnLookEvent?.Invoke(ctx.ReadValue<Vector2>());

        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change == InputDeviceChange.Added)
                Debug.Log($"{device.name}");
        };
    }

    private void Start()
    {
        if (_targetCharacter != null)
            SetPlayerCharacter(_targetCharacter);
        else
            SetPlayerCharacter(WorldManager.Instance.CreatePlayerCharacter());
    }

    private void Update()
    {
        if (_curSkillAttr != null)
            SearchTarget();
    }
    private void SearchTarget()
    {
        switch (_curSkillAttr.TarType)
        {
            case TargetType.Position:

                break;
            case TargetType.Enemy:

                break;
        }
    }

    private void SetPlayerCharacter(Character character)
    {
        if (_targetCharacter != null)
        {
            _targetCharacter.RemovePlayerControl();
            _targetCharacter.OnNormalAttackEvent -= OnCharacterNormalAttack;
            _targetCharacter.OnReloadEvent -= OnChararcterReload;
            _targetCharacter.OnSkillReadyEvent -= SkillReady;
            _targetCharacter.OnSkillCancelEvent -= CancelSkillReady;
            _targetCharacter.OnSkillActivateEvent -= GetSkillTarget;
        }

        _targetCharacter = character;
        _targetCharacter.SetPlayerControl();

        _targetCharacter.OnNormalAttackEvent += OnCharacterNormalAttack;
        _targetCharacter.OnReloadEvent += OnChararcterReload;
        _targetCharacter.OnSkillReadyEvent += SkillReady;
        _targetCharacter.OnSkillCancelEvent += CancelSkillReady;
        _targetCharacter.OnSkillActivateEvent += GetSkillTarget;

        _ammoLeftText.text = _targetCharacter.AmmoRemain.ToString();
        _ammoRightText.text = _targetCharacter.MaxAmmo.ToString();

        _specialSkillContainer.SetSkill(_targetCharacter.SkillSlots[SkillSlot.Special]);
        _skill1Container.SetSkill(_targetCharacter.SkillSlots[SkillSlot.Skill1]);
        _skill2Container.SetSkill(_targetCharacter.SkillSlots[SkillSlot.Skill2]);
        _skill3Container.SetSkill(_targetCharacter.SkillSlots[SkillSlot.Skill3]);

        OnSetPlayerEvent?.Invoke(_targetCharacter);
    }

    private void OnCharacterNormalAttack(int ammoRemain)
    {
        _ammoLeftText.text = ammoRemain.ToString();
    }
    private void OnChararcterReload(int ammoRemain)
    {
        _ammoLeftText.text = ammoRemain.ToString();
    }

    private void SkillReady(SkillBase skl)
    {
        _curSkillAttr = skl.Define;
    }
    private void CancelSkillReady()
    {
        _curSkillAttr = null;
    }
    private void GetSkillTarget()
    {
        Vector3 charPos = _targetCharacter.transform.position;
        Vector3 screenAimPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
        if(_curSkillAttr == null)
        {
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _curSkillAttr.RangeRedius, _terrainLayer);
            if (hit.collider != null)
            {
                OnGetTargetEvent?.Invoke(null, hit.point);
                CancelSkillReady();
            }
            return;
        }
        switch (_curSkillAttr.TarType)
        {
            case TargetType.Position:
                Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _curSkillAttr.RangeRedius, _terrainLayer);
                if (hit.collider != null)
                {
                    OnGetTargetEvent?.Invoke(null, hit.point);
                    CancelSkillReady();
                }
                break;
            case TargetType.Enemy:
                Entity tar = null;
                float minAngle = float.MaxValue;
                foreach (var kv in _targetCharacter.EnemiesInRange)
                {
                    GameObject obj = kv.Value.gameObject;
                    float curAngle = Vector3.Angle(obj.transform.position - charPos, screenAimPos);
                    if (curAngle < 30 && curAngle < minAngle)
                    {
                        minAngle = curAngle;
                        tar = kv.Value;
                    }
                }
                if (tar != null)
                {
                    OnGetTargetEvent?.Invoke(tar, Vector3.zero);
                    CancelSkillReady();
                }
                break;
            case TargetType.Ally:
                Entity tarA = null;
                float minAngleA = float.MaxValue;
                foreach (var kv in _targetCharacter.AlliesInRange)
                {
                    GameObject obj = kv.Value.gameObject;
                    float curAngle = Vector3.Angle(obj.transform.position - charPos, screenAimPos);
                    if (curAngle < 30 && curAngle < minAngleA)
                    {
                        minAngle = curAngle;
                        tar = kv.Value;
                    }
                }
                if (tarA != null)
                {
                    OnGetTargetEvent?.Invoke(tarA, Vector3.zero);
                    CancelSkillReady();
                }
                break;
        }
    }

    private void OnSkillButtonDown(SkillSlot slot)
    {
        OnSkillEvent?.Invoke(SkillSlot.Skill1);
    }
}
