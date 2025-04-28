using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum SkillSlot
{
    NormalAttack,
    Skill1,
    Skill2,
    Skill3,
    Special,
}

public class Character : Entity
{
    public Dictionary<SkillSlot, SkillBase> SkillSlots;
    public bool IsPlayerControl;
    public CharacterDefine Define;

    //参数：剩余子弹量
    public UnityAction<int> OnNormalAttackEvent;
    public UnityAction<int> OnReloadEvent;

    public float gravity;
    public float jumpForce;
    public float rotationSpeed;

    private Vector2 _moveInput;
    private Vector3 _controlVelocity;
    private Vector3 _externalVelocity;
    private int _ammoRemain;
    [SerializeField] private bool _isKneeling;
    [SerializeField] private bool _isAiming;

    protected override void OnAwake()
    {
        if(Define != null)
            data = Define.EntityData;
        int maxValue = Enum.GetValues(typeof(SkillSlot)).Cast<int>().Max();
        SkillSlots = new();

        _controlVelocity = Vector3.zero;
        _isKneeling = false;
        _isAiming = false;
        _ammoRemain = MaxAmmo;

        LoadDefaultSkill();
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        if (_isStaticModel)
            return;
        characterController.Move(Time.deltaTime * _controlVelocity);
        if(_useExternalForce)
        {
            Fall();
            Friction();
            characterController.Move(Time.deltaTime * _externalVelocity);
        }
    }

    private void OnDestroy()
    {
        RemovePlayerControl();
    }

    private void LoadDefaultSkill()
    {
        SkillSlots ??= new();
        SkillSlots[SkillSlot.NormalAttack] = SkillManager.Instance.CreateSkill(Define.DefaultNormalAttackSkillId);
        SkillSlots[SkillSlot.Skill1] = SkillManager.Instance.CreateSkill(Define.DefaultSkill1Id);
        SkillSlots[SkillSlot.Skill2] = SkillManager.Instance.CreateSkill(Define.DefaultSkill2Id);
        SkillSlots[SkillSlot.Skill3] = SkillManager.Instance.CreateSkill(Define.DefaultSkill3Id);
        SkillSlots[SkillSlot.Special] = SkillManager.Instance.CreateSkill(Define.DefaultSpecialSkillId);
    }

    public void SetPlayerControl()
    {
        if (!IsPlayerControl)
        {
            Player.Instance.OnAimEvent += Aim;
            Player.Instance.OnCancelAimEvent += CancelAim;
            Player.Instance.OnAttackEvent += Attack;
            Player.Instance.OnJumpEvent += Jump;
            Player.Instance.OnKneelEvent += Kneel;
            Player.Instance.OnMoveStartEvent += Move;
            Player.Instance.OnMovePerformEvent += UpdateMoveDirection;
            Player.Instance.OnMoveEndEvent += StopMove;
            Player.Instance.OnSkillEvent += ActivateSkill;
            IsPlayerControl = true;
        }
    }

    public void RemovePlayerControl()
    {
        if (IsPlayerControl)
        {
            Player.Instance.OnAimEvent -= Aim;
            Player.Instance.OnCancelAimEvent -= CancelAim;
            Player.Instance.OnAttackEvent -= Attack;
            Player.Instance.OnJumpEvent -= Jump;
            Player.Instance.OnKneelEvent -= Kneel;
            Player.Instance.OnMoveStartEvent -= Move;
            Player.Instance.OnMovePerformEvent -= UpdateMoveDirection;
            Player.Instance.OnMoveEndEvent -= StopMove;
            Player.Instance.OnSkillEvent -= ActivateSkill;
            IsPlayerControl = false;
        }
    }

    public void SetAsStaticModel()
    {
        _useExternalForce = true;
        _isStaticModel = true;
    }

    public void Pose()
    {
        animator.SetBool("Pose", true);
    }
    private void Move(Vector2 input)
    {
        animator.SetBool("Pose", false);
        _moveInput = input;

        OnUpdateEvent += PerformMove;
    }
    private void UpdateMoveDirection(Vector2 input)
    {
        _moveInput = input;
    }
    private void PerformMove(Entity entity)
    {
        // 获取前向向量，并投影到水平面
        Vector3 cameraForward = OrbitCamera.Instance.gameObject.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize(); // 归一化向量
        Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
        Quaternion moveDirection = Quaternion.LookRotation(new Vector3(_moveInput.x / 2, 0, _moveInput.y / 2));
        Quaternion targetDirection = cameraRotation * moveDirection;
        //transform.position += Time.deltaTime * (targetDirection * Vector3.forward) * Speed.CalculateValue();
        _controlVelocity = targetDirection * Vector3.forward * Speed.CalculateValue();

        //转向
        if (!animator.GetBool("IsMoving"))
            return;
        // 平滑插值到目标旋转
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetDirection,
            rotationSpeed * Time.deltaTime
        );
    }
    //KnockbackResistance:每秒降低由外力驱动的水平速度
    private void Friction()
    {
        float trueKnockbackResistance = KnockbackResistance.CalculateValue();
        if (!OnGround)
            trueKnockbackResistance *= 0.3f;
        float frameResist = trueKnockbackResistance * Time.deltaTime;
        Vector3 curHorizontalVelocity = new Vector3(_controlVelocity.x, 0, _controlVelocity.z);
        Vector3 resistVelocity = curHorizontalVelocity.normalized * frameResist;
        Vector3 afterResist = curHorizontalVelocity - resistVelocity;
        _externalVelocity = new Vector3(afterResist.x, _externalVelocity.y, afterResist.z);
    }
    private void Fall()
    {
        if (!OnGround)
        {
            _externalVelocity = new(_externalVelocity.x, _externalVelocity.y + (WorldManager.Gravity + gravity) * Time.deltaTime, _externalVelocity.z);
        }
        else if (_externalVelocity.y < 0.3f)
        {
            _externalVelocity = new Vector3(_externalVelocity.x, -2f, _externalVelocity.z);
        }
    }
    private void StopMove()
    {
        _moveInput = Vector2.zero;
        OnUpdateEvent -= PerformMove;
        _controlVelocity = new(0f, _controlVelocity.y, 0f);
        animator.SetBool("IsMoving", false);
    }
    private void Kneel()
    {
        _isKneeling = !_isKneeling;
        animator.SetBool("IsKneeling", _isKneeling);
    }
    private void Jump()
    {
        if (OnGround)
        {
            Debug.Log("Jump");
            //characterController.Move(new Vector3(0f, jumpForce * 0.1f, 0f));
            _externalVelocity = new(_externalVelocity.x, _externalVelocity.y + jumpForce, _externalVelocity.z);
            animator.SetTrigger("Jump");
        }
        else
        {
            Debug.Log("Jump Failed");
        }
    }
    private void Aim()
    {

        animator.SetBool("IsAiming", true);
    }
    private void CancelAim()
    {
        animator.SetBool("IsAiming", false);
        _isAiming = false;
    }
    private void Attack()
    {
        if(_ammoRemain <= 0)
        {
            Reload();
        }
        if(_isAiming)
        {
            animator.SetTrigger("Attack");
        }
    }
    private void Reload()
    {
        animator.SetTrigger("Reload");
    }

    private void ActivateSkill(SkillSlot slot)
    {
        if(SkillSlots.TryGetValue(slot, out SkillBase skill))
        {
            
        }
    }

    [SerializeField]
    private void OnNormalAttack(int attackIndex)
    {
        if(_ammoRemain > 0)
        {
            Debug.Log("Normal attack!");
            ActivateSkill(SkillSlot.NormalAttack);
            OnNormalAttackEvent(--_ammoRemain);
        }
        else
        {
            Reload();
        }
    }
    [SerializeField]
    private void OnAim()
    {
        _isAiming = true;
    }
    [SerializeField]
    private void OnCancelAim()
    {
        _isAiming = false;
    }
    [SerializeField]
    private void OnReload()
    {
        _ammoRemain = MaxAmmo;
        OnReloadEvent?.Invoke(_ammoRemain);
    }
}
