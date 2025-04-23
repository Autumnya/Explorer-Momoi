using UnityEngine;
using UnityEngine.Windows;

public class OrbitCamera : SingletonMono<OrbitCamera>
{
    public Transform target; // 目标物体
    public float distance; // 摄像机与目标的距离
    public float minDistance; // 摄像机与目标的最小距离
    public float maxDistance; // 摄像机与目标的最大距离
    public float xSpeed; // 水平旋转速度
    public float ySpeed; // 垂直旋转速度
    public float yMinLimit; // 垂直最小角度
    public float yMaxLimit;  // 垂直最大角度
    public float offsetY; // 垂直高度

    private float rotateX;
    private float rotateY;
    private Vector2 lookInput;

    private void Awake()
    {
        // 初始化旋转角度
        Vector3 angles = transform.eulerAngles;
        rotateX = angles.y;
        rotateY = angles.x;

        lookInput = Vector2.zero;

        // 如果有刚体组件，禁用它的旋转以避免干扰
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        Player.Instance.OnSetPlayerEvent += (targetObj) => target = targetObj.transform;
        Player.Instance.OnLookEvent += Look;
    }

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    // 限制角度
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void Look(Vector2 input)
    {
        //Vector2 input = sysInput - lookInput;
        //lookInput = sysInput;
        if (target)
        {
            // 获取鼠标输入
            rotateX += input.x * xSpeed * Time.deltaTime;
            rotateY -= input.y * ySpeed * Time.deltaTime;
        }
    }

    private void FollowTarget()
    {
        if(target)
        {
            // 限制垂直角度
            rotateY = ClampAngle(rotateY, yMinLimit, yMaxLimit);

            // 计算旋转和位置
            Quaternion rotation = Quaternion.Euler(rotateY, rotateX, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position + new Vector3(0f, offsetY, 0f);
            Ray ray = new(target.position, position);
            Physics.Raycast(ray, out RaycastHit hitInfo, distance);

            // 应用到摄像机
            transform.rotation = rotation;
            transform.position = hitInfo.point;
            transform.position = position;
        }
    }
}
