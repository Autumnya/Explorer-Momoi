using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : SingletonMono<Player>
{
    private Character _targetCharacter;

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
    public UnityAction<Vector2> OnLookEvent;

    private PlayerControls _controls;

    private void Awake()
    {
        if (_targetCharacter != null)
            _targetCharacter.SetPlayerControl();
        else
        {
            _targetCharacter = Instantiate(CacheManager.Instance.GetPlayerCache().EntityData.Obj, WorldManager.Instance.PlayerSpawnPoint, Quaternion.Euler(Vector3.forward)).GetComponent<Character>();
        }

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
        _controls.GamePlay.Skill1.started += ctx => OnSkillEvent?.Invoke(SkillSlot.Skill1);
        _controls.GamePlay.Skill2.started += ctx => OnSkillEvent?.Invoke(SkillSlot.Skill2);
        _controls.GamePlay.Skill3.started += ctx => OnSkillEvent?.Invoke(SkillSlot.Skill3);
        _controls.GamePlay.Special.started += ctx => OnSkillEvent?.Invoke(SkillSlot.Special);
        _controls.GamePlay.Look.performed += ctx => OnLookEvent?.Invoke(ctx.ReadValue<Vector2>());

        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change == InputDeviceChange.Added)
                Debug.Log($"{device.name}");
        };
    }

    private void Start()
    {
        if(_targetCharacter != null)
        {
            _targetCharacter.SetPlayerControl();
            OnSetPlayerEvent?.Invoke(_targetCharacter);
        }
    }

    private void Update()
    {
    }
}
