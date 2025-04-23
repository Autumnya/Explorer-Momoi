using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttribute
{
    private float BaseValue { get; } = 0f;
    private float ValueIncrease { get; set; } = 0f;
    private float ValuePercent { get; set; } = 1f;

    private float MinValue { get; }
    private float MaxValue { get; }

    public EntityAttribute(float value, float minValue = 0f, float maxValue = float.MaxValue)
    {
        BaseValue = value;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public float CalculateValue()
    {
        return Mathf.Clamp((BaseValue + ValueIncrease) * ValuePercent, MinValue, MaxValue);
    }

    public void IncreaseValue(float value)
    {
        ValueIncrease += value;
    }

    public void IncreasePercent(float percent)
    {
        ValuePercent += percent;
    }

    public void SetMinValue()
    {
        ValueIncrease = MinValue;
    }
}
