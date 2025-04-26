using UnityEngine;

public class Bullet : Particle
{ 
    public float Speed { get; set; }
    public float MaxDistance { get; set; }
    public bool UseGravity { get; set; }

    protected override void OnAwake()
    {
        
    }

    protected override void OnActing()
    {
        float distanceThisFrame = Speed * Time.deltaTime;

        // 向前发射射线检测碰撞
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
            transform.Translate(Vector3.forward * distanceThisFrame); // 安全移动
        }
    }
}
