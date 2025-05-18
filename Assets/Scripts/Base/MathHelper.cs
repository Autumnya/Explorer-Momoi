using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    // ����н�
    public static float CalculateAngleDeg(Vector3 vectorA, Vector3 vectorB)
    {
        // ��һ�������������ֶ�����ģ����
        Vector3 normalizedA = vectorA.normalized;
        Vector3 normalizedB = vectorB.normalized;

        // ������
        float dotProduct = Vector3.Dot(normalizedA, normalizedB);

        // ���������µĳ���[-1,1]��Χ�����
        dotProduct = Mathf.Clamp(dotProduct, -1f, 1f);

        // ���㻡�Ȳ�ת��Ϊ�Ƕ�
        float angleInRadians = Mathf.Acos(dotProduct);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        return angleInDegrees;
    }
}
