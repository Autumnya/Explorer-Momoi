using UnityEngine;

public class Bullet : Particle
{
    public float Speed { get; set; }
    public float MaxDistance { get; set; }
    public bool UseGravity { get; set; }

    public AttackInfo AtkInfo { get; set; }

    protected override void OnActive()
    {
        float distanceThisFrame = Speed * Time.deltaTime;

        // 向前发射射线检测碰撞
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceThisFrame))
        {
            OnHitTarget(hit.collider); // 触发命中逻辑
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * distanceThisFrame); // 安全移动
        }
    }

    private void OnHitTarget(Collider target)
    {
        WorldManager.Instance.OnHitEntity(target, AtkInfo);
    }
}
