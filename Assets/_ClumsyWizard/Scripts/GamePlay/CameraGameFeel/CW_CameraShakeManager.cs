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
    [SerializeField] private CW_Dictionary<CameraShakeType, CameraShakeData> shakeData; 
    private float currentTime;
    private float magnitude;

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool canShake;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            Shake(CameraShakeType.Low);

        if (Input.GetKeyDown(KeyCode.S))
            Shake(CameraShakeType.Medium);

        if (Input.GetKeyDown(KeyCode.D))
            Shake(CameraShakeType.High);

        if (Input.GetKeyDown(KeyCode.F))
            Shake(CameraShakeType.Ultra);

        if (!canShake)
            return;

        if(currentTime >= 0.0f)
        {
            transform.position = Random.insideUnitCircle * magnitude;
            currentTime -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
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