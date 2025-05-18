using UnityEngine;

public class Bullet : Particle
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private bool _useGravity;
    [SerializeField] private float _remainTime;

    protected override void OnAwake()
    {
        
    }

    protected override void OnActing()
    {
        _remainTime -= Time.deltaTime;
        if(_remainTime < 0f)
        {
            Destroy(gameObject);
            return;
        }
        float distanceThisFrame = _speed * Time.deltaTime;

        // ��ǰ�������߼����ײ
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit1, distanceThisFrame, EntityMask))
        {
            if (WorldManager.Instance.OnHitEntity(hit1.collider, AtkInfo))
            {
                Destroy(gameObject);
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, distanceThisFrame, TerrainMask))
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * distanceThisFrame); // ��ȫ�ƶ�
        }
    }
}
