using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    // 计算夹角
    public static float CalculateAngleDeg(Vector3 vectorA, Vector3 vectorB)
    {
        // 归一化向量（避免手动计算模长）
        Vector3 normalizedA = vectorA.normalized;
        Vector3 normalizedB = vectorB.normalized;

        // 计算点积
        float dotProduct = Vector3.Dot(normalizedA, normalizedB);

        // 处理浮点误差导致的超出[-1,1]范围的情况
        dotProduct = Mathf.Clamp(dotProduct, -1f, 1f);

        // 计算弧度并转换为角度
        float angleInRadians = Mathf.Acos(dotProduct);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        return angleInDegrees;
    }
}
