using ClumsyWizard.Core;
using ClumsyWizard.Utilities;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

public enum CameraShakeType
{
    Low,
    Medium,
    High,
    Ultra
}

[Serializable]
public struct CameraShakeData
{
    public float Duration;
    public float Magnitude;
}

public class CW_CameraShakeManager : CW_Singleton<CW_CameraShakeManager>
{
    [SerializeField] private Transform holder;
    [SerializeField] private CW_Dictionary<CameraShakeType, CameraShakeData> shakeData; 
    private float currentTime;
    private float magnitude;

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool canShake;

    private void Start()
    {
        originalPosition = holder.localPosition;
    }

    private void Update()
    {
        if (!canShake)
            return;

        if(currentTime >= 0.0f)
        {
            holder.localPosition = Random.insideUnitCircle * magnitude;
            currentTime -= Time.deltaTime;
        }
        else
        {
            holder.localPosition = originalPosition;
            canShake = false;
        }
    }

    public void Shake(CameraShakeType type)
    {
        currentTime = shakeData[type].Duration;
        magnitude = shakeData[type].Magnitude;
        canShake = true;
    }
}