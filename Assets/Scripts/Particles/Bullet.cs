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

        // ��ǰ�������߼����ײ
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanceThisFrame))
        {
            OnHitTarget(hit.collider); // ���������߼�
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * distanceThisFrame); // ��ȫ�ƶ�
        }
    }

    private void OnHitTarget(Collider target)
    {
        WorldManager.Instance.OnHitEntity(target, AtkInfo);
    }
}
