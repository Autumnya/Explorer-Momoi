using UnityEngine;
using UnityEngine.Windows;

public class OrbitCamera : SingletonMono<OrbitCamera>
{
    public Transform target; // Ŀ������
    public float distance; // �������Ŀ��ľ���
    public float minDistance; // �������Ŀ�����С����
    public float maxDistance; // �������Ŀ���������
    public float xSpeed; // ˮƽ��ת�ٶ�
    public float ySpeed; // ��ֱ��ת�ٶ�
    public float yMinLimit; // ��ֱ��С�Ƕ�
    public float yMaxLimit;  // ��ֱ���Ƕ�
    public float offsetY; // ��ֱ�߶�

    private float rotateX;
    private float rotateY;
    private Vector2 lookInput;

    private void Awake()
    {
        // ��ʼ����ת�Ƕ�
        Vector3 angles = transform.eulerAngles;
        rotateX = angles.y;
        rotateY = angles.x;

        lookInput = Vector2.zero;

        // ����и������������������ת�Ա������
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

    // ���ƽǶ�
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
            // ��ȡ�������
            rotateX += input.x * xSpeed * Time.deltaTime;
            rotateY -= input.y * ySpeed * Time.deltaTime;
        }
    }

    private void FollowTarget()
    {
        if(target)
        {
            // ���ƴ�ֱ�Ƕ�
            rotateY = ClampAngle(rotateY, yMinLimit, yMaxLimit);

            // ������ת��λ��
            Quaternion rotation = Quaternion.Euler(rotateY, rotateX, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position + new Vector3(0f, offsetY, 0f);
            Ray ray = new(target.position, position);
            Physics.Raycast(ray, out RaycastHit hitInfo, distance);

            // Ӧ�õ������
            transform.rotation = rotation;
            transform.position = hitInfo.point;
            transform.position = position;
        }
    }
}
